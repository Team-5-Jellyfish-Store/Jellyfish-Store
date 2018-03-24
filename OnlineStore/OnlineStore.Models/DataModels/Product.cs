using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStore.Models.DataModels
{
    public class Product
    {
        public Product()
        {
            this.OrderProducts = new HashSet<OrderProduct>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 4)]
        [Index(IsUnique = true)]
        public string Name { get; set; }

        [Range(typeof(decimal), "0.01", "79228162514264337593543950335")]
        public decimal PurchasePrice { get; set; }

        [Range(typeof(decimal), "0.01", "79228162514264337593543950335")]
        public decimal SellingPrice { get; set; }

        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }


        public int CategoryId { get; set; }

        public virtual Category Category { get; set; } //navp

        public int SupplierId { get; set; }

        public virtual Supplier Supplier { get; set; }//navprop

        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
