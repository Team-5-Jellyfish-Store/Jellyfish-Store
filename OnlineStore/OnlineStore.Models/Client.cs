using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Models
{
    public class Client
    {
        public Client()
        {
            this.Orders = new HashSet<Order>();
        }

        public int Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        
        public int TownId { get; set; }
        public Town Town { get; set; } //navprop

        public ICollection<Order> Orders { get; set; } //navprop
    }
}
