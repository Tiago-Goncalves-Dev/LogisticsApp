# LogisticsApp API – Orders Module (Current State)

Este README serve para documentar o estado atual do módulo de Orders da tua API, para que amanhã possas lembrar facilmente o que já está feito, o que cada peça faz, e como tudo se liga. A ideia é ser o “mapa mental” do projeto, não só a lista de ficheiros.

---

## Visão geral da arquitetura

O projeto segue um estilo **API REST com camadas**:

- **Domain**  
  Contém as entidades de negócio (por exemplo, `Order`) e enums associados (`OrderStatus`, `DeliveryType`).
- **Infrastructure**  
  Contém o `LogisticsDbContext` (EF Core) e a ligação à base de dados.
- **Services**  
  Contém a lógica de negócio aplicada a entidades (por exemplo, `OrderService`).
- **Controllers**  
  Exponem endpoints HTTP (por exemplo, `OrderController`) usando DTOs para entrada/saída.

O fluxo típico é:

> Frontend → DTO de entrada → Controller → Service → DbContext/BD  
> DbContext/BD → Service → Entidade → DTO de saída → Controller → Frontend

Este padrão separa bem:

- o que entra (input DTO)
- o que vive no domínio (entidade)
- o que sai (output DTO)
- de quem fala com HTTP (controller)
- e quem fala com BD (service + DbContext). [web:173][web:181][web:184][web:199]

---

## Entidade de domínio: `Order`

**Ficheiro:** `LogisticsApp.Api.Domain/Order.cs`

A entidade `Order` representa uma encomenda na BD:

- `int Id`  
  Chave primária técnica (auto-increment, tratada pelo EF/BD).
- `string? OrderNumber`  
  Identificador “de negócio” (neste momento gerado com `Guid.NewGuid().ToString()` no service).
- `int CustomerId`
- `DeliveryType DeliveryType`
  - Enum: `Pickup` ou `HomeDelivery`.
- `string? PickupAddress`
- `string? HomeDeliveryAddress`
- `decimal WeightKg`
- `OrderStatus Status`
  - Enum: `Created`, `OnGoing`, `Finished`.
- `DateTime CreatedAt`
- `DateTime LastEditedAt`
- `DateTime? PlannedDeliveryDate`

Responsabilidades da entidade:

- Representar o estado completo de uma Order no domínio e na BD.
- Guardar tanto valores vindos do cliente como valores definidos pelo sistema (status, datas, número, etc.). [web:124][web:164][web:167]

---

## DTOs

### 1. DTO de entrada: `CreateOrderDto`

**Ficheiro:** `LogisticsApp.Api.Dtos/CreateOrderDto.cs` (nome esperado)

Responsabilidade:

- Representar os dados que o **frontend envia** para criar uma order nova.

Campos típicos (com base no uso no service):

- `int CustomerId`
- `DeliveryType DeliveryType`
- `decimal WeightKg`
- `DateTime? PlannedDeliveryDate`
- `string? PickupAddress`
- `string? HomeDeliveryAddress`

Este DTO é ligado ao JSON que chega no `POST /api/order`.  
Ele **não** contém campos internos como `Status`, `CreatedAt` ou `OrderNumber` — isso é responsabilidade do backend. [web:173][web:182][web:184]

---

### 2. DTO de saída: `OrderInfoDto`

**Ficheiro:** `LogisticsApp.Api.Dtos/OrderInfoDto.cs`

Responsabilidade:

- Representar os dados que a API devolve ao frontend **depois** de criar uma order (resumo para mostrar ao utilizador logo após o POST).

Campos:

- `string? OrderNumber`
- `int CustomerId`
- `DeliveryType DeliveryType`
- `string? PickupAddress`
- `string? HomeDeliveryAddress`
- `decimal WeightKg`
- `OrderStatus Status`
- `DateTime CreatedAt`
- `DateTime? PlannedDeliveryDate`

Este DTO é construído no `OrderController` a partir da `Order` devolvida pelo `OrderService`. [web:173][web:184][web:199]

---

## Service: `OrderService`

**Ficheiro:** `LogisticsApp.Api.Services/OrderService.cs`

Responsabilidade:

- Implementar a lógica de negócio de Orders, sobretudo criação neste momento.
- Fazer o mapeamento `CreateOrderDto` → `Order`.
- Falar com o `LogisticsDbContext` para gravar as Orders. [web:170][web:173][web:163]

Dependência principal:

- `LogisticsDbContext _context` (injetado via construtor).

### Método principal: `CreateOrderAsync`

Assinatura:

```csharp
public async Task<Order> CreateOrderAsync(CreateOrderDto dto)
```

Função (em palavras):

1. Cria uma nova `Order` com base no `CreateOrderDto`:
   - Copia:
     - `CustomerId`
     - `DeliveryType`
     - `WeightKg`
     - `PlannedDeliveryDate`
     - `PickupAddress` / `HomeDeliveryAddress` consoante o tipo de entrega.
   - Define regras internas:
     - `OrderNumber = Guid.NewGuid().ToString()` (string única)
     - `Status = OrderStatus.Created`
     - `CreatedAt = DateTime.Now`
     - `LastEditedAt = DateTime.Now`
2. Adiciona a `Order` ao `_context.Orders` (`AddAsync`).
3. Chama `_context.SaveChangesAsync()` para gravar na BD.
4. Devolve a `Order` criada (com `Id` atribuído pela BD). [web:148][web:151][web:163]

Importante:

- O service **não** sabe nada de HTTP, `ActionResult` ou controllers.
- Trabalha com entidades e DTOs, e com EF Core. [web:124][web:167]

---

## Controller: `OrderController`

**Ficheiro:** `LogisticsApp.Api.Controllers/OrderController.cs`

Responsabilidade:

- Expor endpoints HTTP (REST) para Orders.
- Falar com o `OrderService` para aplicar lógica de negócio.
- Mapear entre DTOs e objetos de domínio para entrada/saída. [web:188][web:192]

Configuração:

- `[ApiController]`
- `[Route("api/[controller]")]`  
  → Rota base: `api/order` (por causa do nome `OrderController`). [web:186][web:195]
- Herda de `ControllerBase`.

Dependência:

- `OrderService _orderService` injetado via construtor (Dependency Injection). [web:191][web:197]

### Endpoint implementado: `POST /api/order`

Assinatura:

```csharp
[HttpPost]
public async Task<ActionResult<OrderInfoDto>> CreateOrder(CreateOrderDto createorder)
```

Pipeline interno:

1. **Receber o input**
   - O frontend envia JSON com os campos do `CreateOrderDto`.
   - O ASP.NET Core faz model binding para `CreateOrderDto createorder`. [web:182]

2. **Chamar o service**

   ```csharp
   var order = await _orderService.CreateOrderAsync(createorder);
   ```

   - O service cria a `Order`, aplica regras e grava na BD.
   - Devolve a `Order` criada. [web:170][web:163]

3. **Mapear `Order` → `OrderInfoDto`**

   ```csharp
   var orderInfoDto = new OrderInfoDto
   {
       OrderNumber         = order.OrderNumber,
       CustomerId          = order.CustomerId,
       DeliveryType        = order.DeliveryType,
       PickupAddress       = order.PickupAddress,
       HomeDeliveryAddress = order.HomeDeliveryAddress,
       WeightKg            = order.WeightKg,
       Status              = order.Status,
       CreatedAt           = order.CreatedAt,
       PlannedDeliveryDate = order.PlannedDeliveryDate
   };
   ```

   - Isto garante que o frontend recebe apenas o shape que a API decidiu expor. [web:173][web:184][web:199]

4. **Devolver resposta HTTP**

   ```csharp
   return Ok(orderInfoDto);
   ```

   - Por agora, devolve 200 OK com o `OrderInfoDto` no body.
   - Futuro “upgrade” possível:
     - Usar `CreatedAtAction` para devolver 201 Created e apontar para um futuro `GET /api/order/{id}`. [web:188][web:193]

---

## Estado atual vs próximo passo

**Já tens:**

- Entidade `Order` completa no domínio.
- `CreateOrderDto` como DTO de entrada.
- `OrderInfoDto` como DTO de saída.
- `OrderService` com `CreateOrderAsync`, a:
  - mapear DTO → Order
  - aplicar regras de negócio
  - gravar na BD
- `OrderController` com endpoint `POST /api/order` a:
  - receber `CreateOrderDto`
  - chamar o service
  - mapear `Order` → `OrderInfoDto`
  - devolver esse DTO ao frontend [web:170][web:173][web:199]

**Próximos passos naturais (para amanhã):**

- Implementar `GET /api/order/{id}`:
  - Service: método `GetOrderByIdAsync(int id)` que devolve `Order` (ou null).
  - Controller: endpoint `[HttpGet("{id}")]` que:
    - chama o service
    - mapeia `Order` → `OrderInfoDto`
    - devolve `NotFound()` se não existir, ou `Ok(orderInfoDto)` se existir. [web:188][web:193][web:199]
- Opcional: trocar o `Ok(...)` do POST por `CreatedAtAction(...)` apontando para esse `GET`.

---

## Como isto encaixa com o frontend

Para o frontend, o contrato atual do `POST /api/order` é:

- **Request body (JSON)** com os campos de `CreateOrderDto`:
  - `customerId`
  - `deliveryType`
  - `pickupAddress` / `homeDeliveryAddress`
  - `weightKg`
  - `plannedDeliveryDate`
- **Response body (JSON)** com os campos de `OrderInfoDto`:
  - `orderNumber`
  - `customerId`
  - `deliveryType`
  - `pickupAddress`
  - `homeDeliveryAddress`
  - `weightKg`
  - `status`
  - `createdAt`
  - `plannedDeliveryDate` [web:173][web:184][web:199]

Isso é suficiente para:

- Mostrar um resumo imediato da order criada no frontend.
- Mais tarde, usar `orderNumber` (ou `Id`, quando exposto) para navegar para páginas de detalhe via GET.

---

## Notas finais para ti (dev futuro)

- `CreateOrderDto` = o que **entra** do cliente.
- `Order` = o que vive no **domínio/BD**.
- `OrderInfoDto` = o que **sai** da API para o cliente.
- `OrderService` = onde as regras de negócio de Orders vivem.
- `OrderController` = o “porteiro HTTP” que coordena tudo isto. [web:173][web:181][web:184]

Quando voltares amanhã:

- Lembra-te que este pipeline `POST /api/order` já está pronto para testar com Postman/Insomnia/front-end.
- O próximo passo natural é ver isto “na vida real” no front, ou adicionar o `GET /api/order/{id}` usando o mesmo padrão de DTOs.
