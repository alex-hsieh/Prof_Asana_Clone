using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asana.Library.Models
{
    public class Projects
    {
        public Projects()
        {
            Id = 0;
            Status = 0;
            StartDate = DateTime.Now;
        }

        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime? StartDate { get; set; }
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
            return $"[{Id}] Project: {Name} - Description: {Description} - Start Date: {StartDate:MM/dd/yyyy} - Status: {CompleClassifier()}";
        }
    }
}