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
    public class DetailsModel : PageModel
    {
        private readonly Attendance.Data.AppDbContext1 _context;

        public DetailsModel(Attendance.Data.AppDbContext1 context)
        {
            _context = context;
        }

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
    }
}
