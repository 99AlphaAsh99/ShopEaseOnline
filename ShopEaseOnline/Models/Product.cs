using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ShopEaseOnline.Models;

namespace EShopOnline.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public int? StockQuantity { get; set; }

        [StringLength(255)]
        public string ImageURL { get; set; }

        // Add these new properties
        [Required]
        public int CategoryID { get; set; }

        // Navigation properties
        [ForeignKey("CategoryID")]
        public virtual Category? Category { get; set; }

        public virtual ICollection<BasketItem> BasketItems { get; set; }

    }
}
