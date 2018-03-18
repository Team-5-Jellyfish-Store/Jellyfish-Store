using OnlineStore.Models.Enums;
using System.Collections.Generic;

namespace OnlineStore.Models
{
    public class User
    {
        public User()
        {
            this.Orders = new HashSet<Order>();
        }

        public int Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EMail { get; set; }

        public UserRole Role { get; set; }

        public int ReferalUserId { get; set; }
        public virtual User ReferalUser { get; set; } //navprop

        public int AddressId { get; set; }
        public virtual Address Address { get; set; } //navprop

        public virtual ICollection<Order> Orders { get; set; } //navprop
    }
}
