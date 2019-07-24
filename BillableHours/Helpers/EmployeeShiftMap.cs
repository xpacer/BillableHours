using System.Collections.Generic;
using BillableHours.Models.Data;
using CsvHelper.Configuration;

namespace BillableHours.Helpers
{
    /// <summary>
    /// EmployeeShift Map for CSV
    /// </summary>
    public class EmployeeShiftMap : ClassMap<EmployeeShift>
    {
        public EmployeeShiftMap(IDictionary<string, int> positions)
        {
            if (positions.ContainsKey(Constants.EMPLOYEE_ID_KEY))
                Map(m => m.EmployeeId).Index(positions[Constants.EMPLOYEE_ID_KEY]);

            if (positions.ContainsKey(Constants.HOURLY_BILLABLE_RATE_KEY))
                Map(m => m.HourlyBillableRate).Index(positions[Constants.HOURLY_BILLABLE_RATE_KEY]);

            if (positions.ContainsKey(Constants.PROJECT_KEY))
                Map(m => m.Project).Index(positions[Constants.PROJECT_KEY]);

            if (positions.ContainsKey(Constants.DATE_KEY))
                Map(m => m.Date).Index(positions[Constants.DATE_KEY]);

            if (positions.ContainsKey(Constants.START_TIME_KEY))
                Map(m => m.StartTime).Index(positions[Constants.START_TIME_KEY]);

            if (positions.ContainsKey(Constants.END_TIME_KEY))
                Map(m => m.EndTime).Index(positions[Constants.END_TIME_KEY]);

        }
    }
}
