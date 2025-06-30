using AsanaClone.Library.Models___MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsanaClone.Library.Services
{
    public static class ToDoServiceProxy
    {
        // for the sake of a better memory management, we incorpoirate this new declaration of todo list
        // ToDo List will be managed by service proxy and not main
        public static List<ToDo> toDos = new List<ToDo>();

        // Function Definitions
        public static void CreateToDo(ref int itemCount)
        {
            // adding a to-do
            Console.WriteLine("Your task's name: ");
            var name = Console.ReadLine();

            Console.WriteLine("Description: ");
            var description = Console.ReadLine();

            Console.WriteLine("Priority level (1 = low; 2 = medium ; 3 = high): ");
            var prioritylvl = Console.ReadLine();

            // string conversion to use the parsed value from the user 
            if (!int.TryParse(prioritylvl, out var priorityInt))
            {
                priorityInt = 1;
            }

            // initializer with the setter of each property to fill in the defaults to the program
            toDos.Add(new ToDo
            {
                Name = name,
                Description = description,
                IsComplete = false,
                PriorityLvl = priorityInt,
                Id = ++itemCount
            }); // makes sure the first ID is NOT zero
        }

        // List All ToDo's when flag is "false", otherwise, prints the completed ones.
        public static void ListToDos(bool isShowCompleted = false)
        {
            // print if the task has not been completed
            if (isShowCompleted)
            {

                toDos.ForEach(Console.WriteLine);
            }
            // print if the task has been completed/ to do's list is empty
            else
            {
                Console.WriteLine("Whoops! List is empty. You don't have any pending ToDo's");
                toDos.Where(t => (t != null) && (t?.IsComplete ?? true))
                .ToList()
                .ForEach(Console.WriteLine);
            }
        }

        // Deleting a ToDo 
        public static void DeleteToDo(int toDoChoice)
        {
            // deleting a to-do. this time, we do not skip a new line before getting user input
            toDos.ForEach(Console.WriteLine);
            Console.Write("ToDo to Delete: ");

            // deletion is index based. we iterate through all the entire list of ToDos 
            toDoChoice = int.Parse(Console.ReadLine() ?? "0");

            // get reference of the ToDo by getting the ID of the list
            var reference = toDos.FirstOrDefault(t => t.Id == toDoChoice);
            if (reference != null)
            {
                toDos.Remove(reference);
            }
        }

        // Updating a ToDo - "ref" keyword is CRUCIAL here for the list to pass by reference not value
        public static void UpdateToDo(int toDoChoice)
        {
            // updating a to-do. using a combination of the parsing functions from the "deleting a todo" 
            // and the input functions from the "add a todo"
            toDos.ForEach(Console.WriteLine);
            Console.Write("ToDo to Update: ");
            toDoChoice = int.Parse(Console.ReadLine() ?? "0");
            var updateReference = toDos.FirstOrDefault(t => t.Id == toDoChoice);

            // this will eventually fail when we use this program on different machines w diff mem sizes
            if (updateReference != null)
            {
                // here we are updating the reference of the description, priority, and status from the list - smart ptrs
                Console.WriteLine("New Description: ");
                updateReference.Description = Console.ReadLine();

                Console.WriteLine("New Priority Level: ");
                // parsing string into int to update the priority reference
                var priorityInput = Console.ReadLine();
                if (int.TryParse(priorityInput, out int newPriorityInt))
                {
                    updateReference.PriorityLvl = newPriorityInt;
                }
                else
                {
                    Console.WriteLine("invalid input. recall (1 = low ; 2 = mid ; 3 = high). no changes made.");
                }

                Console.WriteLine("Has the task been completed? (1 = yes ; 0 = no)");
                var statusInput = Console.ReadLine();
                if (statusInput == "1")
                {
                    updateReference.IsComplete = true;
                }
                else if (statusInput == "0")
                {
                    updateReference.IsComplete = false;
                }
                else
                {
                    Console.WriteLine("invalid input. recall (1 = yes ; 2 = no). no changes made");
                }


            }
        }

        // Add a Project
        public static void AddProject(ref List<Projects> projects, int statusInt,
            ref int projectCount, ref int itemCount)
        {
            Console.WriteLine("Project name: ");
            var projectName = Console.ReadLine();

            Console.WriteLine("Description: ");
            var projectDescription = Console.ReadLine();

            Console.WriteLine("Start Date (MM/DD/YYYY): ");
            var startDate = Console.ReadLine();

            Console.WriteLine("Due Date (MM/DD/YYYY): ");
            var dueDate = Console.ReadLine();

            Console.WriteLine("Status (0 = Not Started ; 1 = In Progress ; 2 = On Hold ; 3 = Completed): ");
            var statusLevel = Console.ReadLine();

            // string conversion to int to classify the user's input
            if (!int.TryParse(statusLevel, out statusInt))
            {
                statusInt = 0; // default to "Not Started"
            }

            // default values
            projects.Add(new Projects
            {
                Name = projectName,
                Description = projectDescription,
                StartDate = startDate,
                DueDate = dueDate,
                Status = statusInt,
                Id = ++projectCount
            });

            // Option to add ToDos to the new project
            Console.WriteLine("Would you like to add ToDos to this project? (y/n): ");
            var addTodos = Console.ReadLine()?.ToLower();

            while (addTodos == "y" || addTodos == "yes")
            {
                Console.WriteLine("ToDo name: ");
                var todoName = Console.ReadLine();

                Console.WriteLine("ToDo description: ");
                var todoDescription = Console.ReadLine();

                Console.WriteLine("Priority level (1 = low; 2 = medium ; 3 = high): ");
                var todoPriority = Console.ReadLine();

                if (!int.TryParse(todoPriority, out int todoPriorityInt))
                {
                    todoPriorityInt = 1;
                }

                toDos.Add(new ToDo
                {
                    Name = todoName,
                    Description = todoDescription,
                    IsComplete = false,
                    PriorityLvl = todoPriorityInt,
                    ProjectId = projectCount,
                    Id = ++itemCount
                });

                Console.WriteLine("Add another ToDo to this project? (y/n): ");
                addTodos = Console.ReadLine()?.ToLower();
            }
        }

        // list all projects
        public static void ListProjects(List<Projects> projects)
        {
            if (projects.Any())
            {
                projects.ForEach(Console.WriteLine);
            }
            else
            {
                Console.WriteLine("Whoops! No projects found.");
            }

        }

        // deleting a project
        public static void DeleteProject(ref List<Projects> projects, int projectChoice)
        {
            if (projects.Any())
            {
                projects.ForEach(Console.WriteLine);
                Console.Write("Project to Delete: ");

                if (!int.TryParse(Console.ReadLine() ?? "0", out projectChoice))
                {
                    Console.WriteLine("Invalid input. Please enter a valid project ID.");
                    return;
                }

                var projectReference = projects.FirstOrDefault(p => p.Id == projectChoice);
                if (projectReference != null)
                {
                    projects.Remove(projectReference);
                    Console.WriteLine("Project deleted successfully.");
                }
                else
                {
                    Console.WriteLine("Project not found.");
                }
            }
            else
            {
                Console.WriteLine("Whoops! No projects found");
            }
        }

        public static void UpdateProject(ref List<Projects> projects, int projectChoice, ref int itemCount) // added ref flag to the item count to be sure this updates - TEST
        {
            // updating a project. using a combination of the parsing functions from the "deleting a project" 
            // and the input functions from the "add a project"
            projects.ForEach(Console.WriteLine);
            Console.Write("Project to Update: ");
            if (!int.TryParse(Console.ReadLine() ?? "0", out projectChoice))
            {
                Console.WriteLine("Invalid input. Please enter a valid project ID.");
                return;
            }
            var updateProjectReference = projects.FirstOrDefault(p => p.Id == projectChoice);

            // this will eventually fail when we use this program on different machines w diff mem sizes
            if (updateProjectReference != null)
            {
                // here we are updating the reference of the description, dates, and status from the list - smart ptrs
                Console.WriteLine("New Description: ");
                updateProjectReference.Description = Console.ReadLine();

                Console.WriteLine("New Start Date (MM/DD/YYYY): ");
                updateProjectReference.StartDate = Console.ReadLine();

                Console.WriteLine("New End Date (MM/DD/YYYY): ");
                updateProjectReference.DueDate = Console.ReadLine();

                Console.WriteLine("New Status (0 = Not Started; 1 = In Progress; 2 = On Hold; 3 = Completed): ");
                // parsing string into int to update the status reference
                var statusInput = Console.ReadLine();
                if (int.TryParse(statusInput, out int newStatusInt))
                {
                    updateProjectReference.Status = newStatusInt;
                }
                else
                {
                    Console.WriteLine("invalid input. recall (0 = Not Started; 1 = In Progress; 2 = On Hold; 3 = Completed). no changes made.");
                }

                // Option to add more ToDos to the project
                Console.WriteLine("Would you like to add ToDos to this project? (y/n): ");
                var addTodos = Console.ReadLine()?.ToLower();

                while (addTodos == "y" || addTodos == "yes")
                {
                    Console.WriteLine("ToDo name: ");
                    var todoName = Console.ReadLine();

                    Console.WriteLine("ToDo description: ");
                    var todoDescription = Console.ReadLine();

                    Console.WriteLine("Priority level (1 = low; 2 = medium ; 3 = high): ");
                    var todoPriority = Console.ReadLine();

                    if (!int.TryParse(todoPriority, out int todoPriorityInt))
                    {
                        todoPriorityInt = 1;
                    }

                    toDos.Add(new ToDo
                    {
                        Name = todoName,
                        Description = todoDescription,
                        IsComplete = false,
                        PriorityLvl = todoPriorityInt,
                        ProjectId = updateProjectReference.Id,  // Link to the project being updated
                        Id = ++itemCount
                    });

                    Console.WriteLine("Add another ToDo to this project? (y/n): ");
                    addTodos = Console.ReadLine()?.ToLower();
                }

                Console.WriteLine("Project updated successfully.");
            }
            else
            {
                Console.WriteLine("Project not found.");
            }
        }

        public static void ListProjects(ref List<Projects> projects, int projectChoice)
        {
            // listing all ToDos in a given project
            // first show all projects so user can choose
            if (projects.Any())
            {
                projects.ForEach(Console.WriteLine);
                Console.Write("Enter Project ID to view its ToDos: ");
                projectChoice = int.Parse(Console.ReadLine() ?? "0");

                // check if project exists
                var selectedProject = projects.FirstOrDefault(p => p.Id == projectChoice);
                if (selectedProject != null)
                {
                    Console.WriteLine($"ToDos for Project: {selectedProject.Name}");

                    // Filter ToDos by ProjectId
                    var projectTodos = toDos.Where(t => t.ProjectId == projectChoice).ToList();

                    if (projectTodos.Any())
                    {
                        projectTodos.ForEach(Console.WriteLine);
                    }
                    else
                    {
                        Console.WriteLine("No ToDos found in this project.");
                    }
                }
                else
                {
                    Console.WriteLine("Project not found.");
                }
            }
            else
            {
                Console.WriteLine("No projects available. Create a project first.");
            }
        }
    }
}
