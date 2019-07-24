using System;
using BillableHours.Models.Data;
using IronPdf;

namespace BillableHours.Helpers
{
    public static class PdfHelper
    {
        /// <summary>
        /// Render PDF based on specified Html Body
        /// </summary>
        /// <param name="htmlBody">Html Body</param>
        /// <param name="fileName">File Name</param>
        /// <returns></returns>
        public static string RenderPDF(string htmlBody, string fileName)
        {
            var renderer = new HtmlToPdf();

            renderer.PrintOptions.FirstPageNumber = 1;
            renderer.PrintOptions.Header.FontFamily = "Helvetica,Arial";
            renderer.PrintOptions.Title = fileName;

            renderer.PrintOptions.CssMediaType = PdfPrintOptions.PdfCssMediaType.Screen;
            renderer.PrintOptions.EnableJavaScript = true;

            var PDF = renderer.RenderHtmlAsPdf(htmlBody);

            var outputPath = "Media/" + fileName + ".pdf";
            PDF.SaveAs(outputPath);

            return outputPath;
        }

        /// <summary>
        ///  Render PDF
        /// </summary>
        /// <param name="htmlBody"></param>
        /// <returns></returns>
        public static string RenderPDF(string htmlBody)
        {
            return RenderPDF(htmlBody, "BillableHours");
        }

        /// <summary>
        /// Generates Invoice Html for PDF
        /// </summary>
        /// <param name="invoice"></param>
        /// <returns></returns>
        public static string GenerateInvoiceHtml(Invoice invoice)
        {
            var html = "<html><head>";
            html += "<style> body {font-size: 22px;} table {border-collapse: collapse; width: 100%; font-size: 22px;}";
            html += " thead {color: white;background: #3090C7;}";
            html += " th, td {border-bottom: 1px dotted black;padding: 15px;text-align: center;}>";
            html += " tfoot {border-bottom: 1px solid black;border-top: 1px solid black;} .title {text-align: center;} </style></head>";
            html += "<body><div class = 'container'>";
            html += "<h1 class='title'>Foo and Bar Law Firm Invoice</h1><br />";
            html += "<span>Company Name: <strong>" + invoice.CompanyName + "</strong></span><br/><br/>";
            html += "<table>";
            html += "<thead>";
            html += "<tr>";
            html += "<th>Employee ID</th>";
            html += "<th>Number Of Hours</th>";
            html += "<th>Unit Price</th>";
            html += "<th>Cost</th>";
            html += "</tr></thead><tbody>";

            foreach (var employee in invoice.Employees)
            {
                html += "<tr>";
                html += "<td>" + employee.Id + "</td>";
                html += "<td>" + employee.NumberOfHours.ToString() + "</td>";
                html += "<td>" + employee.UnitPrice.ToString() + "</td>";
                html += "<td>" + employee.TotalCost.ToString() + "</td>";
                html += "</tr>";
            }

            html += "</tbody><tfoot><tr> <td></td><td></td><td><strong>Total:</strong></td>";
            html += "<td>" + invoice.TotalCost + "</td>";
            html += "</tr></tfoot></table></div></body></html>";

            return html;
        }
    }
}
