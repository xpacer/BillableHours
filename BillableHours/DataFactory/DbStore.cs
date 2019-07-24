using System;
using System.Collections.Generic;
using BillableHours.DataFactory.Database;
using BillableHours.DataFactory.Repository;
using BillableHours.Models.Data;

namespace BillableHours.DataFactory
{
    public class DbStore : IDataProvider
    {
        private readonly EmployeeShiftRespository _employeeShiftRepository;

        public DbStore(BillableDbContext billableDbContext)
        {
            //TODO: Inject Repository Dependency From Services
            _employeeShiftRepository = new EmployeeShiftRespository(billableDbContext);
        }

        public void AddEmployeeShifts(IEnumerable<EmployeeShift> employeeShifts)
        {
            _employeeShiftRepository.AddAll(employeeShifts);
        }

        public IEnumerable<EmployeeShift> GetEmployeeShifts(string csvId = null)
        {
            return _employeeShiftRepository.GetByCsvId(csvId);
        }
    }
}
