using BakeryManagement.Data;
using BakeryManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace BakeryManagement.Controllers;
[Authorize(Roles="Admin")]
public class AdminController : Controller {
    private readonly ApplicationDbContext _db;
    public AdminController(ApplicationDbContext db) => _db=db;
    public async Task<IActionResult> Index() => View(await _db.Products.OrderBy(p=>p.Name).ToListAsync());
    public IActionResult Create() => View("Edit", new Product());
    [HttpPost, ValidateAntiForgeryToken] public async Task<IActionResult> Save(Product product) {
        if (!ModelState.IsValid) return View("Edit", product);
        if(product.Id==0) _db.Products.Add(product); else _db.Products.Update(product);
        await _db.SaveChangesAsync(); return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Edit(int id) { var p=await _db.Products.FindAsync(id); return p==null?NotFound():View(p); }
    [HttpPost, ValidateAntiForgeryToken] public async Task<IActionResult> Delete(int id) { var p=await _db.Products.FindAsync(id); if(p!=null){_db.Products.Remove(p);await _db.SaveChangesAsync();} return RedirectToAction(nameof(Index)); }
    public async Task<IActionResult> Orders() => View(await _db.Orders.Include(o=>o.Items).OrderByDescending(o=>o.CreatedAt).ToListAsync());
}
