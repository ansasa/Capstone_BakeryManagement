using BakeryManagement.Data;
using BakeryManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
namespace BakeryManagement.Controllers;
public class CartController : Controller {
    private readonly ApplicationDbContext _db;
    public CartController(ApplicationDbContext db)=>_db=db;
    private Dictionary<int,int> GetCart()=>JsonSerializer.Deserialize<Dictionary<int,int>>(HttpContext.Session.GetString("cart")??"{}")??new();
    private void SetCart(Dictionary<int,int> cart)=>HttpContext.Session.SetString("cart",JsonSerializer.Serialize(cart));
    public async Task<IActionResult> Index(){var cart=GetCart();var products=await _db.Products.Where(p=>cart.Keys.Contains(p.Id)).ToListAsync();ViewBag.Quantities=cart;return View(products);}
    [HttpPost] public IActionResult Add(int id){var c=GetCart();c[id]=c.GetValueOrDefault(id)+1;SetCart(c);return RedirectToAction("Index");}
    [HttpPost] public IActionResult Remove(int id){var c=GetCart();c.Remove(id);SetCart(c);return RedirectToAction("Index");}
    [HttpPost] public IActionResult Update(int id,int quantity){var c=GetCart();if(quantity<=0)c.Remove(id);else c[id]=Math.Min(quantity,99);SetCart(c);return RedirectToAction("Index");}
    public IActionResult Checkout()=>GetCart().Count==0?RedirectToAction("Index"):View(new Order());
    [HttpPost,ValidateAntiForgeryToken] public async Task<IActionResult> Checkout(Order order){
        var cart=GetCart(); if(cart.Count==0)return RedirectToAction("Index");
        if(!ModelState.IsValid)return View(order);
        var products=await _db.Products.Where(p=>cart.Keys.Contains(p.Id)).ToListAsync();
        order.Items=products.Select(p=>new OrderItem{ProductId=p.Id,ProductName=p.Name,UnitPrice=p.Price,Quantity=cart[p.Id]}).ToList();
        order.Total=order.Items.Sum(i=>i.UnitPrice*i.Quantity);order.CreatedAt=DateTime.UtcNow;order.PaymentStatus="Unpaid";
        _db.Orders.Add(order);await _db.SaveChangesAsync();HttpContext.Session.Remove("cart");
        return RedirectToAction("Payment",new{id=order.Id});
    }
    public async Task<IActionResult> Payment(int id){var order=await _db.Orders.Include(o=>o.Items).FirstOrDefaultAsync(o=>o.Id==id);return order==null?NotFound():View(order);}
    [HttpPost,ValidateAntiForgeryToken] public async Task<IActionResult> DemoPay(int id){var o=await _db.Orders.FindAsync(id);if(o==null)return NotFound();o.PaymentStatus="Demo payment selected";await _db.SaveChangesAsync();return RedirectToAction("Receipt",new{id});}
    public async Task<IActionResult> Receipt(int id){var o=await _db.Orders.Include(o=>o.Items).FirstOrDefaultAsync(o=>o.Id==id);return o==null?NotFound():View(o);}
}
