namespace BillableHours.Models.Data
{
    public class Employee
    {
        public int Id { get; set; }

        public int NumberOfHours { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal TotalCost
        {
            get
            {
                return NumberOfHours * UnitPrice;
            }
        }
    }
}
