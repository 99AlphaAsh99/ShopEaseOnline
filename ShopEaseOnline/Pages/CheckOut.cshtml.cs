using ShopEaseOnline.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopEaseOnline.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace ShopEaseOnline.Pages
{
    public class CheckOutModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CheckOutModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public CheckoutViewModel CheckoutData { get; set; }

        public decimal CartTotal { get; set; }
        public IList<BasketItem> BasketItems { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                // Redirect unauthenticated users to login
                return RedirectToPage("/Account/Login", new { area = "Identity", returnUrl = Url.Page("/CheckOut") });
            }

            BasketItems = await _context.BasketItems
                .Include(b => b.Product)
                .Where(b => b.UserId == userId)
                .ToListAsync();

            CartTotal = BasketItems.Sum(b => b.Product.Price * b.Quantity);

            // Get user info directly from the database instead of using UserManager
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == userId);

            // Initialize with empty values in case user info can't be found
            CheckoutData = new CheckoutViewModel
            {
                Name = User.Identity.Name ?? "",  
                DeliveryAddress = "",  
                PostalCode = "",       
                City = "",             
                Country = ""
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Get currently authenticated user ID
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToPage("/Account/Login", new { area = "Identity", returnUrl = Url.Page("/CheckOut") });
            }

            BasketItems = await _context.BasketItems
                .Include(b => b.Product)
                .Where(b => b.UserId == userId)
                .ToListAsync();

            // Check if the basket is empty
            if (BasketItems == null || !BasketItems.Any())
            {
                ModelState.AddModelError("", "Your cart is empty. Please add items to your cart before checking out.");
                return Page();
            }

            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.Now,
                ExpectedDeliveryDate = DateTime.Now.AddDays(7),
                DeliveryAddress = CheckoutData.DeliveryAddress,
                PostalCode = CheckoutData.PostalCode,
                City = CheckoutData.City,
                Country = CheckoutData.Country,
                OrderItems = new List<OrderItem>()
            };

            foreach (var basketItem in BasketItems)
            {
                order.OrderItems.Add(new OrderItem
                {
                    ProductID = basketItem.ProductID,
                    Quantity = basketItem.Quantity,
                    UnitPrice = basketItem.Product.Price // Store the price at purchase time
                });
            }

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            // Remove all basket items for this user
            var basketItems = _context.BasketItems.Where(b => b.UserId == userId);
            _context.BasketItems.RemoveRange(basketItems);
            await _context.SaveChangesAsync();

            return RedirectToPage("OrderConfirmation", new { orderId = order.OrderID });
        }
    }
}
