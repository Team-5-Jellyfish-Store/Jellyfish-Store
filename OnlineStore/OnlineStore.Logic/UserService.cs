using OnlineStore.Data.Contracts;
using OnlineStore.DTO;
using System.Collections.Generic;

namespace OnlineStore.Logic
{
    public class UserService
    {
        private readonly IOnlineStoreContext context;

        public UserService(IOnlineStoreContext context)
        {
            this.context = context;
        }

        public IEnumerable<UserRegisterModel> GetAllUserRegisterData()
        {
            var users = this.context.Users;

            var userRegisterModels = new List<UserRegisterModel>();

            foreach (var user in users)
            {
                userRegisterModels.Add(new UserRegisterModel()
                {
                    Username = user.Username,
                    EMail = user.EMail,
                });
            }

            return userRegisterModels;
        }
    }
}
