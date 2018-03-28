namespace OnlineStore.DTO.SupplierModels
{
    public interface ISuppliersImportModel
    {
        string Name { get; set; }

        string Phone { get; set; }

        string AddressText { get; set; }

        string TownName { get; set; }
    }
}
