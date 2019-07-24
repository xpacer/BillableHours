using System.Collections.Generic;

namespace BillableHours.Models.ViewModels
{
    public class ProjectsViewModel
    {
        public string CsvId { get; set; }

        public IEnumerable<string> ProjectNames { get; set; }
    }
}
