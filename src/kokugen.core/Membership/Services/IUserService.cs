#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using Kokugen.Core.Domain;
using Kokugen.Core.Membership.Security;
using Kokugen.Core.Persistence.Repositories;
using Kokugen.Core.Validation;
using PagedList;

#endregion

namespace Kokugen.Core.Membership.Services
{

    public interface IUserService<USER> where USER : IUser
    {
        void Update(USER user);
        void Delete(USER user);
        USER Retrieve(object id);
        INotification Create(USER user);

        USER GetUserByLogin(string name);
        USER GetUserByEmail(string email);
        IPagedList<USER> FindAll(int pageIndex, int pageSize);

        int TotalUsers { get; }
        
    }

    public interface IUserService : IUserService<User>{}
}