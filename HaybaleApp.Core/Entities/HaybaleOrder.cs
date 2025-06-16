using System;

namespace HaybaleApp.Core.Entities;

public class HaybaleOrder
{
    public int Id { get; set; }
    public int HaybalesCount { get; set; }
    public string TypeOfHay { get; set; } = string.Empty;
    public float Weight { get; set; }
    public float? VehicleWeight { get; set; }
    public string PickupLocation { get; set; } = string.Empty;
    public string DropoffLocation { get; set; } = string.Empty;
    public string CustomerName { get; set; } = string.Empty;
    public DateTime DeliveryTime { get; set; }
    public float Price { get; set; }
    public string? Description { get; set; }
    public string Status { get; set; } = "Pending";

    public int DriverId { get; set; }
    public Driver? Driver { get; set; }
}