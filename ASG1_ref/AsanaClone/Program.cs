using AsanaClone.Library.Models___MVVM;
using AsanaClone.Library.Services;
using Microsoft.VisualBasic;

namespace AsanaClone.CLI
{
    public class Program
    {
        ///////////////////
        // Main Function //
        public static void Main(string[] args)
        {
            // stuff related to tooddles (ToDo's) //
            var toDos = new List<ToDo>(); // type List<ToDo> - prof's tip: hover on toDos to read the type 
            int choiceInt;
            var itemCount = 0;
            var toDoChoice = 0;
            
            // var priorityInt = 1; // it works without needing this one' it's a redeclaration of priorityInt

            // stuff related to projects
            var projects = new List<Projects>();
            int projectCount = 0;
            int projectChoice = 0;
            int statusInt = 0;



            do 
            { 
                // menu options
                Console.WriteLine("Please choose a menu option: ");
                Console.WriteLine("1. Create a ToDo");
                Console.WriteLine("2. List All ToDo's");
                Console.WriteLine("3. List Outstanding ToDo's");
                Console.WriteLine("4. Delete a ToDo");
                Console.WriteLine("5. Update a ToDo");
                Console.WriteLine("--------------------------------- ");
                Console.WriteLine("6. Create a Project");
                Console.WriteLine("7. List all Projects");
                Console.WriteLine("8. Delete a Project");
                Console.WriteLine("9. Update a Project");
                Console.WriteLine("10. List all To-Do's in a Project");
                Console.WriteLine("11. Exit");

                // read user input - safe default is an exit5
                var choice = Console.ReadLine() ?? "11";

                // menu config
                // safe default 
                if (int.TryParse(choice, out choiceInt))
                {

                    switch (choiceInt)
                    {
                        case 1:
                            ToDoServiceProxy.CreateToDo(ref itemCount);
                            break;

                        case 2:
                            // listing ALL the to-do's
                            ToDoServiceProxy.ListToDos(true);
                            break;

                        case 3:
                            // listing the outstanding to-do's 
                            ToDoServiceProxy.ListToDos();
                            break;
                        
                        case 4:
                            // deleting a todo 
                            ToDoServiceProxy.DeleteToDo(toDoChoice);
                            break;

                        case 5:
                            // updating a todo
                            ToDoServiceProxy.UpdateToDo(toDoChoice);
                            break;

                        case 6:
                            // adding a new project
                            ToDoServiceProxy.AddProject(ref projects, statusInt, ref projectCount, ref itemCount);
                            break;

                        case 7:
                            // listing all projects
                            ToDoServiceProxy.ListProjects(projects);
                            break;

                        case 8:
                            // deleting a project and its associated ToDos
                            ToDoServiceProxy.DeleteProject(ref projects, projectChoice);
                            break;

                        case 9:
                            // updating a project
                            ToDoServiceProxy.UpdateProject(ref projects, projectChoice, ref itemCount);
                            break;

                        case 10:
                            // listing all todos
                            ToDoServiceProxy.ListProjects(ref projects, projectChoice);
                            break;

                        case 11:
                                Console.WriteLine("exited the code with code 0 - no problemo, amigo");
                                break;

                            default:
                                Console.WriteLine("ERROR ALERT: invalid menu selection");
                                break;
                            }
                } else
                {
                    Console.WriteLine($"ERROR ALERT: {choice} is not a valid selection");
                }              
            } while (choiceInt != 11);
        }
    }
}
