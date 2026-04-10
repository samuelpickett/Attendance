using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Attendance.Models;
using System.Dynamic;

namespace Attendance.Data;

public class AppDbContext1 : IdentityDbContext<IdentityUser>
{
    public AppDbContext1(DbContextOptions<AppDbContext1> options)
        : base(options) { }

    public DbSet<Attendance.Models.Event> Event {get; set;} = default!;
    public DbSet<Attendance.Models.Attendee> Attendees => Set<Attendee>();

}


