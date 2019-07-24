using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BillableHours.Models.Data;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Internal;

namespace BillableHours.Helpers
{
    public static class Helper
    {
        /// <summary>
        /// Creates and Maps CSV Form File records to a list of EmployeeShift objects
        /// </summary>
        /// <param name="csvId">CSV Id</param>
        /// <param name="formFile">Form file</param>
        /// <param name="elementPositions">Element Positions</param>
        /// <param name="hasHeader">Record's first row is header</param>
        /// <returns></returns>
        public static IEnumerable<EmployeeShift> CreateRecordsFromFile(string csvId, IFormFile formFile, IDictionary<string, int> elementPositions, bool hasHeader = false)
        {
            var records = new List<EmployeeShift>();
            Configuration csvConfig = new Configuration();

            using (var reader = new StreamReader(formFile.OpenReadStream()))
            using (var csv = new CsvReader(reader))
            {
                csv.Configuration.HasHeaderRecord = hasHeader;
                csv.Configuration.RegisterClassMap(new EmployeeShiftMap(elementPositions, csvId));

                if (hasHeader)
                {
                    csv.Configuration.PrepareHeaderForMatch = (string header, int index) => header.ToLower();
                }

                records = csv.GetRecords<EmployeeShift>().ToList();
            }

            return records;
        }

        /// <summary>
        /// Generates invoices from Employee Shifts data
        /// </summary>
        /// <param name="employeeShifts">Employee Shifts</param>
        /// <returns></returns>
        public static IEnumerable<Invoice> GetInvoices(IEnumerable<EmployeeShift> employeeShifts)
        {
            var invoices = new List<Invoice>();

            if (employeeShifts == null)
                return invoices;

            var distinctProjects = employeeShifts.Select(item => item.Project).Distinct();

            foreach (var project in distinctProjects)
            {
                var invoice = new Invoice
                {
                    CompanyName = project
                };

                var projectEmployeeData = employeeShifts.Where(data => data.Project == project);
                invoice.Employees = ExtractEmployeeData(projectEmployeeData);
                invoices.Add(invoice);
            }

            return invoices;
        }

        /// <summary>
        /// Extracts employee basic information from the Shifts data
        /// </summary>
        /// <param name="employeeShifts">Employee Shifts</param>
        /// <returns></returns>
        public static IEnumerable<Employee> ExtractEmployeeData(IEnumerable<EmployeeShift> employeeShifts)
        {
            var employees = new List<Employee>();

            if (employeeShifts == null || !employeeShifts.Any())
                return employees;

            var distinctEmployees = employeeShifts.Select(item => item.EmployeeId).Distinct();

            foreach (var employeeId in distinctEmployees)
            {
                // Grouping Shifts by Date to cater for duplicates.
                // If a duplicate exists with different time stamps, select the entry with the larger timestamp
                var employeeData = employeeShifts
                    .Where(c => c.EmployeeId == employeeId)
                    .GroupBy(a => a.Date)
                    .Select(p => (p.First().NumberOfHours, p.First().HourlyBillableRate));

                var employee = new Employee
                {
                    Id = employeeId,
                    NumberOfHours = employeeData.Sum(s => s.NumberOfHours),
                    UnitPrice = employeeData.Select(s => s.HourlyBillableRate).First()
                };

                employees.Add(employee);
            }

            return employees;
        }

        /// <summary>
        /// Generates CSV ID
        /// </summary>
        /// <returns>GUID String</returns>
        public static string GenerateCsvId()
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}
