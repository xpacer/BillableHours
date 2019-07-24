using System;
namespace BillableHours.DataFactory.Database
{
    public static class DbInitializer
    {
        public static void Initialize(BillableDbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
