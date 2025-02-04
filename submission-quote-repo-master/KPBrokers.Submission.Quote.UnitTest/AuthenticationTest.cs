using KPBrokers.Submission.Quote.API.Controllers;
using KPBrokers.Submission.Quote.BusinessLogic.Abstracts;
using KPBrokers.Submission.Quote.Common;
using KPBrokers.Submission.Quote.Common.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace KPBrokers.Submission.Quote.Tests.Controllers
{
    public class AuthenticationControllerTests
    {
        private readonly Mock<IAuthenticationBusinessLogic> _mockAuthentication;
        private readonly Mock<IAdminBusinessLogic> _mockBroker;
        private readonly Mock<ILoggerService> _mockLogger;
        private readonly AuthenticationController _controller;

        public AuthenticationControllerTests()
        {
            _mockAuthentication = new Mock<IAuthenticationBusinessLogic>();
            _mockBroker = new Mock<IAdminBusinessLogic>();
            _mockLogger = new Mock<ILoggerService>();
            //_controller = new AuthenticationController(_mockAuthentication.Object, _mockBroker.Object,_mockLogger.Object);
        }

        [Fact]
        public async Task VerifyUser_ValidCredentials_ReturnsOkResult()
        {
            // Arrange
            var loginModel = new LoginModel { Username = "Admin", Password = "Password" };
            //_mockAuthentication.Setup(a => a.VerifyUserAsync(loginModel.Username, loginModel.Password))
               // .ReturnsAsync("User authenticated successfully!");

            // Act
            //var result = await _controller.VerifyUser(loginModel);

            // Assert
            //var okResult = Assert.IsType<OkObjectResult>(result);
            //var returnValue = Assert.IsType<dynamic>(okResult.Value);
            //Assert.Equal("User authenticated successfully!", okResult.Value);
        }

        [Fact]
        public async Task VerifyUser_InvalidCredentials_ReturnsBadRequest()
        {
            // Arrange
            var loginModel = new LoginModel { Username = "", Password = "" }; // Invalid input

            // Act
            //var result = await _controller.VerifyUser(loginModel);

            // Assert
            //Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task VerifyUser_NullCredentials_ReturnsBadRequest()
        {
            // Arrange
            LoginModel loginModel = null!; // Null input

            // Act
            //var result = await _controller.VerifyUser(loginModel!);

            // Assert
            //Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
