namespace HaybaleApp.Core.Entities;

public class Customer
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;

    public string ContactInfo { get; set; } = string.Empty;
    public string? CompanyName { get; set; }
    public string? Address { get; set; }
    public ICollection<HaybaleOrder> Orders { get; set; } = new List<HaybaleOrder>();
}