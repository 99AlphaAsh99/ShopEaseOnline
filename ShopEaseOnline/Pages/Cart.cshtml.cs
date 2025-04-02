using ShopEaseOnline.Data;
using ShopEaseOnline.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace ShopEaseOnline.Pages
{
    public class CartModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CartModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<BasketItem> BasketItems { get; set; }

        public async Task OnGetAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                // Handle unauthenticated users - empty cart or redirect
                BasketItems = new List<BasketItem>();
                return;
            }

            BasketItems = await _context.BasketItems
                .Include(b => b.Product)
                .Where(b => b.UserId == userId)
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostRemoveFromCartAsync(int basketId)
        {
            var item = await _context.BasketItems
                .Include(b => b.Product)
                .FirstOrDefaultAsync(b => b.BasketID == basketId);


            if (item != null)
            {
                item.Product.StockQuantity += item.Quantity;
                _context.BasketItems.Remove(item);
                await _context.SaveChangesAsync();
                TempData["Message"] = "Item removed from cart.";
            }
            return RedirectToPage();
        }








    }







}

