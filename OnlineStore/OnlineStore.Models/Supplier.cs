using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Models
{
    public class Supplier
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int TownId { get; set; }
        public Town Town { get; set; } //navprop

        public ICollection<Order> Orders { get; set; } //navprop


    }
}
