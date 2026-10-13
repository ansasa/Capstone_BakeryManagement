using BakeryManagement.Data;
using BakeryManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace BakeryManagement.Controllers;
public class CommunityController : Controller {
 private readonly ApplicationDbContext _db; public CommunityController(ApplicationDbContext db)=>_db=db;
 public async Task<IActionResult> Reviews()=>View(await _db.Reviews.OrderByDescending(r=>r.CreatedAt).ToListAsync());
 [HttpPost,ValidateAntiForgeryToken] public async Task<IActionResult> AddReview(Review r){if(ModelState.IsValid){_db.Reviews.Add(r);await _db.SaveChangesAsync();}return RedirectToAction(nameof(Reviews));}
 public IActionResult Contact()=>View(new ContactMessage());
 [HttpPost,ValidateAntiForgeryToken] public async Task<IActionResult> Contact(ContactMessage m){if(ModelState.IsValid){_db.ContactMessages.Add(m);await _db.SaveChangesAsync();ViewBag.Success="Thanks! Your message has been received.";return View(new ContactMessage());}return View(m);}
}
