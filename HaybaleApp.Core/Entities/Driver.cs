namespace HaybaleApp.Core.Entities;

public class Driver
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;
    public string VehicleInfo { get; set; } = string.Empty;

    public ICollection<HaybaleOrder> Orders { get; set; } = new List<HaybaleOrder>();
}