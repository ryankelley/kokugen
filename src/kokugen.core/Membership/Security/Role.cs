using System;
using Kokugen.Core.Domain;

namespace Kokugen.Core.Membership.Security
{
    public class Role
    {
        public Guid Id { get; set; }
        public virtual string Name { get; set; }
    }
}