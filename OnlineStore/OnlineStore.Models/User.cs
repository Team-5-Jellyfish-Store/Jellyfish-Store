using OnlineStore.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Models
{
    public class User
    {
        public User()
        {
            this.Orders = new HashSet<Order>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 8)]
        [Index(IsUnique = true)]
        public string Username { get; set; }

        [Required]
        [StringLength(64, MinimumLength = 64)]
        public string Password { get; set; }

        [StringLength(30, MinimumLength = 2)]
        public string FirstName { get; set; }

        [StringLength(30, MinimumLength = 2)]
        public string LastName { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        [Index(IsUnique = true)]
        public string EMail { get; set; }

        [StringLength(15, MinimumLength = 4)]
        [Index(IsUnique = true)]
        public string Phone { get; set; }

        [Required]
        public UserRole Role { get; set; }

        public Nullable<int> ReferalUserId { get; set; }
        public User ReferalUser { get; set; } //navprop

        [Required]
        public int AddressId { get; set; }
        public Address Address { get; set; } //navprop

        public virtual ICollection<Order> Orders { get; set; } //navprop
    }
}
