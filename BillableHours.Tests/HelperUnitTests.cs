using System;
using Xunit;
using BillableHours.Helpers;
using Moq;
using System.Collections.Generic;
using BillableHours.Data;
using System.Linq;
using BillableHours.Tests.Helpers;

namespace BillableHours.Tests
{
    public class HelperUnitTests
    {

        [Fact]
        public void TestGetInvoicesReturnsNonEmptyInvoices()
        {
            var dummyInputCsv = TestsHelper.GetDummyData();

            var invoices = Helper.GetInvoices(dummyInputCsv);

            Assert.NotEmpty(invoices);
            Assert.NotNull(invoices);
        }

        [Fact]
        public void TestGetInvoicesReturnsInvoicesForDistinctCompanies()
        {
            var dummyInputCsv = TestsHelper.GetDummyData();

            var invoices = Helper.GetInvoices(dummyInputCsv);

            var expectedNumberOfInvoices = 2;

            Assert.Equal(expectedNumberOfInvoices, invoices.Count());

        }

        [Fact]
        public void TestGetInvoicesReturnsNonEmptyEmployeesForAnExistingProject()
        {
            var dummyInputCsv = TestsHelper.GetDummyData();

            var invoices = Helper.GetInvoices(dummyInputCsv);
            var facebookInvoice = invoices.FirstOrDefault(i => i.CompanyName == "Facebook");


            Assert.NotNull(facebookInvoice.Employees);
            Assert.NotEmpty(facebookInvoice.Employees);
        }

        [Fact]
        public void TestGetInvoicesReturnsNullForANonExistingProject()
        {
            var dummyInputCsv = TestsHelper.GetDummyData();

            var invoices = Helper.GetInvoices(dummyInputCsv);
            var tibetanInvoice = invoices.FirstOrDefault(i => i.CompanyName == "Tibetan");

            Assert.Null(tibetanInvoice);
        }

        [Fact]
        public void TestGetInvoicesReturnsACorrectTotalNumberOfHoursForAnEmployee()
        {
            var dummyInputCsv = TestsHelper.GetDummyData();

            var invoices = Helper.GetInvoices(dummyInputCsv);
            var facebookInvoice = invoices.FirstOrDefault(i => i.CompanyName == "Facebook");
            var employeeHours = facebookInvoice.Employees.Where(i => i.Id == "2").Select(s => s.NumberOfHours).FirstOrDefault();

            var expectedNumberOfHours = 4;
            Assert.NotNull(employeeHours);
            Assert.Equal(expectedNumberOfHours, employeeHours);

        }

        [Fact]
        public void TestGetInvoicesReturnsADistinctSetOfEmployeesPerInvoice()
        {
            var dummyInputCsv = TestsHelper.GetDummyData();

            var invoices = Helper.GetInvoices(dummyInputCsv);

            var googleInvoice = invoices.FirstOrDefault(i => i.CompanyName == "Google");

            var expectedNumberOfEmployees = 2;

            Assert.NotNull(googleInvoice.Employees);
            Assert.Equal(expectedNumberOfEmployees, googleInvoice.Employees.Count());

        }
    }

}

