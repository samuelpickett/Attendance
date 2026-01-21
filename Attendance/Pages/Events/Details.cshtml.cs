using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Attendance.Data;
using Attendance.Models;

namespace Attendance.Pages_Events
{
    public class DetailsModel : PageModel
    {
        private readonly Attendance.Data.AppDbContext _context;

        public DetailsModel(Attendance.Data.AppDbContext context)
        {
            _context = context;
        }

        public Event Event { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Event = await _context.Event.FirstOrDefaultAsync(m => m.Id == id);

            if (Event is not null)
            {
                Event = Event;

                return Page();
            }

            return NotFound();
        }
    }
}
