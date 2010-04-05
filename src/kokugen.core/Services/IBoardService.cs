using System;
using System.Collections.Generic;
using System.Linq;
using Kokugen.Core.Domain;
using Kokugen.Core.Persistence.Repositories;

namespace Kokugen.Core.Services
{
    public interface IBoardService
    {
        bool ReorderColumns(Guid projectId, IList<ColumnOrderDTO> columns);
        CustomBoardColumn ModifyColumn(Guid projectId, Guid columnId, string name, string description, int limit);
        CustomBoardColumn GetCustomColumn(Guid columnId);
        BoardColumn GetColumn(Guid id);
        bool DeleteColumn(Guid id);
    }

    public class BoardService : IBoardService
    {
        private readonly IProjectService _projectService;
        private readonly ICustomBoardColumnRepository _customBoardColumnRepository;
        private readonly IBoardColumnRepository _boardColumnRepository;

        public BoardService(IProjectService projectService, ICustomBoardColumnRepository customBoardColumnRepository, IBoardColumnRepository boardColumnRepository)
        {
            _projectService = projectService;
            _customBoardColumnRepository = customBoardColumnRepository;
            _boardColumnRepository = boardColumnRepository;
        }

        public bool ReorderColumns(Guid projectId, IList<ColumnOrderDTO> columns)
        {
            var project = _projectService.GetProjectFromId(projectId);

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

        public CustomBoardColumn ModifyColumn(Guid projectId, Guid columnId, string name, string description, int limit)
        {
            var project = _projectService.GetProjectFromId(projectId);

            if(columnId.IsEmpty())
            {
                var newColumn = new CustomBoardColumn {Name = name, Description = description, Limit = limit};
                project.AddBoardColumn(newColumn);
                _customBoardColumnRepository.Save(newColumn);
                _projectService.SaveProject(project);
                return newColumn;
            }
            else
            {
                var column = project.GetBoardColumns().Where(x => x.Id == columnId).FirstOrDefault();

                if(column != null)
                {
                    column.Name = name;
                    column.Description = description;
                    column.Limit = limit;

                    _customBoardColumnRepository.Save(column);
                    return column;
                }

            }

            return null;
            
        }

        public CustomBoardColumn GetCustomColumn(Guid columnId)
        {
            return _customBoardColumnRepository.Get(columnId);
        }

        public BoardColumn GetColumn(Guid id)
        {
            return _boardColumnRepository.Get(id);
        }

        public bool DeleteColumn(Guid id)
        {
            var col = _customBoardColumnRepository.Load(id);
            _customBoardColumnRepository.Delete(col);
            return true;
        }
    }

    public class ColumnOrderDTO
    {
        public Guid Id { get; set; }
        public int ColumnOrder { get; set; }
    }
}