using System;
using System.Collections.Generic;

namespace OnlineStore.Models
{
    public class Order
    {
        public Order()
        {
            this.Products = new HashSet<Product>();
        }

        public int Id { get; set; }

        public string Comment { get; set; }

        public DateTime OrderedOn { get; set; }

        public DateTime? DeliveredOn { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } //navprop

        public int CourierId { get; set; }
        public Courier Courier { get; set; } //navprop

        public ICollection<Product> Products { get; set; } //navprop
    }
}
