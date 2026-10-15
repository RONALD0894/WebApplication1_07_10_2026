using Microsoft.AspNetCore.Mvc;
using WebApplication1_07_10_2026.Models;

namespace WebApplication1_07_10_2026.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private static readonly List<Order> _orders = new()
        {
            new Order {
                Id = 1,
                ClientId = 10,
                Date = System.DateTime.UtcNow.AddDays(-5),
                Items = new List<OrderItem> {
                    new OrderItem{ ProductId = 1, Name = "Laptop", Quantity = 1, UnitPrice = 899.99m },
                    new OrderItem{ ProductId = 2, Name = "Mouse", Quantity = 1, UnitPrice = 29.99m }
                }
            },
            new Order {
                Id = 2,
                ClientId = 11,
                Date = System.DateTime.UtcNow.AddDays(-4),
                Items = new List<OrderItem> {
                    new OrderItem{ ProductId = 3, Name = "Monitor", Quantity = 1, UnitPrice = 199.50m },
                    new OrderItem{ ProductId = 4, Name = "Keyboard", Quantity = 1, UnitPrice = 49.99m }
                }
            },
            new Order {
                Id = 3,
                ClientId = 12,
                Date = System.DateTime.UtcNow.AddDays(-3),
                Items = new List<OrderItem> {
                    new OrderItem{ ProductId = 5, Name = "Phone", Quantity = 1, UnitPrice = 599.00m },
                    new OrderItem{ ProductId = 6, Name = "Case", Quantity = 2, UnitPrice = 19.99m }
                }
            },
            new Order {
                Id = 4,
                ClientId = 13,
                Date = System.DateTime.UtcNow.AddDays(-2),
                Items = new List<OrderItem> {
                    new OrderItem{ ProductId = 7, Name = "Headphones", Quantity = 1, UnitPrice = 129.90m },
                    new OrderItem{ ProductId = 8, Name = "USB Cable", Quantity = 3, UnitPrice = 4.99m }
                }
            },
            new Order {
                Id = 5,
                ClientId = 14,
                Date = System.DateTime.UtcNow.AddDays(-1),
                Items = new List<OrderItem> {
                    new OrderItem{ ProductId = 9, Name = "SSD", Quantity = 1, UnitPrice = 149.99m },
                    new OrderItem{ ProductId = 10, Name = "SATA Cable", Quantity = 2, UnitPrice = 3.50m }
                }
            }
        };

        // Calcular totales al recuperar
        private void EnsureTotals()
        {
            foreach (var o in _orders)
            {
                o.Total = o.Items.Sum(i => i.TotalPrice);
            }
        }

        [HttpGet(Name = "GetOrders")]
        public ActionResult<IEnumerable<Order>> Get()
        {
            EnsureTotals();
            return Ok(_orders);
        }

        [HttpGet("{id}", Name = "GetOrderById")]
        public ActionResult<Order> GetById(int id)
        {
            var order = _orders.FirstOrDefault(o => o.Id == id);
            if (order == null) return NotFound();
            order.Total = order.Items.Sum(i => i.TotalPrice);
            return Ok(order);
        }

        [HttpPost(Name = "CreateOrder")]
        [ProducesResponseType(typeof(Order), 201)]
        [ProducesResponseType(400)]
        public ActionResult<Order> Create([FromBody] Order order)
        {
            if (order == null) return BadRequest();
            // ModelState validation is automatic with [ApiController], but check to return readable errors
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            // Calcular total y asignar id
            order.Total = order.Items.Sum(i => i.TotalPrice);
            var nextId = _orders.Any() ? _orders.Max(o => o.Id) + 1 : 1;
            order.Id = nextId;
            order.Date = order.Date == default ? System.DateTime.UtcNow : order.Date;
            _orders.Add(order);
            return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);
        }

        [HttpDelete("{id}", Name = "DeleteOrder")]
        public IActionResult Delete(int id)
        {
            var order = _orders.FirstOrDefault(o => o.Id == id);
            if (order == null) return NotFound();
            _orders.Remove(order);
            return NoContent();
        }
    }
}
