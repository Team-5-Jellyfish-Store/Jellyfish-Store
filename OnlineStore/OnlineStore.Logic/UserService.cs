using OnlineStore.Data.Contracts;

namespace OnlineStore.Logic
{
    public class UserService
    {
        private readonly IOnlineStoreContext context;

        public UserService(IOnlineStoreContext context)
        {
            this.context = context;
        }

        public void GetAllUsers()
        {

        }
    }
}
