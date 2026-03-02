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

namespace Attendance.Pages_CheckIn
{
    public class IndexModel : PageModel
    {
        private readonly Attendance.Data.AppDbContext1 _context;

        public IndexModel(Attendance.Data.AppDbContext1 context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public int EventId { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Code { get; set; } = string.Empty;


        [BindProperty(SupportsGet = true)]
        public string? SearchTerm { get; set; }

        public List<Attendee> Attendees { get; set; } = new();

        public async Task OnGetAsync()
        {
            
            var eventEntity = await _context.Event
            .FirstOrDefaultAsync(e => e.CheckInCode == Code);

            EventId = eventEntity.Id;
            
            var query = _context.Attendees
                .Where(a => a.EventId == EventId);
            
            ViewData["Checked_In"] = await _context.Attendees.CountAsync(a => a.EventId == EventId && a.IsCheckedIn);

            ViewData["Total_Attendees"] = await _context.Attendees.CountAsync(a => a.EventId == EventId);

            if (!string.IsNullOrEmpty(SearchTerm))
            {
                query = query.Where(a =>
                    a.Name.Contains(SearchTerm));
            }

            Attendees = await query
                .OrderBy(a => a.Name)
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostCheckInAsync(int attendeeId)
        {
            var eventEntity = await _context.Event
            .FirstOrDefaultAsync(e => e.CheckInCode == Code);

            if (eventEntity == null)
                return NotFound();

            EventId = eventEntity.Id;

            var attendee = await _context.Attendees
                .FirstOrDefaultAsync(a =>
                a.Id == attendeeId &&
                a.EventId == EventId);

            if (attendee == null)
                return NotFound();

            attendee.IsCheckedIn = true;
            attendee.CheckInTime = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return RedirectToPage("/CheckIn/Success");
        }
    }
}
