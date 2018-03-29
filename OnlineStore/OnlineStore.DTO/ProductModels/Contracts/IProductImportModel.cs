using AutoMapper;

namespace OnlineStore.DTO.ProductModels.Contracts
{
    public interface IProductImportModel
    {
        string Name { get; set; }

        decimal PurchasePrice { get; set; }

        int Quantity { get; set; }

        string Category { get; set; }

        string Supplier { get; set; }
    }
}
