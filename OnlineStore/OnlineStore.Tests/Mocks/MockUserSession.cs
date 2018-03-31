using OnlineStore.Core.Providers.Providers;
using OnlineStore.DTO.UserModels;
using OnlineStore.DTO.UserModels.Contracts;

namespace OnlineStore.Tests.Mocks
{
    internal class MockUserSession : UserSession
    {
        public IUserLoginModel ExposeLoggedUser() => this.LoggedUser;

        protected override IUserLoginModel LoggedUser
        {
            get
            {
                if (base.LoggedUser == null)
                {
                    return null;
                }

                return new UserLoginModel()
                {
                    Username = base.LoggedUser.Username,
                    Password = base.LoggedUser.Password,
                    Role = base.LoggedUser.Role
                };
            }
        }
    }
}
