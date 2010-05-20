namespace Kokugen.Core.Membership.Services
{
    public interface IPasswordValidator
    {
        bool ValidatePassword(string password);
    }
}