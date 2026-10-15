using System.ComponentModel.DataAnnotations;

namespace WebApplication1_07_10_2026.Models
{
    public class OrderItem
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 1)]
        public string? Name { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public int Quantity { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "UnitPrice must be greater than 0")]
        public decimal UnitPrice { get; set; }

        public decimal TotalPrice => Quantity * UnitPrice;
    }
}
