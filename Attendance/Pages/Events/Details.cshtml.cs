using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Attendance.Data;
using Attendance.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Authorization;

namespace Attendance.Pages_Events
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly Attendance.Data.AppDbContext1 _context;
        private readonly UserManager<IdentityUser> _userManager;

        public DetailsModel(Attendance.Data.AppDbContext1 context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public Event Event { get; set; } = default!;
        

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = await _userManager.GetUserAsync(User);
            var _event = await _context.Event.FirstOrDefaultAsync(m => m.Id == id  && m.OwnerId == user.Id);

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
