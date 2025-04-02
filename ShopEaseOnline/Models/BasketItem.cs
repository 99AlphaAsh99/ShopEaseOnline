using EShopOnline.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ShopEaseOnline.Models
{
    public class BasketItem
    {
        [Key]
        public int BasketID { get; set; }

        [Required]
        public string UserId { get; set; }  // Changed from CustomerID to UserId

        [Required]
        public int ProductID { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public DateTime DateAdded { get; set; }

        // Navigation properties
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }  // Changed from Customer to ApplicationUser

        [ForeignKey("ProductID")]
        public virtual Product Product { get; set; }
    }
}
