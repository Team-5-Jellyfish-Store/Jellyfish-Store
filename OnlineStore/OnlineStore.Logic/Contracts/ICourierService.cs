using OnlineStore.DTO;

namespace OnlineStore.Logic.Contracts
{
    public interface ICourierService
    {
        void AddCourierFromDto(CourierImportDto courier);
    }
}
