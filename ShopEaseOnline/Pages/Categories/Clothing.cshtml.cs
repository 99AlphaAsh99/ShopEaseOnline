using ShopEaseOnline.Data;
using ShopEaseOnline.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace ShopEaseOnline.Pages.Categories
{
    public class ClothingModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ClothingModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Product> Products { get; set; }

        public async Task OnGetAsync()
        {
            Products = await _context.Products
                .Where(p => p.CategoryID == 1)
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostAddToCartAsync(int productId)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.ProductID == productId);

            if (product == null || product.StockQuantity <= 0)
            {
                TempData["Message"] = "This product is out of stock.";
                return RedirectToPage("/Index");
            }



            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                // User is not authenticated, redirect to login
                return RedirectToPage("/Account/Login", new { area = "Identity", returnUrl = Url.Page("/Categories/Clothing") });
            }

            var existingItem = await _context.BasketItems
                .FirstOrDefaultAsync(b => b.ProductID == productId && b.UserId == userId);

            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                var basketItem = new BasketItem
                {
                    ProductID = productId,
                    UserId = userId,
                    Quantity = 1,
                    //DataAdded = DateTime.Now
                };
                _context.BasketItems.Add(basketItem);

            }

            product.StockQuantity--;

            await _context.SaveChangesAsync();

            TempData["Message"] = "Product added to cart successfully.";
            return RedirectToPage("/Cart");








        }



    }
}
