using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Models.DataModels
{
    public class Order
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        [Range(1, 1000)]
        public int ProductsCount { get; set; }

        [MaxLength(300)]
        public string Comment { get; set; }

        [Range(typeof(decimal), "0.01", "79228162514264337593543950335")]
        public decimal Amount { get; set; }

        public DateTime OrderedOn { get; set; }

        public Nullable<DateTime> DeliveredOn { get; set; }

        [Required]
        public int UserId { get; set; }
        public virtual User User { get; set; } //navprop

        [Required]
        public int CourierId { get; set; }
        public virtual Courier Courier { get; set; } //navprop

    }
}
