﻿using System.Collections.Generic;

namespace OnlineStore.Models
{
    public class Supplier
    {
        public int Id { get; set; }

        public string Phone { get; set;  }

        public int AddressId { get; set; }
        public Address Address { get; set; }

        public ICollection<Product> Products { get; set; } //navprop
    }
}
