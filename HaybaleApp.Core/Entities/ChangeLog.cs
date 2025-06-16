using System;

namespace HaybaleApp.Core.Entities;

public class ChangeLog
{
    public int Id { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public string Username { get; set; } = string.Empty;
    public string Action { get; set; } = string.Empty; // e.g., "add", "edit", "delete"
    public string TargetEntity { get; set; } = string.Empty;
    public string FieldChanged { get; set; } = string.Empty;
    public string? OldValue { get; set; }
    public string? NewValue { get; set; }
    public string? Notes { get; set; }
}