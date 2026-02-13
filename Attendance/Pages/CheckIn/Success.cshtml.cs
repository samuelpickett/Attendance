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
    public class SuccessModel : PageModel
    {
        private readonly AppDbContext2 _context;

        public SuccessModel(AppDbContext2 context)
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
            
        }

        
    }
}
