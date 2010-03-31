using System;
using Kokugen.Core.Services;

namespace Kokugen.Web.Actions.BoardColumn
{
    public class RemoveAction
    {
        private readonly IBoardService _boardService;

        public RemoveAction(IBoardService boardService)
        {
            _boardService = boardService;
        }

        public AjaxResponse Remove(DeleteColumnInputModel model)
        {
            _boardService.DeleteColumn(model.Id);
            return new AjaxResponse
            {
                Success = true,
                Item = model.Id
            };
        }
    }

    public class DeleteColumnInputModel
    {
        public Guid Id { get; set; }
    }
}