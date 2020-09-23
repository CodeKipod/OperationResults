using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Roman.Ambinder.DataTypes.OperationResults.Tests.OperationResultOf
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
                new System.Exception(expectedErrorMessage));

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
                new System.Exception(expectedErrorMessage));

            //Assert
            AssertValuesMatchExpected(opRes,
                expectedSuccessIndication,
                expectedErrorMessage,
                expectedValue);
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