using System.ComponentModel.DataAnnotations;
namespace BakeryManagement.Models;
public class Product {
    public int Id { get; set; }
    [Required, StringLength(100)] public string Name { get; set; } = "";
    [StringLength(500)] public string Description { get; set; } = "";
    [Range(0.01, 100000)] public decimal Price { get; set; }
    [StringLength(60)] public string Category { get; set; } = "Bakery";
    public string ImageUrl { get; set; } = "";
    public bool IsAvailable { get; set; } = true;
}
