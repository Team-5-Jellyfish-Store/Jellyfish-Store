using OnlineStore.Models.DataModels;

namespace OnlineStore.Logic.Contracts
{
    public interface ITownService
    {
        //Town FindOrCreate(string name);

        void Create(string name);
    }
}
