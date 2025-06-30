using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// definition of the ToDo class - will be used by the rest of the program 
namespace AsanaClone.Library.Models___MVVM
{
    public class ToDo
    {
        // private attribute 
        private string? name;
        private string? description;
        private bool? completion;
        private int? priority;
        private int? projId;

        // public atrributes of the class - data
        public string? Name { get; set; }

        public string? Description { get; set; }

        public bool? IsComplete { get; set; }

        public int? PriorityLvl { get; set; }

        public int Id {  get; set; }
        public int ProjectId { get; set; }

        public string PrioClassifier()
        {
            return PriorityLvl switch
            {
                1 => "Low Priority",
                2 => "Medium Priority",
                3 => "High Priority",
                _ => "Undefined Priority"
            };
        }

        public string ComplyClassifier()
        {
            return IsComplete switch
            {
                true => "Completed",
                false => "Incomplete",
                _ => "Unknown"
            };
        }

        // this is really important to convert the obj into a string, otherwise, it'll just print the library's name (qualified assembly's name) lol
        public override string ToString()
        {
            return $"[{Id}] - Proj.ID (if any): {ProjectId} - ToDo: {Name} - Description: {Description} - Priority Level: {PrioClassifier()} - Status: {ComplyClassifier()}";
        }
    } 
}