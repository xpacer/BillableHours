using System.Collections.Generic;
using BillableHours.Models.Data;

namespace BillableHours.Models.ViewModels
{
    public class InvoiceViewModel
    {
        public List<Invoice> Invoices { get; set; }
    }
}
