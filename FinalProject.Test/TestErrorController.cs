using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using FinalProject.Controllers;

namespace FinalProject.Tests
{
    public class TestErrorController
    {
        private readonly ErrorController _controller;

        public TestErrorController()
        {
            _controller = new ErrorController();
        }

        [Fact]
        public void StatusCode_404_ReturnsNotFoundView()
        {
            var result = _controller.HandleStatusCode(404) as ViewResult;

            Assert.NotNull(result);
            Assert.Equal("NotFound", result.ViewName);
            Assert.Equal("Page Not Found", result.ViewData["Title"]);
        }

        [Fact]
        public void StatusCode_Other_ReturnsErrorView()
        {
            var result = _controller.HandleStatusCode(500) as ViewResult;

            Assert.NotNull(result);
            Assert.Equal("Error", result.ViewName);
            Assert.Equal("Error", result.ViewData["Title"]);
        }

        [Fact]
        public void ServerError_ReturnServerErrorView()
        {
            var result = _controller.HandleServerError() as ViewResult;

            Assert.NotNull(result);
            Assert.Equal("ServerError", result.ViewName);
        }
    }
}
