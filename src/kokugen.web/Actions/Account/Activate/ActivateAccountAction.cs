using System;
using Kokugen.Web.Conventions;

namespace Kokugen.Web.Actions.Account.Activate
{
    public class ActivateAccountAction
    {
        public ActivateAccountModel Execute(ActivateAccountModel model)
        {
            return new ActivateAccountModel();
        }
    }

    public class ActivateAccountModel : IRequestById
    {
        public Guid Id { get; set; }
    }


}