using System;
using System.Collections.Generic;
using System.Linq;
using Kokugen.Core.Domain;
using Kokugen.Core.Persistence.Repositories;
using Kokugen.Core.Validation;

namespace Kokugen.Core.Services
{
    public interface IProjectService
    {
        IEnumerable<Project> ListProjects();
        INotification SaveProject(Project project);
        Project Load(Guid Id);
        Project GetProjectFromName(string name);
        Project CreateProject(Project project, Company company);
    }

    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IValidator _validator;

        public ProjectService(IProjectRepository projectRepository, IValidator validator)
        {
            _projectRepository = projectRepository;
            _validator = validator;
        }

        public IEnumerable<Project> ListProjects()
        {
            return _projectRepository.Query().OrderBy(x => x.Name).ToList();

        }

        public INotification SaveProject(Project project)
        {
            var notification = _validator.Validate(project);

            if(notification.IsValid())
            {
                _projectRepository.Save(project);
            }

            return notification;

        }

        public Project Load(Guid Id)
        {
           return  _projectRepository.Get(Id);
        }

        public Project GetProjectFromName(string name)
        {
            return _projectRepository.Query().Where(c => c.Name == name).FirstOrDefault();
        }

        public Project CreateProject(Project project, Company company)
        {
            project.Backlog = new BoardColumn {Name = "Backlog", Description = "This is the project Backlog"};
            project.Archive = new BoardColumn {Name = "Archive", Description = "This queue contains all finished tasks"};

            project.Company = company;

            return project;
        }
    }
}