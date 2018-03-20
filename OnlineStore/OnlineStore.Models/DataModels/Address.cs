using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Models.DataModels
{
    public class Address
    {
        private ICollection<User> users;
        private ICollection<Courier> couriers;
        private ICollection<Supplier> suppliers;

        public Address()
        {
            this.users = new HashSet<User>();
            this.couriers = new HashSet<Courier>();
            this.suppliers = new HashSet<Supplier>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 4)]
        public string AddressText { get; set; }

        [Required]
        public int TownId { get; set; }
        public Town Town { get; set; } //navprop

        public virtual ICollection<User> Users
        {
            get { return this.users; }
            set { this.users = value; } 
        } 
        public virtual ICollection<Courier> Couriers
        {
            get { return this.couriers; }
            set { this.couriers = value; }
        }
        public virtual ICollection<Supplier> Suppliers
        {
            get { return this.suppliers; }
            set { this.suppliers = value; }
        }
    }
}
