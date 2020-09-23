using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Roman.Ambinder.DataTypes.OperationResults.Tests.OpResOf
{
    [TestClass]
    public class OperationResultOfCtorTests
    {
        [TestMethod]
        public void ValueTypeUseFullCtor_MatchingValuesOperationResultCreated()
        {
            //Arrange
            const bool expectedSuccessIndication = false;
            const string expectedErrorMessage = "Some error message";
            const int expectedValue = 2;

            //Act
            var opRes = new OperationResultOf<int>(
                expectedSuccessIndication,
                expectedValue,
                expectedErrorMessage);

            //Assert
            AssertValuesMatchExpected(opRes,
                expectedSuccessIndication,
                expectedErrorMessage,
                expectedValue);
        }

        [TestMethod]
        public void RefTypeUseFullCtor_MatchingValuesOperationResultCreated()
        {
            //Arrange
            const bool expectedSuccessIndication = false;
            const string expectedErrorMessage = "Some error message";
            const string expectedValue = "2";

            //Act
            var opRes = new OperationResultOf<string>(
                expectedSuccessIndication,
                expectedValue,
                expectedErrorMessage);

            //Assert
            AssertValuesMatchExpected(opRes,
                expectedSuccessIndication,
                expectedErrorMessage,
                expectedValue);
        }

        [TestMethod]
        public void ValueTypeUseExceptionCtor_FailedOperationResultWithMatchingMessageCreated()
        {
            //Arrange
            const bool expectedSuccessIndication = false;
            const string expectedErrorMessage = "Some error message";
            const int expectedValue = default;

            //Act
            var opRes = new OperationResultOf<int>(
                new Exception(expectedErrorMessage));

            //Assert
            AssertValuesMatchExpected(opRes,
                expectedSuccessIndication,
                expectedErrorMessage,
                expectedValue);
        }

        [TestMethod]
        public void RefTypeValueTypeUseExceptionCtor_FailedOperationResultWithMatchingMessageCreated()
        {
            //Arrange
            const bool expectedSuccessIndication = false;
            const string expectedErrorMessage = "Some error message";
            const string expectedValue = default;

            //Act
            var opRes = new OperationResultOf<string>(
                new Exception(expectedErrorMessage));

            //Assert
            AssertValuesMatchExpected(opRes,
                expectedSuccessIndication,
                expectedErrorMessage,
                expectedValue);
        }

        [TestMethod]
        public void CtorWithExceptionNoErrorMessage_FailedOpResWithMatchingErrorMessage()
        {
            //Arrange 
            var exception = new Exception();

            //Act 
            var opRes = new OperationResultOf<int>(exception);

            //Assert
            Assert.IsFalse(opRes.Success);
            Assert.AreEqual(exception.Message, opRes.ErrorMessage);
        }

        [TestMethod]
        public void CtorWithExceptionInnerExceptionNoErrorMessage_FailedOpResWithMatchingErrorMessage()
        {
            //Arrange 
            var exception = new Exception(null, innerException: new Exception());

            //Act 
            var opRes = new OperationResultOf<int>(exception);

            //Assert
            Assert.IsFalse(opRes.Success);
            // ReSharper disable once PossibleNullReferenceException
            Assert.AreEqual(exception.InnerException.Message, opRes.ErrorMessage);
        }

        [TestMethod]
        public void CtorWithExceptionInnerExceptionWithErrorMessage_FailedOpResWithMatchingErrorMessage()
        {
            //Arrange 
            var exception = new Exception("Error message 1", innerException: new Exception("Error message 2"));

            //Act 
            var opRes = new OperationResultOf<int>(exception);

            //Assert
            Assert.IsFalse(opRes.Success);
            // ReSharper disable once PossibleNullReferenceException
            Assert.AreEqual(exception.InnerException.Message, opRes.ErrorMessage);
        }

        [TestMethod]
        public void CtorWithExceptionWithErrorMessage_FailedOpResWithMatchingErrorMessage()
        {
            //Arrange 
            var exception = new Exception("Some error message");

            //Act 
            var opRes = new OperationResultOf<int>(exception);

            //Assert
            Assert.IsFalse(opRes.Success);
            Assert.AreEqual(exception.Message, opRes.ErrorMessage);
        }


        private static void AssertValuesMatchExpected<TValue>(
            OperationResultOf<TValue> opRes,
            bool expectedSuccessIndication,
            string expectedErrorMessage,
            TValue expectedValue)
        {
            Assert.AreEqual(expectedValue, opRes.Value, $"Expected {nameof(opRes.Value)} '{opRes.Value}' to match '{expectedValue}'");
            Assert.AreEqual(expectedSuccessIndication, opRes.Success, $"Expected {nameof(opRes.Success)} '{opRes.Success}' to match '{expectedSuccessIndication}'");
            Assert.AreEqual(expectedErrorMessage, opRes.ErrorMessage, $"Expected {nameof(opRes.ErrorMessage)} '{opRes.ErrorMessage}' to match '{expectedErrorMessage}'");
        }
    }
}