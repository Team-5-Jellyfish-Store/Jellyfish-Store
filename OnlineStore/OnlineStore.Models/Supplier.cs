using System.Collections.Generic;

namespace OnlineStore.Models
{
    public class Supplier
    {
        public Supplier()
        {
            this.Products = new HashSet<Product>();
        }

        public int Id { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Phone { get; set;  }

        public int AddressId { get; set; }
        public Address Address { get; set; }

        public virtual ICollection<Product> Products { get; set; } //navprop
    }
}
