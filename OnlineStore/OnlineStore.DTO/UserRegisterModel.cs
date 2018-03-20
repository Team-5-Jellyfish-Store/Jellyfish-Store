using OnlineStore.Models.DataModels;

namespace OnlineStore.DTO
{
    public class UserRegisterModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EMail { get; set; }
        public string AddressText { get; set; }
        public string TownName { get; set; }
    }
}
