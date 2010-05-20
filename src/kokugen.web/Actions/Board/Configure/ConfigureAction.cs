using System.Linq;
using FubuMVC.Core.View;
using Kokugen.Core.Services;

namespace Kokugen.Web.Actions.Board.Configure
{
    public class ConfigureAction
    {
        private readonly IProjectService _projectService;

        public ConfigureAction(IProjectService projectService)
        {
            _projectService = projectService;
        }

        public BoardConfigurationModel Query(BoardConfigurationModel model)
        {
            return new BoardConfigurationModel
            {
                Id = model.Id,
                BoardColumns = _projectService.GetProjectFromId(model.Id).GetAllBoardColumns().Select(x => new BoardColumnDTO { Name = x.Name, Description = x.Description, Id = x.Id, CardLimit = x.CardLimit }).ToList(),
                ProjectId = model.Id

            };
        }
    }

    public class Configure : FubuPage<BoardConfigurationModel> { }
}