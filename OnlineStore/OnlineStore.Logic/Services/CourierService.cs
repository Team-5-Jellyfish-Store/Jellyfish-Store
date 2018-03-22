using System.Collections.Generic;
using OnlineStore.Data.Contracts;
using OnlineStore.Logic.Contracts;
using OnlineStore.Models.DataModels;

namespace OnlineStore.Logic.Services
{
    public class CourierService : ICourierService
    {
        private readonly IOnlineStoreContext context;

        public CourierService(IOnlineStoreContext context)
        {
            this.context = context;
        }

        public void AddCourierRange(List<Courier> couriers)
        {
            couriers.ForEach(c => this.context.Couriers.Add(c));
            this.context.SaveChanges();
        }
    }
}