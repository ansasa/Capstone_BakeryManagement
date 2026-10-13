using System.ComponentModel.DataAnnotations;
namespace BakeryManagement.Models;
public class Review {
    public int Id { get; set; }
    [Required, StringLength(80)] public string CustomerName { get; set; } = "";
    [Required, Range(1,5)] public int Rating { get; set; } = 5;
    [Required, StringLength(1000)] public string Comment { get; set; } = "";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
public class ContactMessage {
    public int Id { get; set; }
    [Required] public string Name { get; set; } = "";
    [Required, EmailAddress] public string Email { get; set; } = "";
    [Required, StringLength(2000)] public string Message { get; set; } = "";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
