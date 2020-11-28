using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Runtime.CompilerServices;

namespace Roman.Ambinder.DataTypes.OperationResults.Tests.NamedOpRes
{
    [TestClass]
    public class NamedOperationResultCtorTests  
    {
        [TestMethod]
        public void SuccessfulOperationPropertyResult_ValuesMatch()
        {
            //Act
            var opRes = new NamedOperationResult(true);

            //Assert
            Assert.IsTrue(opRes.Success);
            Assert.IsNull(opRes.ErrorMessage);
            Assert.AreEqual(
                nameof(SuccessfulOperationPropertyResult_ValuesMatch),
                opRes.OperationName);
        }

        [TestMethod]
        public void FullCtor_OperationWithMatchingValues()
        {
            //Arrange
            const bool expectedSuccessIndication = false;
            const string expectedErrorMessage = "Some error message";

            //Act
            var opRes = new NamedOperationResult(
                success: expectedSuccessIndication,
                errorMessage: expectedErrorMessage);

            //Assert
            Assert.AreEqual(expectedSuccessIndication, opRes.Success, $"Expected {nameof(opRes.Success)} '{opRes.Success}' to match '{expectedSuccessIndication}'");
            Assert.AreEqual(expectedErrorMessage, opRes.ErrorMessage, $"Expected {nameof(opRes.ErrorMessage)} '{opRes.ErrorMessage}' to match '{expectedErrorMessage}'");
            AssertOperationNameMatchCaller(opRes);
        }

        [TestMethod]
        public void ExceptionCtor_FailedOperationResultWithMatchingErrorMessageCreated()
        {
            //Arrange
            const bool expectedSuccessIndication = false;
            const string expectedErrorMessage = "Some error message";

            //Act
            var opRes = new NamedOperationResult(new Exception(expectedErrorMessage));

            //Assert
            Assert.AreEqual(expectedSuccessIndication, opRes.Success, $"Expected {nameof(opRes.Success)} '{opRes.Success}' to match '{expectedSuccessIndication}'");
            Assert.AreEqual(expectedErrorMessage, opRes.ErrorMessage, $"Expected {nameof(opRes.ErrorMessage)} '{opRes.ErrorMessage}' to match '{expectedErrorMessage}'");
            AssertOperationNameMatchCaller(opRes);
        }

        [TestMethod]
        public void ErrorMessageCtor_FailedOperationResultWithMatchingErrorMessageCreated()
        {
            //Arrange
            const bool expectedSuccessIndication = false;
            const string expectedErrorMessage = "Some error message";

            //Act
            var opRes = new NamedOperationResult(expectedErrorMessage);

            //Assert
            Assert.AreEqual(expectedSuccessIndication, opRes.Success, $"Expected {nameof(opRes.Success)} '{opRes.Success}' to match '{expectedSuccessIndication}'");
            Assert.AreEqual(expectedErrorMessage, opRes.ErrorMessage, $"Expected {nameof(opRes.ErrorMessage)} '{opRes.ErrorMessage}' to match '{expectedErrorMessage}'");
            AssertOperationNameMatchCaller(opRes);
        }

        [TestMethod]
        public void ExceptionInnerExceptionNoErrorMessageCtor_FailedOpResWithMatchingErrorMessage()
        {
            //Arrange
            var exception = new Exception(null, innerException: new Exception());

            //Act
            var opRes = new NamedOperationResult(exception);

            //Assert
            Assert.IsFalse(opRes.Success);
            // ReSharper disable once PossibleNullReferenceException
            Assert.AreEqual(exception.InnerException.Message, opRes.ErrorMessage);
            AssertOperationNameMatchCaller(opRes);
        }

        [TestMethod]
        public void WithExceptionInnerExceptionWithErrorMessageCtor_FailedOpResWithMatchingErrorMessage()
        {
            //Arrange
            var exception = new Exception("Error message 1", innerException: new Exception("Error message 2"));

            //Act
            var opRes = new NamedOperationResult(exception);

            //Assert
            Assert.IsFalse(opRes.Success);
            // ReSharper disable once PossibleNullReferenceException
            Assert.AreEqual(exception.InnerException.Message, opRes.ErrorMessage);
            AssertOperationNameMatchCaller(opRes);
        }

        private static void AssertOperationNameMatchCaller(
            in NamedOperationResult opRes,
            [CallerMemberName] string expectedOperationName = null)
        {
            Assert.AreEqual(
               expectedOperationName,
               opRes.OperationName);
        }
    }
}