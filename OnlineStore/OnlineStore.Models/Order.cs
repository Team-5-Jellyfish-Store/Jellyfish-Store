using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Models
{
    public class Order
    {
        public Order()
        {
            this.Products = new HashSet<Product>();
        }

        public int Id { get; set; }

        [MaxLength(300)]
        public string Comment { get; set; }

        public DateTime OrderedOn { get; set; }

        public DateTime? DeliveredOn { get; set; }

        [Required]
        public int UserId { get; set; }
        public User User { get; set; } //navprop

        [Required]
        public int CourierId { get; set; }
        public Courier Courier { get; set; } //navprop

        public virtual ICollection<Product> Products { get; set; } //navprop
    }
}
