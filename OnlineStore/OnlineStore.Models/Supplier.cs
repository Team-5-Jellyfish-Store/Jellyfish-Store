using System.Collections.Generic;

namespace OnlineStore.Models
{
    public class Supplier
    {
        public int Id { get; set; }
        public string Phone { get; set;  }
        public ICollection<Product> Products { get; set; } //navprop
    }
}
