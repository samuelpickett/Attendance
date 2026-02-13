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
        private readonly AppDbContext2 _context;

        public IndexModel(AppDbContext2 context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public int EventId { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SearchTerm { get; set; }

        public List<Attendee> Attendees { get; set; } = new();

        public async Task OnGetAsync()
        {
            var query = _context.Attendees
                .Where(a => a.EventId == EventId);

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
            var attendee = await _context.Attendees
                .FirstOrDefaultAsync(a =>
                    a.Id == attendeeId &&
                    a.EventId == EventId);

            if (attendee == null)
                return NotFound();

            attendee.IsCheckedIn = true;
            attendee.CheckInTime = DateTime.Now;

            await _context.SaveChangesAsync();

            return RedirectToPage("./Success");
        }
    }
}
