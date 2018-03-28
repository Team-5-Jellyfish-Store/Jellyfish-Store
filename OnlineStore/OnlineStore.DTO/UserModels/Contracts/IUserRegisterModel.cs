using OnlineStore.Models.Enums;

namespace OnlineStore.DTO.UserModels.Contracts
{
    public interface IUserRegisterModel
    {
        string Username { get; set; }

        string Password { get; set; }

        string EMail { get; set; }

        string FirstName { get; set; }

        string LastName { get; set; }

        string AddressText { get; set; }

        string TownName { get; set; }

        UserRole Role { get; set; }
    }
}
