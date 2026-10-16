// CI/CD pipeline test - verify GitHub Actions on pull requests.

using BakeryManagement.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace BakeryManagement.Controllers;
public class HomeController : Controller {
    private readonly ApplicationDbContext _db;
    public HomeController(ApplicationDbContext db) => _db=db;
    public async Task<IActionResult> Index() => View(await _db.Products.Where(p=>p.IsAvailable).ToListAsync());
    public IActionResult Privacy() => View();
}
