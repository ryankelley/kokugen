using System;
using Kokugen.Web.Conventions;
using Kokugen.Web.Actions.Account.Register;

namespace Kokugen.Web.Actions.Account.Activate
{
    public class ActivateAccountAction
    {

        private readonly IRegistrationService _registrationService;

        public ActivateAccountAction(IRegistrationService registrationService )
        {
            _registrationService = registrationService;
        }

        public ActivateAccountModel Execute(ActivateAccountModel model)
        {
            _registrationService.ActivateAccount(model.Id);
            return new ActivateAccountModel();
        }
    }

    public class ActivateAccountModel : IRequestById
    {
        public Guid Id { get; set; }
    }


}