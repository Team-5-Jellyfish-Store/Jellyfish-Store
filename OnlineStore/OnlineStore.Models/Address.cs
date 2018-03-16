using System.Collections.Generic;

namespace OnlineStore.Models
{
    public class Address
    {
        public Address()
        {
            this.Users = new HashSet<User>();
            this.Couriers = new HashSet<Courier>();
            this.Suppliers = new HashSet<Supplier>();
        }

        public int Id { get; set; }

        public string AddressText { get; set; }

        public int TownId { get; set; }
        public Town Town { get; set; } //navprop

        public ICollection<User> Users { get; set; } //navprop
        public ICollection<Courier> Couriers { get; set; } //navprop
        public ICollection<Supplier> Suppliers { get; set; } //navprop
    }
}
