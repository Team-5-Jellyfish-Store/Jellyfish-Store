using OnlineStore.Models.DataModels;

namespace OnlineStore.Logic.Contracts
{
    public interface IAddressService
    {
        /*AddressModel*/ Address GetAddress(string addressText, string townName);
        Address FindOrCreate(string address, string town);
        Address Create(string address, Town town);
    }
}
