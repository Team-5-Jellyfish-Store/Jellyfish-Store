using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineStore.DTO.OrderModels
{
    public class OrderMakeModel
    {
        public OrderMakeModel()
        {
            this.ProductNameAndCounts = new Dictionary<string, int>();
        }

        [Required]
        public IDictionary<string, int> ProductNameAndCounts { get; set; }

        [MaxLength(300)]
        public string Comment { get; set; }

        public DateTime OrderedOn { get; set; }

        [Required(ErrorMessage = "User is required!")]
        public string Username { get; set; }
    }
}