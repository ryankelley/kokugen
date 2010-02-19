using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using Kokugen.Core.Domain;
using Kokugen.Core.Persistence;
using Kokugen.Core.Services;
using StructureMap;

namespace ProjectTimeTracking
{
    public partial class ProjectTimeTracker : Form, IProjectTimeTracker
    {
        private Project project;
        private Stopwatch _stopWatch;
        private TimeRecord timer;
        private static IProjectService _projectService;
        private List<string> _existingProjectNames;

        public ProjectTimeTracker(IProjectService projectService)
        {
            _projectService = projectService;
            InitializeComponent();
        }


        private void ProjectTimeTracker_Load(object sender, EventArgs e)
        {
            //_projectService = ObjectFactory.GetInstance<IProjectService>();
            _existingProjectNames = new List<string>();

            With.UnitOfWork(() =>
                                {
                                    foreach (var proj in _projectService.ListProjects())
                                    {
                                        _existingProjectNames.Add(proj.Name);
                                    };
                                });
            

            ExistingProjectSelectBox.Text = _existingProjectNames.First();
        }

       

        private void SaveButton_Click(object sender, EventArgs e)
        {
            
            
            project= new Project()
                         {
                             Company = new Company(){Id = Guid.NewGuid(),Name=CompanyNameTextBox.Text,}, 
                             Name = NewProjectNameTextBox.Text,
                             NumberOfSessions = 1,
                             
                         };

            inputGroupBox.Visible = false;
            StartButton.Visible = true;
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            StopButton.Visible = true;
            StartButton.Visible = false;
            timer = new TimeRecord();
            timer.Start();
            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            
            clockGroupBox.Visible = true;
            displayStopwatch.Start();
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            _stopWatch.Stop();
            timer.Stop();
            displayStopwatch.Stop();
            timer.ComputeDuration();
            project.ComputeAverageTimeSpentPerSession();

            TimeSpentLabel.Text = (timer.Duration).ToString("N1");
            CumulativeTimeSpentLabel.Text =(project.TotalTime).ToString("N1");
            AverageTimeSpentLabel.Text = (project.AverageTimeSpentPerSession).ToString("N1");
            TimeGroupBox.Visible = true;
            
            project.AddTime(timer);

            _projectService.SaveProject(project);
            

        }

        private void ExistingProjectSelectButton_Click(object sender, EventArgs e)
        {
            inputGroupBox.Visible = false;
            project = _projectService.GetProjectFromName(ExistingProjectSelectBox.Text);
            timer = new TimeRecord();

        }

        private void stopwatch_Tick(object sender, EventArgs e)
        {
            ClockTime.Text = string.Format("{0}:{1}:{2}", _stopWatch.Elapsed.Hours.ToString("00"), _stopWatch.Elapsed.Minutes.ToString("00"), _stopWatch.Elapsed.Seconds.ToString("00"));
        }

       

        

       
      
    }

    public interface IProjectTimeTracker
    {
    }
}
