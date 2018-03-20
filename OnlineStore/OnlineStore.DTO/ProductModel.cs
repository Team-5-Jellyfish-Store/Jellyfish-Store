using OnlineStore.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.DTO
{
    public class ProductModel
    {
        public string Name { get; set; }

        public decimal PurchasePrice { get; set; }

        public decimal SellingPrice { get; set; }

        public int Quantity { get; set; }

        public string CategoryName { get; set; } //navp

      //  public virtual Supplier Supplier { get; set; }//navprop
    }
}
