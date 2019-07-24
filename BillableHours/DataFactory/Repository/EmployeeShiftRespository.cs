using System.Collections.Generic;
using System.Linq;
using BillableHours.DataFactory.Database;
using BillableHours.Models.Data;

namespace BillableHours.DataFactory.Repository
{
    public class EmployeeShiftRespository
    {
        private readonly BillableDbContext _billableDbContext;

        public EmployeeShiftRespository(BillableDbContext billableDbContext)
        {
            _billableDbContext = billableDbContext;
        }

        public void Add(EmployeeShift employeeShift)
        {
            _billableDbContext.Add(employeeShift);
            _billableDbContext.SaveChanges();
        }

        public void AddAll(IEnumerable<EmployeeShift> employeeShifts)
        {
            _billableDbContext.AddRange(employeeShifts);
            _billableDbContext.SaveChanges();
        }

        public IEnumerable<EmployeeShift> GetAll()
        {
            return _billableDbContext.EmployeeShifts.ToList();
        }

        public IEnumerable<EmployeeShift> GetByCsvId(string csvId)
        {
            return _billableDbContext.EmployeeShifts.Where(i => i.CsvID == csvId).ToList();
        }
    }
}
