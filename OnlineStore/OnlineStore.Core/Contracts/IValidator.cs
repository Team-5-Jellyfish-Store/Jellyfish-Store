namespace OnlineStore.Core.Contracts
{
    public interface IValidator
    {
        bool IsValid(object obj);

        string ValidateValue(string property, bool isRequired);

        void ValidateEmail(string email);

        void ValidatePassword(string password);

        void ValidateLength(string property, int minLength, int maxLength);

        void ValidateLength(int property, int minLength, int maxLength);
    }
}
