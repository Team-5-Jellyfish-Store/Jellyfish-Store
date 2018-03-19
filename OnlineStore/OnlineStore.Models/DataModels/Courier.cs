﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Models.DataModels
{
    public class Courier
    {
        public Courier()
        {
            this.Orders = new HashSet<Order>();
        }

        public int Id { get; set; }

        [Required]
        [MinLength(2,ErrorMessage = "First name should be atleast 2 characters")]
        [MaxLength(20,ErrorMessage = "First name should be shorter than 20 characters")]
        public string FirstName { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Last name should be atleast 2 characters")]
        [MaxLength(20, ErrorMessage = "Last name should be shorter than 20 characters")]
        public string LastName { get; set; }

        [Required]
        [StringLength(13, MinimumLength =6,ErrorMessage = "Please enter correct Phone")]
        public string Phone { get; set; }

        [Required]
        public int AddressId { get; set; }

        public virtual Address Address { get; set; } //navprop

        public virtual ICollection<Order> Orders { get; set; } //navprop


    }
}