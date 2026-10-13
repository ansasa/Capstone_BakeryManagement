using System.ComponentModel.DataAnnotations;
namespace BakeryManagement.Models;
public class Order {
    public int Id { get; set; }
    [Required] public string CustomerName { get; set; } = "";
    [Required, EmailAddress] public string Email { get; set; } = "";
    [Required] public string DeliveryAddress { get; set; } = "";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public decimal Total { get; set; }
    public string Status { get; set; } = "Pending";
    public string PaymentStatus { get; set; } = "Unpaid";
    public List<OrderItem> Items { get; set; } = new();
}
public class OrderItem {
    public int Id { get; set; }
    public int OrderId { get; set; }
    public Order? Order { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; } = "";
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
}
