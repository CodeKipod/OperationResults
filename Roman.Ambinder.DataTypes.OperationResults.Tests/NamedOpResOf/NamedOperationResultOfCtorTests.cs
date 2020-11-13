using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Runtime.CompilerServices;

namespace Roman.Ambinder.DataTypes.OperationResults.Tests.NamedOpResOf
{
    [TestClass]
    public class NamedNamedOperationResultOfCtorTests
    {
        [TestMethod]
        public void ValueTypeUseFullCtor_MatchingValuesOperationResultCreated()
        {
            //Arrange
            const bool expectedSuccessIndication = false;
            const string expectedErrorMessage = "Some error message";
            const int expectedValue = 2;

            //Act
            var opRes = new NamedOperationResultOf<int>(
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
            var opRes = new NamedOperationResultOf<string>(
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
            var opRes = new NamedOperationResultOf<int>(
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
            var opRes = new NamedOperationResultOf<string>(
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
            var opRes = new NamedOperationResultOf<int>(exception);

            //Assert
            AssertValuesMatchExpected(opRes,
                  expectedSuccessIndication: false,
                  expectedErrorMessage: exception.Message,
                  expectedValue: default);
        }

        [TestMethod]
        public void CtorWithExceptionInnerExceptionNoErrorMessage_FailedOpResWithMatchingErrorMessage()
        {
            //Arrange
            var exception = new Exception(null, innerException: new Exception());

            //Act
            var opRes = new NamedOperationResultOf<int>(exception);

            //Assert
            AssertValuesMatchExpected(opRes,
                expectedSuccessIndication: false,
                expectedErrorMessage: exception.InnerException.Message,
                expectedValue: default);
        }

        [TestMethod]
        public void CtorWithExceptionInnerExceptionWithErrorMessage_FailedOpResWithMatchingErrorMessage()
        {
            //Arrange
            var exception = new Exception("Error message 1",
                innerException: new Exception("Error message 2"));

            //Act
            var opRes = new NamedOperationResultOf<int>(exception);

            //Assert
            AssertValuesMatchExpected(opRes,
              expectedSuccessIndication: false,
              expectedErrorMessage: exception.InnerException.Message,
              expectedValue: default);
        }

        [TestMethod]
        public void CtorWithExceptionWithErrorMessage_FailedOpResWithMatchingErrorMessage()
        {
            //Arrange
            const string errorMessage = "Some error message";
            var exception = new Exception(errorMessage);

            //Act
            var opRes = new NamedOperationResultOf<int>(exception);

            //Assert
            AssertValuesMatchExpected(opRes,
                false,
                errorMessage,
                default);
        }

        private static void AssertValuesMatchExpected<TValue>(
            NamedOperationResultOf<TValue> opRes,
            bool expectedSuccessIndication,
            string expectedErrorMessage,
            TValue expectedValue,
            [CallerMemberName] string expecteOpName = null)
        {
            Assert.AreEqual(expectedValue, opRes.Value, $"Expected {nameof(opRes.Value)} '{opRes.Value}' to match '{expectedValue}'");
            Assert.AreEqual(expectedSuccessIndication, opRes.Success, $"Expected {nameof(opRes.Success)} '{opRes.Success}' to match '{expectedSuccessIndication}'");
            Assert.AreEqual(expectedErrorMessage, opRes.ErrorMessage, $"Expected {nameof(opRes.ErrorMessage)} '{opRes.ErrorMessage}' to match '{expectedErrorMessage}'");
            Assert.AreEqual(expecteOpName, opRes.OperationName);
        }
    }
}