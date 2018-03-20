using OnlineStore.Models.DataModels;

namespace OnlineStore.DTO
{
    public class ProductModel
    {

        public string Name { get; set; }

        public decimal PurchasePrice { get; set; }

        public decimal SellingPrice { get; set; }

        public int Quantity { get; set; }

        public Category Category { get; set; }

        public Supplier Supplier { get; set; }
    }
}
