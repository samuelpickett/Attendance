using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Attendance.Data;
using Attendance.Models;
using System.Security.Cryptography.X509Certificates;
using System.Reflection.Metadata;

namespace Attendance.Pages_Events
{
    public class DetailsModel : PageModel
    {
        private readonly Attendance.Data.AppDbContext1 _context;

        public DetailsModel(Attendance.Data.AppDbContext1 context)
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

            var _event = await _context.Event.FirstOrDefaultAsync(m => m.Id == id);

            if (_event is not null)
            {
                Event = _event;

                return Page();
            }

            return NotFound();
        }

        public IActionResult OnPostSelectId(int id)
        {
            TempData["SavedId"] = id;

            return RedirectToPage("/Attendees/Index");
        }

        }
}
