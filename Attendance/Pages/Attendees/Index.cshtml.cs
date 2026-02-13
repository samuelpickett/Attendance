using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Attendance.Data;
using Attendance.Models;

namespace Attendance.Pages_Attendees
{
    public class IndexModel : PageModel
    {
        private readonly Attendance.Data.AppDbContext2 _context;
        private readonly Attendance.Data.AppDbContext1 _context1;

        public IndexModel(Attendance.Data.AppDbContext2 context, Attendance.Data.AppDbContext1 context1)
        {
            _context = context;
            _context1 = context1;
        }

        public IList<Attendee> Attendee { get;set; } = new List<Attendee>();
        
        [BindProperty(SupportsGet = true)]
        public int EventId { get; set; }

        [BindProperty]
        public IFormFile? CsvFile { get; set; }

        public async Task OnGetAsync()
        {
            Attendee = await _context.Attendees
            .Where(a => a.EventId == EventId)
            .OrderBy(a => a.Id)
            .ToListAsync();
        }

        public async Task<IActionResult> OnPostUploadAsync()
        {
            if (CsvFile == null || CsvFile.Length == 0)
            {
                ModelState.AddModelError("", "Please select a valid CSV file.");
                return Page();
            }

            using var reader = new StreamReader(CsvFile.OpenReadStream());

            var line = await reader.ReadLineAsync();
            while ((line = await reader.ReadLineAsync()) is not null)
                {

                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    var values = line.Split(',');

                    if (values.Length < 1)
                        continue;

                    var attendee = new Attendee
                    {
                        Name = values[0].Trim(),
                        EventId = EventId,
                        IsCheckedIn = false,
                        CheckInTime = null
                    };
                
                    var eventExists = await _context1.Event.AnyAsync(e => e.Id == EventId);
                    if (!eventExists)
                    {
                        ViewData["ErrorMessage"] = $"You are attempting to upload to an event that doesn't exist. Please use the View Attendees button in the Details page of the event. ";
                        return Page();
                    }

                    _context.Attendees.Add(attendee);
                }

                await _context.SaveChangesAsync();

                return RedirectToPage("./Index", new { EventId });
            }

    }
}
