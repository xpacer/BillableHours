using System;
using BillableHours.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace BillableHours.DataFactory.Database
{
    public class BillableDbContext : DbContext
    {
        public BillableDbContext(DbContextOptions<BillableDbContext> options) : base(options)
        {
        }

        public DbSet<EmployeeShift> EmployeeShifts { get; set; }
    }
}
