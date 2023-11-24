using BarrocIntensTestlLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[TestClass]
public class AuthenticationManagerTests
{
    [TestMethod]
    public void Authenticate_ValidCredentials_ReturnsTrue()
    {
        // Arrange
        var mockAuthService = new Mock<IAuthenticationService>();
        mockAuthService.Setup(authService => authService.Authenticate(It.IsAny<string>(), It.IsAny<string>())).Returns(true);

        var authManager = new AuthenticationManager(mockAuthService.Object);

        // Act
        bool result = authManager.Authenticate("validUsername", "validPassword");

        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void Authenticate_InvalidCredentials_ReturnsFalse()
    {
        // Arrange
        var mockAuthService = new Mock<IAuthenticationService>();
        mockAuthService.Setup(authService => authService.Authenticate(It.IsAny<string>(), It.IsAny<string>())).Returns(false);

        var authManager = new AuthenticationManager(mockAuthService.Object);

        // Act
        bool result = authManager.Authenticate("invalidUsername", "invalidPassword");

        // Assert
        Assert.IsFalse(result);
    }
}