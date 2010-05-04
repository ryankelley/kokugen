using System;
using System.Collections.Generic;
using System.Linq;
using FubuCore;
using Kokugen.Core.Membership;
using Kokugen.Core.Membership.Security;
using Kokugen.Core.Services;
using Kokugen.Core.Validation;

namespace Kokugen.Core.Domain
{
    public class SuperUser : User
    {
        protected SuperUser()
        {
        }

        public SuperUser(string userName, string email, string password) : base(userName, email, password)
        {
        }
    }

    public class User : Entity, IUser
    {
        private IList<Role> _roles = new List<Role>();
        private IList<Project> _projects = new List<Project>();

        protected User()
        {}

        public User(string userName, string email, string password)
        {
            Password = password;
            UserName = userName;
            Email = email;
        }

        [Required]
        public virtual string UserName { get; protected set; }
        [Required, ValidEmail]
        public virtual string Email { get; set; }

        public virtual bool IsLocked { get; private set; }

        public virtual bool IsActivated { get; set; }

        public virtual object ProviderUserKey
        {
            get { return Id; }
        }

        public virtual string DisplayName()
        {
            return FirstName.IsEmpty() ? UserName : FirstName + " " + LastName;
        }
        
        public virtual string FirstName { get; set; }

        public virtual string LastName { get; set; }

        [Required]
        public virtual string Password { get; protected set; }

        public virtual string Question { get; set; }
        public virtual string Answer { get; set; }

        public virtual string GravatarHash { get; set; }

        public virtual IEnumerable<Role> GetRoles()
        {
            return _roles;
        }

        public virtual void AddRole(Role role)
        {
            _roles.Add(role);
        }

        public virtual void AddRoles(IEnumerable<Role> roles)
        {
            roles.Each(x => _roles.Add(x));
        }

        public virtual void ClearRoles()
        {
            _roles.Clear();
        }

        public virtual void SetRoles(IEnumerable<Role> roles)
        {
            ClearRoles();
            AddRoles(roles);
        }

        public virtual bool IsInRole(string roleName)
        {
            return _roles.Where(x => x.Name == roleName).Count() > 0;
        }

        public virtual bool HasPermission(string permissionName)
        {
            //if (IsAdmin())
            //{
            //    return true;
            //}

            foreach (var role in _roles)
            {
                if (role.Permissions == null) continue;
                foreach (var permission in role.Permissions)
                {
                    if (permission.Name.DisplayName == permissionName) return true;
                }
            }

            return false;
        }

        public virtual void SetPassword(IPasswordHelperService service)
        {
            this.Password = service.CreatePasswordHash(this.Password);
        }

        public virtual void ChangePassword(IPasswordHelperService service, string newPassword)
        {
            this.Password = service.CreatePasswordHash(newPassword);
        }

        public virtual void Unlock()
        {
            IsLocked = false;
        }

        public virtual IEnumerable<Project> GetProjects()
        {
            return _projects;
        }

        public virtual void RemoveRole(Role role)
        {
            _roles.Remove(role);
        }
    }
}