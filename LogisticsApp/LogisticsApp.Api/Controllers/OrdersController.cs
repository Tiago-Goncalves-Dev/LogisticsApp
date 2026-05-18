using System;
using System.Collections.Generic;
using System.Linq;
using LogisticsApp.Api.Domain;
using LogisticsApp.Api.Dtos;
using LogisticsApp.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace LogisticsApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly OrderService _orderService;

    public OrderController(OrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost]
    public async Task<ActionResult<OrderInfoDto>> CreateOrder(CreateOrderDto createorder)
    {

        var OrderInfo = await _orderService.CreateOrderAsync(createorder);

        var orderInfoDto = new OrderInfoDto
        {
            OrderNumber = OrderInfo.OrderNumber,
            CustomerId = OrderInfo.CustomerId,
            DeliveryType = OrderInfo.DeliveryType,
            PickupAddress = OrderInfo.PickupAddress,
            HomeDeliveryAddress = OrderInfo.HomeDeliveryAddress,
            WeightKg = OrderInfo.WeightKg,
            Status = OrderInfo.Status,
            CreatedAt = OrderInfo.CreatedAt,
            PlannedDeliveryDate = OrderInfo.PlannedDeliveryDate
        };

        return Ok(orderInfoDto);
        
    }

}

