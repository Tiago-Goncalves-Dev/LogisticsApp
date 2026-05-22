using LogisticsApp.Api.Infrastructure;
using Microsoft.EntityFrameworkCore;
using LogisticsApp.Api.Controllers;
using LogisticsApp.Api.Services;
using Microsoft.AspNetCore.Authorization.Policy;
using System.Net;


var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// 1) Registar serviços AQUI
builder.Services.AddDbContext<LogisticsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));



// Se tiveres controllers/services, também aqui:
builder.Services.AddControllers();
builder.Services.AddScoped<OrderService>();

var app = builder.Build();

// 2) Só agora construir a app


// 3) Pipeline HTTP
app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

// Rota de teste
app.MapGet("/", () => "API Rodando!");
app.MapControllers();

// 4) Correr app
app.Run();