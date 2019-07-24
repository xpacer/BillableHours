using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BillableHours.Controllers;
using BillableHours.DataFactory;
using BillableHours.Models.ViewModels;
using BillableHours.Tests.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Moq;
using Xunit;

namespace BillableHours.Tests
{
    public class ControllerTests
    {
        private readonly Mock<IFileProvider> _mockFileProvider;
        private readonly Mock<IDataProvider> _mockDataProvider;

        public ControllerTests()
        {
            _mockFileProvider = new Mock<IFileProvider>();
            _mockDataProvider = new Mock<IDataProvider>();
            Setup();
        }

        private void Setup()
        {
            _mockFileProvider
               .Setup(c => c.GetFileInfo(It.IsAny<string>()).CreateReadStream())
              .Returns(new MemoryStream(Encoding.UTF8.GetBytes("Test")));

            _mockDataProvider
               .Setup(c => c.GetEmployeeShifts(null))
               .Returns(TestsHelper.GetDummyData());
        }


        [Fact]
        public void TestGeneratePDF_ReturnsAViewResult()
        {
            var controller = new HomeController(_mockFileProvider.Object, _mockDataProvider.Object);
            var result = controller.Projects();

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void TestGeneratePDF_ReturnsADistinctListOfUsers()
        {
            var controller = new HomeController(_mockFileProvider.Object, _mockDataProvider.Object);
            var viewResult = (ViewResult)controller.Projects();

            ProjectsViewModel viewModel = (ProjectsViewModel)viewResult.ViewData.Model;

            Assert.Equal(viewModel.ProjectNames.Count(), viewModel.ProjectNames.Distinct().Count());

        }

        //TODO: Write Tests For Index and Invoice pages
    }
}
