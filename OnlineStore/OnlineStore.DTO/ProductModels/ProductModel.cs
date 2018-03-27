namespace OnlineStore.DTO.ProductModels
{
    public class ProductModel
    {
        public string Name { get; set; }

        public decimal PurchasePrice { get; set; }

        public decimal SellingPrice { get; set; }

        public int Quantity { get; set; }

        public string CategoryName { get; set; }
    }
}
