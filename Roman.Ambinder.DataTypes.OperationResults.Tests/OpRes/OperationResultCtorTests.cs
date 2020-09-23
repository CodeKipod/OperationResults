using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Roman.Ambinder.DataTypes.OperationResults.Tests.OpRes
{
    [TestClass]
    public class OperationResultCtorTests
    {
        [TestMethod]
        public void SuccessfulOperationPropertyResult_ValuesMatch()
        {
            //Act 
            var opRes = OperationResult.Successful;

            //Assert
            Assert.IsTrue(opRes.Success);
            Assert.IsNull(opRes.ErrorMessage);
        }

        [TestMethod]
        public void FullCtor_OperationWithMatchingValues()
        {
            //Arrange
            const bool expectedSuccessIndication = false;
            const string expectedErrorMessage = "Some error message";

            //Act
            var opRes = new OperationResult(
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
            var opRes = new OperationResult(new Exception(expectedErrorMessage));

            //Assert
            Assert.AreEqual(expectedSuccessIndication, opRes.Success, $"Expected {nameof(opRes.Success)} '{opRes.Success}' to match '{expectedSuccessIndication}'");
            Assert.AreEqual(expectedErrorMessage, opRes.ErrorMessage, $"Expected {nameof(opRes.ErrorMessage)} '{opRes.ErrorMessage}' to match '{expectedErrorMessage}'");
        }

        [TestMethod]
        public void ErrorMessageCtor_FailedOperationResultWithMatchingErrorMessageCreated()
        {
            //Arrange
            const bool expectedSuccessIndication = false;
            const string expectedErrorMessage = "Some error message";

            //Act
            var opRes = new OperationResult(expectedErrorMessage);

            //Assert
            Assert.AreEqual(expectedSuccessIndication, opRes.Success, $"Expected {nameof(opRes.Success)} '{opRes.Success}' to match '{expectedSuccessIndication}'");
            Assert.AreEqual(expectedErrorMessage, opRes.ErrorMessage, $"Expected {nameof(opRes.ErrorMessage)} '{opRes.ErrorMessage}' to match '{expectedErrorMessage}'");
        }

        [TestMethod]
        public void ExceptionInnerExceptionNoErrorMessageCtor_FailedOpResWithMatchingErrorMessage()
        {
            //Arrange 
            var exception = new Exception(null, innerException: new Exception());

            //Act 
            var opRes = new OperationResult(exception);

            //Assert
            Assert.IsFalse(opRes.Success);
            // ReSharper disable once PossibleNullReferenceException
            Assert.AreEqual(exception.InnerException.Message, opRes.ErrorMessage);
        }

        [TestMethod]
        public void WithExceptionInnerExceptionWithErrorMessageCtor_FailedOpResWithMatchingErrorMessage()
        {
            //Arrange 
            var exception = new Exception("Error message 1", innerException: new Exception("Error message 2"));

            //Act 
            var opRes = new OperationResult(exception);

            //Assert
            Assert.IsFalse(opRes.Success);
            // ReSharper disable once PossibleNullReferenceException
            Assert.AreEqual(exception.InnerException.Message, opRes.ErrorMessage);
        }
    }
}
