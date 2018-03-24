using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Models.DataModels
{
    public class Order
    {
        public Order()
        {
            this.OrderProducts = new HashSet<OrderProduct>();
        }

        public int Id { get; set; }

        [MaxLength(300)]
        public string Comment { get; set; }

        public DateTime OrderedOn { get; set; }

        public Nullable<DateTime> DeliveredOn { get; set; }

        public virtual ICollection<OrderProduct> OrderProducts { get; set; }

        public int UserId { get; set; }
        [Required]
        public virtual User User { get; set; } //navprop

        public int CourierId { get; set; }
        [Required]
        public virtual Courier Courier { get; set; } //navprop

    }
}
