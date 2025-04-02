using ShopEaseOnline.Models;
using ShopEaseOnline.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace ShopEaseOnline.Pages
{
    public class OrderConfirmationModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public OrderConfirmationModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Order Order { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Shipping { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }

        public async Task<IActionResult> OnGetAsync(int orderId)
        {
            // Get currently authenticated user ID
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                // Redirect unauthenticated users to login
                return RedirectToPage("/Account/Login", new { area = "Identity", returnUrl = Url.Page("/OrderConfirmation") });
            }

            // Include security check to ensure users can only see their own orders
            Order = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.OrderID == orderId && o.UserId == userId);
            
            if (Order == null)
            {
                // Order not found or doesn't belong to current user
                return RedirectToPage("/Index");
            }

            // Calculate subtotal, shipping, tax, and total
            Subtotal = Order.OrderItems?.Sum(oi => oi.UnitPrice * oi.Quantity) ?? 0;
            Shipping = Subtotal > 0 ? 5.99m : 0;
            Tax = Subtotal * 0.08m;
            Total = Subtotal + Shipping + Tax;

            return Page();

        }
    }
}
