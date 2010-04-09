using System;
using System.Web.Security;

namespace Kokugen.Core.Membership.Security
{
    public interface IUser
    {
        string UserName { get; }
        object ProviderUserKey { get; }
        string Email { get; set; }
    }

    public class User :  IUser
    {

        protected User()
        {
        }

        public User(string userName,string email, object providerUserKey)
        {
            UserName = userName;
            ProviderUserKey = providerUserKey;
            Email = email;
        }


        public virtual string UserName { get; protected set; }

        public virtual object ProviderUserKey { get; protected set; }

        public virtual string Email { get; set; }

    }
}