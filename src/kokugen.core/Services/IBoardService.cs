using System;
using System.Collections.Generic;
using System.Linq;

namespace Kokugen.Core.Services
{
    public interface IBoardService
    {
        bool ReorderColumns(Guid ProjectId, IList<ColumnOrderDTO> columns);
    }

    public class BoardService : IBoardService
    {
        private readonly IProjectService _projectService;

        public BoardService(IProjectService projectService)
        {
            _projectService = projectService;
        }

        public bool ReorderColumns(Guid ProjectId, IList<ColumnOrderDTO> columns)
        {
            var project = _projectService.GetProjectFromId(ProjectId);

            project.GetBoardColumns().Each(x =>
                                               {
                                                   var data = columns.Where(col => col.Id == x.Id).FirstOrDefault();

                                                   if (data != null)
                                                   {
                                                       x.ColumnOrder = data.ColumnOrder;
                                                   }
                                               });

            _projectService.SaveProject(project);

            return true;
        }
    }

    public class ColumnOrderDTO
    {
        public Guid Id { get; set; }
        public int ColumnOrder { get; set; }
    }
}