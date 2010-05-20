using System;
using FubuMVC.Core.Security;
using Kokugen.Core.Membership.Services;
using Kokugen.Core.Services;

namespace Kokugen.Web.Actions.Card.claim
{
    public class CardClaimAction
    {
        private readonly ICardService _cardService;
        private readonly IUserService _userService;
        private readonly ISecurityContext _securityContext;

        public CardClaimAction(ICardService cardService, IUserService userService, ISecurityContext securityContext)
        {
            _cardService = cardService;
            _userService = userService;
            _securityContext = securityContext;
        }

        public AjaxResponse Command(CardClaimInputModel model)
        {
            var user = _userService.GetUserByLogin(_securityContext.CurrentIdentity.Name);

            var card = _cardService.GetCard(model.CardId);

            card.AssignedTo = user;
            
            _cardService.SaveCard(card);

            return new AjaxResponse() {Success = true, Item = new CardUserModel {GravatarHash = user.GravatarHash, UserDisplay = user.DisplayName()}};
        }
    }

    public class CardUserModel
    {
        public string GravatarHash { get; set; }

        public string UserDisplay { get; set; }
    }

    public class CardClaimInputModel
    {
        public Guid CardId { get; set; }
    }
}