using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Models
{
    public class Order
    {
        public int Id { get; set; }

        public string Comment { get; set; }

        public DateTime OrderedOn { get; set; }

        public int ClientId { get; set; }
        public Client Client { get; set; } //navprop

        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; } //navprop

        public ICollection<Product> Products { get; set; } //navprop
    }
}
