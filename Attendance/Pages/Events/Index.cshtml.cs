using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Attendance.Data;
using Attendance.Models;
using Microsoft.AspNetCore.Authorization;

namespace Attendance.Pages_Events
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly Attendance.Data.AppDbContext1 _context;
        private readonly UserManager<IdentityUser> _userManager;

        public IndexModel(Attendance.Data.AppDbContext1 context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<Event> Event { get;set; } = default!;

        public async Task OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            Event = await _context.Event.Where(e => e.OwnerId == user.Id).ToListAsync();
        }
    }
}
