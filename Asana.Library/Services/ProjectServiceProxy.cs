﻿using Asana.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asana.Library.Services
{
    public class ProjectServiceProxy
    {
        private List<Projects> _projectList;
        public List<Projects> Projects { 
            get
            {
                return _projectList?.Take(100).ToList() ?? new List<Projects>();
            }

            private set {
                if (value != _projectList)
                {
                    _projectList = value ?? new List<Projects>();
                }
            }
        }

        private ProjectServiceProxy()
        {
            _projectList = new List<Projects> // Fixed: assign to _projectList instead of Projects
            {
                new Projects{Id = 1, Name = "Project 1", Description = "My Project 1", StartDate = DateTime.Parse("2025-01-01"), Status = 0},
                new Projects{Id = 2, Name = "Project 2", Description = "My Project 2", StartDate = DateTime.Parse("2025-02-01"), Status = 1},
                new Projects{Id = 3, Name = "Project 3", Description = "My Project 3", StartDate = DateTime.Parse("2025-03-01"), Status = 2},
                new Projects{Id = 4, Name = "Project 4", Description = "My Project 4", StartDate = DateTime.Parse("2025-04-01"), Status = 3},
                new Projects{Id = 5, Name = "Project 5", Description = "My Project 5", StartDate = DateTime.Parse("2025-05-01"), Status = 0}
            };
        }

        private static ProjectServiceProxy? instance;

        private int nextKey
        {
            get
            {
                if(Projects.Any())
                {
                    return Projects.Select(t => t.Id).Max() + 1;
                }
                return 1;
            }
        }

        public static ProjectServiceProxy Current
        {
            get
            {
                if(instance == null)
                {
                    instance = new ProjectServiceProxy();
                }

                return instance;
            }
        }
        
        public Projects? AddOrUpdate(Projects? projects)
        {
            if(projects != null && projects.Id == 0)
            {
                projects.Id = nextKey;
                _projectList.Add(projects);
            }

            return projects;
        }
        
        public void DisplayProjects(bool isShowCompleted = false)
        {
            if (isShowCompleted)
            {
                Projects.ForEach(Console.WriteLine);
            }
            else
            {
                Projects.Where(t => (t != null) && !(t?.Status == 3))
                                .ToList()
                                .ForEach(Console.WriteLine);
            }
        }
        
        public Projects? GetById(int id)
        {
            return Projects.FirstOrDefault(t => t.Id == id);
        }

        public void DeleteProject(Projects? project)
        {
            if (project == null)
            {
                return;
            }
            _projectList.Remove(project);
        }
    }
}