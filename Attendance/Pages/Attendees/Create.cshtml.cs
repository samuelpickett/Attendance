using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Attendance.Data;
using Attendance.Models;

namespace Attendance.Pages_Attendees
{
    public class CreateModel : PageModel
    {
        private readonly Attendance.Data.AppDbContext1 _context;

        public CreateModel(Attendance.Data.AppDbContext1 context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public int EventId { get; set; }

        [BindProperty]
        public Attendee Attendee { get; set; } = default!;
        public IActionResult OnGet()
        {
            Attendee = new Attendee { EventId = EventId };
            return Page();
        }


        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Attendee.EventId = EventId;
            _context.Attendees.Add(Attendee);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index", new {EventId});
        }
    }
}
