using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Roman.Ambinder.DataTypes.OperationResults.Tests.OperationResult
{
    [TestClass]
    public class OperationResultCtorTests
    {
        [TestMethod]
        public void FullCtor_OperationWithMatchingValues()
        {
            //Arrange
            const bool expectedSuccessIndication = false;
            const string expectedErrorMessage = "Some error message";

            //Act
            var opRes = new OperationResults.OperationResult(
                success: expectedSuccessIndication,
                errorMessage: expectedErrorMessage);

            //Assert
            Assert.AreEqual(expectedSuccessIndication, opRes.Success, $"Expected {nameof(opRes.Success)} '{opRes.Success}' to match '{expectedSuccessIndication}'");
            Assert.AreEqual(expectedErrorMessage, opRes.ErrorMessage, $"Expected {nameof(opRes.ErrorMessage)} '{opRes.ErrorMessage}' to match '{expectedErrorMessage}'");
        }

        [TestMethod]
        public void ExceptionCtor_FailedOperationResultWithMatchingErrorMessageCreated()
        {
            //Arrange
            const bool expectedSuccessIndication = false;
            const string expectedErrorMessage = "Some error message";

            //Act
            var opRes = new OperationResults.OperationResult(new Exception(expectedErrorMessage));

            //Assert
            Assert.AreEqual(expectedSuccessIndication, opRes.Success, $"Expected {nameof(opRes.Success)} '{opRes.Success}' to match '{expectedSuccessIndication}'");
            Assert.AreEqual(expectedErrorMessage, opRes.ErrorMessage, $"Expected {nameof(opRes.ErrorMessage)} '{opRes.ErrorMessage}' to match '{expectedErrorMessage}'");
        }
    }
}
