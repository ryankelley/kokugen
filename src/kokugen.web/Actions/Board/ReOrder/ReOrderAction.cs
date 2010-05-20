using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using FubuMVC.Core.View;
using Kokugen.Core.Services;
using Kokugen.Web.Conventions;

namespace Kokugen.Web.Actions.Board.ReOrder
{
    public class ReOrderAction
    {
        private readonly IBoardService _boardService;

        public ReOrderAction(IBoardService boardService)
        {
            _boardService = boardService;
        }

        public AjaxResponse Command(BoardColumnReorderModel model)
        {
            var data = new JavaScriptSerializer().Deserialize<List<ColumnOrderDTO>>(model.columns);

            _boardService.ReorderColumns(model.ProjectId, data);


            return new AjaxResponse();
        }
    }

    public class ReOrderRequest : IRequestById
    {
        public Guid Id { get; set; }
    }


}