using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Roman.Ambinder.DataTypes.OperationResults.Tests.Extensions
{
    [TestClass]
    public class ExceptionExtensionsTests
    {
        [TestMethod]
        public void ExceptionWithErrorMessage_ResolveErrorMessage_MatchingErrorMessage()
        {
            //Arrange
            const string expectedErrorMessage = "Some error message";
            var exception = new Exception(expectedErrorMessage);

            //Act
            var errorMessage = exception.ResolveErrorMessage();

            //Assert
            Assert.AreEqual(expectedErrorMessage, errorMessage);
        }

        [TestMethod]
        public void ExceptionWithNoErrorMessage_ResolveErrorMessage_MatchingErrorMessage()
        {
            //Arrange
            var exception = new Exception(null);

            //Act
            var errorMessage = exception.ResolveErrorMessage();

            //Assert
            Assert.AreEqual(exception.Message, errorMessage);
        }

        [TestMethod]
        public void ExceptionWithSingleInnerException_ResolveErrorMessage_MatchingErrorMessage()
        {
            //Arrange
            const string expectedErrorMessage = "Some exception message";
            var exception = new Exception("Outer exception", new Exception(expectedErrorMessage));

            //Act
            var errorMessage = exception.ResolveErrorMessage();

            //Assert
            Assert.AreEqual(expectedErrorMessage, errorMessage);
        }

        [TestMethod]
        public void ExceptionWithMultipleInnerExceptions_ResolveErrorMessage_MatchingErrorMessage()
        {
            //Arrange
            const string expectedErrorMessage = "Some exception message";
            var exception = new Exception("1", new Exception("2", new Exception(expectedErrorMessage)));

            //Act
            var errorMessage = exception.ResolveErrorMessage();

            //Assert
            Assert.AreEqual(expectedErrorMessage, errorMessage);
        }
    }
}