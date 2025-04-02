using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using ShopEaseOnline.Models;

namespace ShopEaseOnline.Models
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }

        [Required]
        public string UserId { get; set; }  // Changed from CustomerID to UserId

        [Required]
        public DateTime OrderDate { get; set; }

        public DateTime ExpectedDeliveryDate { get; set; }

        [Required]
        [StringLength(200)]
        public string DeliveryAddress { get; set; }

        [Required]
        [StringLength(20)]
        public string PostalCode { get; set; }

        [Required]
        [StringLength(50)]
        public string City { get; set; }

        [Required]
        [StringLength(50)]
        public string Country { get; set; }

        // Navigation property for Identity User
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }  // Changed from Customer to ApplicationUser

        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
