namespace OnlineStore.DTO.CourierModels
{
    public interface ICourierImportModel
    {
        string FirstName { get; set; }
        
        string LastName { get; set; }
        
        string Phone { get; set; }
        
        string AddressText { get; set; }
        
        string TownName { get; set; }
    }
}
