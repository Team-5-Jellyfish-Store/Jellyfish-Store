using OnlineStore.Models.Enums;

namespace OnlineStore.DTO.UserModels.Contracts
{
    public interface IUserLoginModel
    {
        string Username { get; set; }
        string Password { get; set; }
        UserRole Role { get; set; }
    }
}
