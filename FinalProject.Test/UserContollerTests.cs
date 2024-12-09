using Moq;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using FinalProject.Controllers;
using FinalProject.Models;
using System;
using System.Linq;

namespace FinalProject.Tests
{
    public class UserControllerTests
    {
        private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private readonly Mock<ISession> _sessionMock;
        private readonly UserController _controller;
        private readonly Mock<UserContext> _mockContext;

        public UserControllerTests()
        {
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            _sessionMock = new Mock<ISession>();
            var httpContextMock = new Mock<HttpContext>();
            httpContextMock.Setup(ctx => ctx.Session).Returns(_sessionMock.Object);
            _httpContextAccessorMock.Setup(ctx => ctx.HttpContext).Returns(httpContextMock.Object);

            var options = new DbContextOptionsBuilder<UserContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()) 
                .Options;

            _mockContext = new Mock<UserContext>(options);
            _mockContext.Setup(m => m.Users).Returns(CreateTestUsersDbSet());

            _controller = new UserController(_mockContext.Object);
        }

        private DbSet<User> CreateTestUsersDbSet()
        {
            var users = new[]
            {
                new User { UserID = 1, Username = "tate.padilla", Password = "Test1", BodyWeight = 220, P4PStrength = 50, Premium = true },
                new User { UserID = 2, Username = "tommy.wells", Password = "Test2", BodyWeight = 190, P4PStrength = 50, Premium = false },
                new User { UserID = 3, Username = "caden.heidebrink", Password = "Test3", BodyWeight = 190, P4PStrength = 50, Premium = false }
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<User>>();
            mockDbSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(users.Provider);
            mockDbSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(users.Expression);
            mockDbSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(users.ElementType);
            mockDbSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(users.GetEnumerator());
            return mockDbSet.Object;
        }

        [Fact]
        public void GoodLogin_RedirectsToUserHome()
        {
            // Arrange
            var mockUser = new User
            {
                UserID = 1,
                Username = "tate.padilla",
                Password = "Test1",
                Premium = true
            };

            var users = new List<User> { mockUser }.AsQueryable();

            var mockSet = new Mock<DbSet<User>>();
            mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(users.Provider);
            mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(users.Expression);
            mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(users.ElementType);
            mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(users.GetEnumerator());

            var mockContext = new Mock<UserContext>();
            mockContext.Setup(c => c.Users).Returns(mockSet.Object);

            var _sessionMock = new Mock<ISession>();
            var _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            var httpContextMock = new Mock<HttpContext>();
            httpContextMock.Setup(ctx => ctx.Session).Returns(_sessionMock.Object);
            _httpContextAccessorMock.Setup(ctx => ctx.HttpContext).Returns(httpContextMock.Object);

            var controller = new UserController(mockContext.Object);
            controller.ControllerContext.HttpContext = httpContextMock.Object;

            // Act
            var result = controller.Login("tate.padilla", "Test1", true);

            // Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("UserHome", redirectResult.ActionName);  
            Assert.Equal("Exercise", redirectResult.ControllerName);  
            Assert.Equal(1, redirectResult.RouteValues["id"]);  
        }


        [Fact]
        public void BadLogin_IndexWithErrorMessage()
        {
            // Arrange
            var username = "faksdfasdf";
            var password = "ffd34";
            var premium = false;

            // Act
            var result = _controller.Login(username, password, premium);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("~Views/Home/Index", viewResult.ViewName);
        }
    }
}
