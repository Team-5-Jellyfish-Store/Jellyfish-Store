using OnlineStore.Models.DataModels;

namespace OnlineStore.DTO
{
    public class ProductImportModel
    {
        public string Name { get; set; }

        public decimal PurchasePrice { get; set; }

        public int Quantity { get; set; }

        public Category CategoryName { get; set; }

        public SupplierImportModel Supplier { get; set; }
    }
}
