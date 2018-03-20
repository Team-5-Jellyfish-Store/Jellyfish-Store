using OnlineStore.DTO;
using OnlineStore.Models.DataModels;

namespace OnlineStore.Logic.Contracts
{
    public interface IAddressService
    {
        /*AddressModel*/ Address GetAddress(string addressText, string townName);
    }
}
