using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogisticsApp.Api.Domain;

public class Order
{
    [Required] public int Id { get; set; }

    [Required] public string? OrderNumber { get; set; }

    /* [ForeignKey] */
    public int CustomerId { get; set; }

    [Required]public DeliveryType DeliveryType { get; set; }

    public string? PickupAddress { get; set; }

    public string? HomeDeliveryAddress { get; set; }
    public decimal WeightKg { get; set; }

    public OrderStatus Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime LastEditedAt { get; set; }

    public DateTime? PlannedDeliveryDate { get; set; }


}

public enum OrderStatus
{
    Created,
    OnGoing,
    Finished
}

public enum DeliveryType
{
    Pickup,
    HomeDelivery
}