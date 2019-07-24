using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace BillableHours.Models.Data
{
    public class EmployeeShift
    {
        [Key]
        public int ID { get; set; }

        public string CsvID { get; set; }

        public string EmployeeId { get; set; }

        public decimal HourlyBillableRate { get; set; }

        public string Project { get; set; }

        public string Date { get; set; }

        public string StartTime { get; set; }

        public string EndTime { get; set; }

        public DateTime CreatedDate { get; set; }

        [NotMapped]
        public int NumberOfHours
        {
            get
            {
                try
                {
                    var startDateTime = DateTime.ParseExact(StartTime, "HH:mm",
                                        CultureInfo.InvariantCulture);
                    var endDateTime = DateTime.ParseExact(EndTime, "HH:mm",
                                        CultureInfo.InvariantCulture);

                    return (endDateTime - startDateTime).Hours;

                }
                catch (Exception)
                {
                    return 0;
                }
            }
        }
    }
}
