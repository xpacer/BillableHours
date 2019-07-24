using System.Collections.Generic;
using System.Linq;

namespace BillableHours.Models.Data
{
    public class Invoice
    {
        public string CompanyName { get; set; }

        public IEnumerable<Employee> Employees { get; set; }

        public decimal TotalUnitPrice
        {
            get
            {
                return Employees.Sum(item => item.UnitPrice);
            }
        }

        public decimal TotalCost
        {
            get
            {
                return Employees.Sum(item => item.TotalCost);
            }
        }
    }
}
