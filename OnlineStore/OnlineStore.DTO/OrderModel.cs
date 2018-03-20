using OnlineStore.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.DTO
{
    public class OrderModel
    {

       
        public int Id { get; set; }

  
        public int ProductsCount { get; set; }

        
        public string Comment { get; set; }

        public DateTime OrderedOn { get; set; }

        public Nullable<DateTime> DeliveredOn { get; set; }

       
        public virtual User User { get; set; } //navprop

        
        public virtual Courier Courier { get; set; } //navprop

    }
}

