using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Attendance.Models;
public class Event
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = "";
    [Required]
    public DateTime Date { get; set; }
    public string Location { get; set; } = "";
    public string OwnerId { get; set; } = string.Empty;
    public IdentityUser? owner { get; set; }
    public string CheckInCode { get; set; } = Guid.NewGuid().ToString("N");
}
