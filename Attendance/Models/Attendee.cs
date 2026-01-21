using System.ComponentModel.DataAnnotations;

namespace Attendance.Models;
public class Attendee

{
    public int Id { get; set; }
    public int EventId { get; set; }
    public Event Event { get; set; } = null!;
    public string Name { get; set; } = "";
    public string? Email { get; set; }
    public bool IsCheckedIn { get; set; }
    public DateTime? CheckInTime { get; set; }
}
