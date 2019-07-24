using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace BillableHours.Models.ViewModels
{
    public class HomeViewModel
    {
        [Required]
        [Display(Name = "Select a CSV file")]
        public IFormFile File { get; set; }

        [Display(Name = "Generate PDF")]
        public bool GeneratePDF { get; set; }

        [Display(Name = "First row is header")]
        public bool FirstRowHeader { get; set; }

        // Index position of properties
        [Required, Display(Name = "Employee Id")]
        public int EmployeeId { get; set; }

        [Required, Display(Name = "Hourly Billable Rate")]
        public int HourlyBillableRate { get; set; }

        [Required, Display(Name = "Project")]
        public int Project { get; set; }

        [Required, Display(Name = "Date")]
        public int Date { get; set; }

        [Required, Display(Name = "Start Time")]
        public int StartTime { get; set; }

        [Required, Display(Name = "End Time")]
        public int EndTime { get; set; }


    }
}
