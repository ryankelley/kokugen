using System;
using System.Linq;
using Kokugen.Core.Domain;
using Kokugen.Core.Services;

namespace Kokugen.Web.Actions.Card.Move
{
    public class CardMovedAction
    {
        private readonly ICardService _cardService;
        private readonly IBoardService _boardService;

        public CardMovedAction(ICardService cardService, IBoardService boardService)
        {
            _cardService = cardService;
            _boardService = boardService;
        }

        public AjaxResponse Command(CardMoveInputModel model)
        {
            var card = _cardService.GetCard(model.Id);
            var column = _boardService.GetColumn(model.ColumnId);
            card.Column = column;
            card.ColumnChanged();

            var customColumn = card.Project.GetBoardColumns().Where(x => x.Id == model.ColumnId).FirstOrDefault();
            
            if (column.Name == Core.Domain.BoardColumn.ArchiveName)
            {
                card.Status = CardStatus.Complete;
                card.DateCompleted = DateTime.Now;
                card.StopActivity();
            }
            else if (column.Name == Core.Domain.BoardColumn.BacklogName)
            {
                card.Status = CardStatus.New;
                card.Started = null;
                card.DateCompleted = null;
            }
            else
                card.Status = CardStatus.New;
            
            // set the dates
            if(customColumn != null)
            {
                if(customColumn.ColumnOrder == 1 || card.Started == null)
                {
                    // the card was started
                    card.Started = DateTime.Now;
                    card.StartWorking();
                }
                
                    
                card.DateCompleted = null;
                card.StartWorking();
            }
            else
            {
                card.StopActivity();
            }
            

            card.BlockReason = "";

            

            _cardService.SaveCard(card);

            return new AjaxResponse { Success = true };
        }
    }
}