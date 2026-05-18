

using LogisticsApp.Api.Domain;
using LogisticsApp.Api.Infrastructure;
using LogisticsApp.Api.Dtos;
using System.Threading.Tasks;
namespace LogisticsApp.Api.Services;

public class OrderService
{
    private readonly LogisticsDbContext _context;

    public OrderService(LogisticsDbContext context)
    {
        _context = context;
    }

    public async Task<Order> CreateOrderAsync(CreateOrderDto dto)
    {
        var order = new Order()
        {
            OrderNumber = Guid.NewGuid().ToString(),
            CustomerId = dto.CustomerId,
            WeightKg = dto.WeightKg,
            PlannedDeliveryDate = dto.PlannedDeliveryDate,
            Status = OrderStatus.Created,
            CreatedAt = DateTime.Now,
            LastEditedAt = DateTime.Now,
            DeliveryType = dto.DeliveryType,

        };
        if (dto.DeliveryType == DeliveryType.Pickup)
        {
            order.PickupAddress = dto.PickupAddress;

        }
        else
        {
            order.HomeDeliveryAddress = dto.HomeDeliveryAddress;
        }

        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();
        return order;
    }


}