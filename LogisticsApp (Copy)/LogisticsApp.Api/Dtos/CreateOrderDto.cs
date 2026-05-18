using System.ComponentModel.DataAnnotations;
using LogisticsApp.Api.Domain;

namespace LogisticsApp.Api.Dtos;

public class CreateOrderDto
{
    public int CustomerId { get; set; }
    [Required] public DeliveryType DeliveryType { get; set; }

    public decimal WeightKg { get; set; }
    public DateTime? PlannedDeliveryDate { get; set; }

    public string? PickupAddress { get; set; }

    public string? HomeDeliveryAddress { get; set; }

}