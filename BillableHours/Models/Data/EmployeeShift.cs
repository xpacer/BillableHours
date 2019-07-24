using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using CsvHelper.Configuration.Attributes;

namespace BillableHours.Models.Data
{
    public class EmployeeShift
    {
        [Key]
        public int ID { get; set; }

        public string CsvID { get; set; }

        public int EmployeeId { get; set; }

        public decimal HourlyBillableRate { get; set; }

        public string Project { get; set; }

        public DateTime Date { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public DateTime CreatedDate
        {
            get
            {
                return DateTime.Now;
            }
        }

        [NotMapped]
        public int NumberOfHours
        {
            get
            {
                try
                {
                    return (EndTime - StartTime).Hours;
                }
                catch (Exception)
                {
                    return 0;
                }
            }
        }
    }
}
