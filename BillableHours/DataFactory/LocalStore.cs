using System;
using System.Collections.Generic;
using BillableHours.Models.Data;

namespace BillableHours.DataFactory
{
    /// <summary>
    /// Local Store to store uploaded CSV Data;
    /// </summary>
    public sealed class LocalStore : IDataProvider
    {
        private static readonly Lazy<LocalStore> lazyInstance =
            new Lazy<LocalStore>(() => new LocalStore());

        private LocalStore()
        {
        }

        public static LocalStore Instance
        {
            get
            {
                return lazyInstance.Value;
            }
        }

        private IEnumerable<EmployeeShift> EmployeeShifts { get; set; }

        public void AddEmployeeShifts(IEnumerable<EmployeeShift> employeeShifts)
        {
            EmployeeShifts = employeeShifts;
        }

        public IEnumerable<EmployeeShift> GetEmployeeShifts(string csvId = null)
        {
            return EmployeeShifts;
        }

    }
}
