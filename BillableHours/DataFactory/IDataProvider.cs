using System;
using System.Collections.Generic;
using BillableHours.Models.Data;

namespace BillableHours.DataFactory
{
    public interface IDataProvider
    {
        IEnumerable<EmployeeShift> GetEmployeeShifts(string csvId = null);

        void AddEmployeeShifts(IEnumerable<EmployeeShift> employeeShifts);
    }
}
