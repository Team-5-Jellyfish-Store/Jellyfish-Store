using System.Linq;
using AutoMapper;
using OnlineStore.Data.Contracts;
using OnlineStore.DTO;
using OnlineStore.Logic.Contracts;
using OnlineStore.Models.DataModels;

namespace OnlineStore.Logic.Services
{
    public class TownService : ITownService
    {
        private readonly IOnlineStoreContext context;
        private readonly IMapper mapper;


        public TownService(IOnlineStoreContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public Town FindOrCreate(string name)
        {
            var townFound = this.context.Towns.FirstOrDefault(f => f.Name == name);

            if (townFound == null)
            {
                townFound = this.Create(name);
            }

            return townFound;
        }

        public Town Create(string name)
        {
            var townModel = new TownModel {Name = name};
            var townToAdd = this.mapper.Map<TownModel, Town>(townModel);
            this.context.Towns.Add(townToAdd);
            this.context.SaveChanges();
            var town = this.context.Towns.First(t => t.Name == name);
            return town;
        }
    }
}
