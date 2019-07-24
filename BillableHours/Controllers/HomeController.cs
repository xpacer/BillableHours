using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using BillableHours.DataFactory;
using BillableHours.Helpers;
using BillableHours.Models.Data;
using BillableHours.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

namespace BillableHours.Controllers
{

    public class HomeController : Controller
    {
        private static readonly string ERROR_MESSAGE_KEY = "ErrorMessage";
        private readonly IFileProvider _fileProvider;
        private readonly IDataProvider _dataProvider;

        public HomeController(IFileProvider fileProvider, IDataProvider dataProvider)
        {
            _fileProvider = fileProvider;
            _dataProvider = dataProvider;
        }

        /// <summary>
        /// Home Page
        /// </summary>
        /// <returns>ViewResult Index</returns>
        public IActionResult Index()
        {
            if (TempData.ContainsKey(ERROR_MESSAGE_KEY))
            {
                ViewBag.Error = TempData[ERROR_MESSAGE_KEY];
                TempData.Remove(ERROR_MESSAGE_KEY);
            }

            //Setting expected arrangement as default.
            var viewModel = new HomeViewModel
            {
                EmployeeId = 0,
                HourlyBillableRate = 1,
                Project = 2,
                Date = 3,
                StartTime = 4,
                EndTime = 5
            };

            return View(viewModel);
        }

        /// <summary>
        /// Processes Form POST request, converts, persists the CSV data and handles Redirection.
        /// </summary>
        /// <param name="model">HomeViewModel</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Invoices([FromForm] HomeViewModel model)
        {
            try
            {
                if (model.File == null || model.File.ContentType != "text/csv")
                    throw new Exception("Please upload a valid CSV file!");

                if (!ModelState.IsValid)
                    throw new Exception("One or more required input fields is empty!");

                var columnPositions = new Dictionary<string, int>
                {
                    { Constants.EMPLOYEE_ID_KEY, model.EmployeeId },
                    { Constants.HOURLY_BILLABLE_RATE_KEY, model.HourlyBillableRate },
                    { Constants.PROJECT_KEY, model.Project },
                    { Constants.DATE_KEY, model.Date },
                    { Constants.START_TIME_KEY, model.StartTime },
                    { Constants.END_TIME_KEY, model.EndTime }
                };

                int columnPositionsCount = columnPositions.Count();
                List<int> columnPositionValues = columnPositions.Values.Select(c => c).Distinct().ToList();

                if (columnPositionValues.Count() < columnPositionsCount)
                    //Duplicate values for column positions exist
                    throw new Exception("Invalid index set. Indices can not be duplicate");

                if (columnPositions.Values.Min() < 0 || columnPositions.Values.Max() >= columnPositionsCount)
                    throw new Exception("Invalid index set. Index of a column can not be less than 0 or greater than "
                    + (columnPositionsCount - 1).ToString());


                string csvId = Helper.GenerateCsvId();

                IEnumerable<EmployeeShift> uploadedRecords = Helper.CreateRecordsFromFile(csvId, model.File, columnPositions, model.FirstRowHeader);
                _dataProvider.AddEmployeeShifts(uploadedRecords);

                // Redirects to a page to allow users to download PDF copies of CSV Data
                if (model.GeneratePDF)
                    return RedirectToAction("Projects", new { csvId });

                var viewModel = new InvoiceViewModel
                {
                    Invoices = Helper.GetInvoices(uploadedRecords).ToList()
                };

                return View(viewModel);
            }
            catch (Exception e)
            {
                TempData[ERROR_MESSAGE_KEY] = e.Message;
                return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// View for list of projects in a CSV and a means to view their respective invoices in PDF.
        /// <param name="csvId">Csv Id</param>
        /// </summary>
        /// <returns>ViewResult view</returns>
        [HttpGet]
        public IActionResult Projects(string csvId)
        {
            var csvData = _dataProvider.GetEmployeeShifts(csvId);

            var viewModel = new ProjectsViewModel()
            {
                CsvId = csvId
            };

            if (csvData != null && csvData.Any())
                viewModel.ProjectNames = csvData.Select(i => i.Project).Distinct();
            else
                viewModel.ProjectNames = new List<string>();

            return View(viewModel);
        }

        /// <summary>
        /// Download project invoice in PDF
        /// </summary>
        /// <param name="projectName">Project Name</param>
        /// <param name="csvId">Csv Id</param>
        /// <returns>FileStreamResult invoice</returns>
        [HttpGet]
        public IActionResult Download(string csvId, string projectName)
        {
            IEnumerable<EmployeeShift> csvData = _dataProvider.GetEmployeeShifts(csvId);
            Invoice projectInvoice = Helper.GetInvoices(csvData).FirstOrDefault(i => i.CompanyName == projectName);

            string pdfName = csvId + "_" + projectName;
            string body = PdfHelper.GenerateInvoiceHtml(projectInvoice);
            string outputPath = PdfHelper.RenderPDF(body, pdfName);

            var fileService = new FileService(_fileProvider);

            return fileService.GetFileAsStream(outputPath, "application/pdf") ?? (IActionResult)NotFound();
        }

        /// <summary>
        /// Error Page for Non Development Environment
        /// </summary>
        /// <returns></returns>
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
