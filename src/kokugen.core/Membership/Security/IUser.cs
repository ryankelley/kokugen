namespace Kokugen.Core.Membership.Security
{
    public interface IUser
    {
        string UserName { get; }
        object ProviderUserKey { get; }
        string Email { get; set; }
    }
}