using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Models.DataModels
{
    public class Product
    {
        public Product()
        {
            this.Orders = new HashSet<Order>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 4)]
        public string Name { get; set; }

        [Range(0, int.MaxValue)]
        public decimal PurchasePrice { get; set; }

        [Range(0, int.MaxValue)]
        public decimal SellingPrice { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        public int CategoryId { get; set; }
        [Required]
        public Category Category { get; set; } //navp

        public int SupplierId { get; set; }
        [Required]
        public Supplier Supplier { get; set; }//navprop

        public virtual ICollection<Order> Orders { get; set; } //navprop
    }
}
