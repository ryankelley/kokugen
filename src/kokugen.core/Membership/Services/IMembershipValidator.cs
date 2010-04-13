using System;
using Kokugen.Core.Domain;
using Kokugen.Core.Membership.Security;
using Kokugen.Core.Membership.Settings;
using Kokugen.Core.Services;

namespace Kokugen.Core.Membership.Services
{
    public interface IMembershipValidator<USER> where USER : IUser
    {
        bool ValidateUser(USER user, string password);
    }

    public interface IMembershipValidator : IMembershipValidator<User>{}

    public class MembershipValidator : IMembershipValidator
    {
        private readonly IPasswordHelperService _passwordHelperService;
        private readonly PasswordSettings _passwordSettings;

        public MembershipValidator(IPasswordHelperService passwordHelperService, PasswordSettings passwordSettings)
        {
            _passwordHelperService = passwordHelperService;
            _passwordSettings = passwordSettings;
        }

        public bool ValidateUser(User user, string password)
        {
            switch (_passwordSettings.PasswordFormat)
            {
                case PasswordFormat.Hashed:
                    return _passwordHelperService.ComparePasswordToHash(password, user.Password);
                    break;
                case PasswordFormat.Clear:
                    return password.Equals(user.Password);
                    break;
                case PasswordFormat.Encrypted:
                    throw new NotImplementedException();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}