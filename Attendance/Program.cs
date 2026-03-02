using Microsoft.EntityFrameworkCore;
using Attendance.Data;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<AppDbContext1>(options => options.UseSqlite("Data Source=eventTracker.db"));

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var db1 = scope.ServiceProvider.GetRequiredService<AppDbContext1>();
    db1.Database.EnsureCreated();
}


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages().WithStaticAssets();

app.Run();
