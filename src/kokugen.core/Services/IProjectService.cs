using System;
using System.Collections.Generic;
using System.Linq;
using Kokugen.Core.Domain;
using Kokugen.Core.Persistence.Repositories;

namespace Kokugen.Core.Services
{
    public interface IProjectService
    {
        IEnumerable<Project> ListProjects();
        void SaveProject(Project project);
        Project Load(Guid Id);
        Project GetProjectFromName(string name);
    }

    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public IEnumerable<Project> ListProjects()
        {
            return _projectRepository.Query().OrderBy(x => x.Name).ToList();

        }

        public void SaveProject(Project project)
        {
            _projectRepository.Save(project);
        }

        public Project Load(Guid Id)
        {
           return  _projectRepository.Load(Id);
        }

        public Project GetProjectFromName(string name)
        {
            return _projectRepository.Query().Where(c => c.Name == name).FirstOrDefault();
        }
    }
}