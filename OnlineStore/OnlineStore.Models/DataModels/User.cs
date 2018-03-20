using OnlineStore.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStore.Models.DataModels
{
    public class User
    {
        private ICollection<Order> orders;
        public User()
        {
            this.orders = new HashSet<Order>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 5)]
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
        public string Phone { get; set; }

        [Required]
        public UserRole Role { get; set; }

        public Nullable<int> ReferalUserId { get; set; }
        public virtual User ReferalUser { get; set; } //navprop

        public int AddressId { get; set; }
        [Required]
        public virtual Address Address { get; set; } //navprop

        public virtual ICollection<Order> Orders
        {
            get { return this.orders; }
            set { this.orders = value; }
        }
    }
}
