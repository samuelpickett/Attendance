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

namespace Attendance.Pages_Events
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var _event = await _context.Event.FindAsync(id);
            if (_event != null)
            {
                Event = _event;
                _context.Event.Remove(Event);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
