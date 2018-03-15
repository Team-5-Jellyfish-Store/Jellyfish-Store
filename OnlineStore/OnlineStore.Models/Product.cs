using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Models
{
    public class Product
    {
        public Product()
        {
            this.Orders= new HashSet<Order>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; } //navprop

        public ICollection<Order> Orders { get; set; } //navprop

    }
}
