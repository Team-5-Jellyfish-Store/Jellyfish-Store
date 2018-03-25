using OnlineStore.Models.DataModels;

namespace OnlineStore.Logic.Contracts
{
    public interface IAddressService
    {
        void Create(string address, string town);
    }
}
