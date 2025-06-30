using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsanaClone.Library.Models___MVVM
{
    public class Projects
    {
        private string? name;
        private string? description;
        private string? startDate;
        private string? dueDate;
        private int? status;

        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? StartDate { get; set; }
        public string? DueDate { get; set; }
        public int? Status { get; set; }

        public int Id { get; set; }

        public string CompleClassifier()
        {
            return Status switch
            {
                0 => "Not Started",
                1 => "In Progress",
                2 => "On Hold",
                3 => "Completed",
                _ => "Unknown"
            };
        }

        public override string ToString()
        {
            return $"[{Id}] Project: {Name} - Description: {Description} - Due Date: {DueDate} - Start Date: {StartDate} - Status: {CompleClassifier()}";
        }
    }
}
