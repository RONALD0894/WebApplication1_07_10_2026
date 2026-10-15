using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1_07_10_2026.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public int ClientId { get; set; }

        public System.DateTime Date { get; set; }

        public decimal Total { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "Order must contain at least one item")]
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();
    }
}
