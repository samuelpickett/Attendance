using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Attendance.Data;
using Attendance.Models;
using Microsoft.AspNetCore.Authorization;

namespace Attendance.Pages_Attendees
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly Attendance.Data.AppDbContext1 _context;

        public DeleteModel(Attendance.Data.AppDbContext1 context)
        {
            _context = context;
        }

        [BindProperty]
        public Attendee Attendee { get; set; } = default!;
        [BindProperty(SupportsGet = true)]
        public int EventId { get; set; }


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendee = await _context.Attendees.FirstOrDefaultAsync(m => m.Id == id);

            if (attendee is not null)
            {
                Attendee = attendee;

                return Page();
            }

            return NotFound();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendee = await _context.Attendees.FindAsync(id);
            if (attendee != null)
            {
                Attendee = attendee;
                _context.Attendees.Remove(Attendee);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index", new {EventId = EventId});
        }
    }
}
