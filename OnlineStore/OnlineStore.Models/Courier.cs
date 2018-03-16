using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Models
{
    public class Courier
    {
        public Courier()
        {
            this.Orders = new HashSet<Order>();
        }

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int AddressId { get; set; }
        public Address Address { get; set; }

        public ICollection<Order> Orders { get; set; } //navprop


    }
}
