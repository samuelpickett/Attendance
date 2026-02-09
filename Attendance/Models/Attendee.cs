using System.ComponentModel.DataAnnotations;

namespace Attendance.Models;
public class Attendee

{
    public int Id { get; set; }

    [Required]
    public int EventId { get; set; }

    [Required]
    public string Name { get; set; } = "";

    public bool IsCheckedIn { get; set; }

    public DateTime? CheckInTime { get; set; }
}
