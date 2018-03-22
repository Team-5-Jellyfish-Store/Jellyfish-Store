namespace OnlineStore.DTO.ExternalImportDto
{
    public class ProductJsonModel
    {
        public string Name { get; set; }

        public decimal PurchasePrice { get; set; }

        public int Quantity { get; set; }

        public string Category { get; set; }

        public string Supplier { get; set; }
    }
}
