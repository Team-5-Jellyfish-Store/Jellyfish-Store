using System.Linq;
using AutoMapper;
using OnlineStore.Data.Contracts;
using OnlineStore.DTO;
using OnlineStore.Logic.Contracts;
using OnlineStore.Models.DataModels;
using System;

namespace OnlineStore.Logic.Services
{
    public class TownService : ITownService
    {
        private readonly IOnlineStoreContext context;

        public TownService(IOnlineStoreContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Create(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Town name is required!", nameof(name));
            }

            if (this.context.Towns.Any(x => x.Name == name))
            {
                throw new ArgumentException("Town already exists!");
            }

            var townModel = new Town { Name = name };

            this.context.Towns.Add(townModel);
            this.context.SaveChanges();
        }
    }
}
