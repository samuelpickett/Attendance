using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Attendance.Data;
using Attendance.Models;

namespace Attendance.Pages_Attendees
{
    public class IndexModel : PageModel
    {
        private readonly Attendance.Data.AppDbContext2 _context;

        public IndexModel(Attendance.Data.AppDbContext2 context)
        {
            _context = context;
        }

        public IList<Attendee> Attendee { get;set; } = new List<Attendee>();
        
        [BindProperty(SupportsGet = true)]
        public int EventId { get; set; }
        public async Task OnGetAsync()
        {
            Attendee = await _context.Attendees
            .Where(a => a.EventId == EventId)
            .OrderBy(a => a.Name)
            .ToListAsync();
        }
    }
}
