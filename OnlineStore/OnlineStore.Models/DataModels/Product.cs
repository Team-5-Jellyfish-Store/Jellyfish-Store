using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStore.Models.DataModels
{
    public class Product
    {
        private ICollection<Order> orders;
        public Product()
        {
            this.orders = new HashSet<Order>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 4)]
        [Index(IsUnique = true)]
        public string Name { get; set; }

        [Range(0, int.MaxValue)]
        public decimal PurchasePrice { get; set; }

        [Range(0, int.MaxValue)]
        public decimal SellingPrice { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        public int CategoryId { get; set; }

        
        public virtual Category Category { get; set; } //navp

        public int SupplierId { get; set; }
        
        public virtual Supplier Supplier { get; set; }//navprop

        public virtual ICollection<Order> Orders
        {
            get { return this.orders; }
            set { this.orders = value; }
        }
    }
}
