using BakeryManagement.Data;
using BakeryManagement.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(o => o.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDefaultIdentity<IdentityUser>(o => { o.SignIn.RequireConfirmedAccount = false; o.Password.RequireNonAlphanumeric = false; })
    .AddRoles<IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(o => { o.Cookie.HttpOnly = true; o.IdleTimeout = TimeSpan.FromHours(2); });
var app = builder.Build();
using (var scope = app.Services.CreateScope()) {
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.EnsureCreated();
    if (!db.Products.Any()) {
        db.Products.AddRange(
            new Product { Name="Chocolate Cake", Description="Rich chocolate sponge cake", Price=650, Category="Cakes", ImageUrl="https://images.unsplash.com/photo-1578985545062-69928b1d9587?w=600" },
            new Product { Name="Butter Croissant", Description="Flaky, buttery and freshly baked", Price=90, Category="Pastries", ImageUrl="https://images.unsplash.com/photo-1555507036-ab1f4038808a?w=600" },
            new Product { Name="Blueberry Muffin", Description="Soft muffin with blueberries", Price=120, Category="Muffins", ImageUrl="https://images.unsplash.com/photo-1607958996333-41aef7caefaa?w=600" },
            new Product { Name="Sourdough Loaf", Description="Slow-fermented artisan bread", Price=180, Category="Bread", ImageUrl="https://images.unsplash.com/photo-1585478259715-876acc5be8eb?w=600" });
        db.SaveChanges();
    }
    var roles = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    if (!await roles.RoleExistsAsync("Admin")) await roles.CreateAsync(new IdentityRole("Admin"));
    if (!await roles.RoleExistsAsync("Customer")) await roles.CreateAsync(new IdentityRole("Customer"));
}
if (!app.Environment.IsDevelopment()) { app.UseExceptionHandler("/Home/Error"); app.UseHsts(); }
app.UseHttpsRedirection(); app.UseStaticFiles(); app.UseRouting(); app.UseSession(); app.UseAuthentication(); app.UseAuthorization();
app.MapControllerRoute(name:"default", pattern:"{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.Run();
