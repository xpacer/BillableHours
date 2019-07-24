using System.Collections.Generic;
using BillableHours.Models.Data;

namespace BillableHours.Tests.Helpers
{
    public static class TestsHelper
    {
        public static IEnumerable<EmployeeShift> GetDummyData()
        {
            var csvData = new List<EmployeeShift>();

            var csvRow1 = new EmployeeShift
            {
                EmployeeId = "1",
                Date = "2019-10-07",
                StartTime = "09:00",
                EndTime = "10:00",
                Project = "Google"

            };

            var csvRow2 = new EmployeeShift
            {
                EmployeeId = "2",
                Date = "2019-10-07",
                StartTime = "10:00",
                EndTime = "12:00",
                Project = "Facebook"

            };

            var csvRow3 = new EmployeeShift
            {
                EmployeeId = "3",
                Date = "2019-10-07",
                StartTime = "10:00",
                EndTime = "12:00",
                Project = "Google"

            };

            var csvRow4 = new EmployeeShift
            {
                EmployeeId = "2",
                Date = "2019-10-08",
                StartTime = "10:00",
                EndTime = "12:00",
                Project = "Facebook"

            };


            csvData.Add(csvRow1);
            csvData.Add(csvRow2);
            csvData.Add(csvRow3);
            csvData.Add(csvRow4);

            return csvData;
        }
    }
}
