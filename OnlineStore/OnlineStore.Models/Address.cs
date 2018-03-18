using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [Required]
        [StringLength(60, MinimumLength = 4)]
        [Index(IsUnique = true)]
        public string AddressText { get; set; }

        [Required]
        public int TownId { get; set; }
        public Town Town { get; set; } //navprop

        public virtual ICollection<User> Users { get; set; } //navprop
        public virtual ICollection<Courier> Couriers { get; set; } //navprop
        public virtual ICollection<Supplier> Suppliers { get; set; } //navprop
    }
}
