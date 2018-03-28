namespace OnlineStore.DTO.ProductModels.Contracts
{
    public interface IProductModel
    {
        string Name { get; set; }

        decimal PurchasePrice { get; set; }

        decimal SellingPrice { get; set; }

        int Quantity { get; set; }

        string CategoryName { get; set; }
    }
}
