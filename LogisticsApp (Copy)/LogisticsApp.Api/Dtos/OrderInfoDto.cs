using LogisticsApp.Api.Domain;

namespace LogisticsApp.Api.Dtos;

public class OrderInfoDto
{
    public string? OrderNumber { get; set; }
    public int CustomerId { get; set; }

    public DeliveryType DeliveryType { get; set; }
    public string? PickupAddress { get; set; }

    public string? HomeDeliveryAddress { get; set; }

    public decimal WeightKg { get; set; }

    public OrderStatus Status { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? PlannedDeliveryDate { get; set; }

}