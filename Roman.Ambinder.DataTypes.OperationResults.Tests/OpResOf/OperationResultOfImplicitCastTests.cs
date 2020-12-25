using Microsoft.VisualStudio.TestTools.UnitTesting;
using Roman.Ambinder.DataTypes.OperationResults.Extensions;

namespace Roman.Ambinder.DataTypes.OperationResults.Tests.OpResOf
{
    [TestClass]
    public class OperationResultOfImplicitCastTests
    {
        [TestMethod]
        public void SuccessfulValueTypeOpRes_ImplicitCast_MatchingExpected()
        {
            //Arrange
            const bool expectedSuccessIndication = true;
            const string expectedErrorMessage = null;
            const int expectedValue = 2;
            var opRes = new OperationResultOf<int>(
                expectedSuccessIndication,
                expectedValue,
                errorMessage: expectedErrorMessage);

            //Act
            bool actualSuccessIndication = opRes;
            int actualValue = opRes.Value;
            OperationResult opResNoValue = opRes;

            //Assert
            AssertAreEqual(opRes,
                expectedSuccessIndication,
                expectedValue,
                actualSuccessIndication,
                actualValue,
                opResNoValue);
        }

        [TestMethod]
        public void FailedValueTypeOpRes_ImplicitCast_MatchingExpected()
        {
            //Arrange
            const bool expectedSuccessIndication = false;
            const string expectedErrorMessage = "Some error message";
            const int expectedValue = default;
            var opRes = new OperationResultOf<int>(
                expectedSuccessIndication,
                expectedValue,
                expectedErrorMessage);

            //Act
            bool actualSuccessIndication = opRes;
            int actualValue = opRes.Value;
            OperationResult opResNoValue = opRes;

            //Assert
            AssertAreEqual(opRes,
                expectedSuccessIndication,
                expectedValue,
                actualSuccessIndication,
                actualValue,
                opResNoValue);
        }

        [TestMethod]
        public void SuccessfulRefTypeOpRes_ImplicitCast_MatchingExpected()
        {
            //Arrange
            const bool expectedSuccessIndication = true;
            const string expectedErrorMessage = null;
            const string expectedValue = "2";
            var opRes = new OperationResultOf<string>(
                expectedSuccessIndication,
                expectedValue,
                errorMessage: expectedErrorMessage);

            //Act
            bool actualSuccessIndication = opRes;
            string actualValue = opRes.Value;
            OperationResult opResNoValue = opRes;

            //Assert
            AssertAreEqual(opRes,
                expectedSuccessIndication,
                expectedValue,
                actualSuccessIndication,
                actualValue,
                opResNoValue);
        }

        [TestMethod]
        public void FailedRefTypeOpRes_ImplicitCast_MatchingExpected()
        {
            //Arrange
            const bool expectedSuccessIndication = false;
            const string expectedErrorMessage = "Some error message";
            const string expectedValue = default;
            var opRes = new OperationResultOf<string>(
                expectedSuccessIndication,
                expectedValue,
                expectedErrorMessage);

            //Act
            bool actualSuccessIndication = opRes;
            string actualValue = opRes.Value;
            OperationResult opResNoValue = opRes;

            //Assert
            AssertAreEqual(opRes,
                expectedSuccessIndication,
                expectedValue,
                actualSuccessIndication,
                actualValue,
                opResNoValue);
        }

        private static void AssertAreEqual<T>(
            OperationResultOf<T> opRes,
            bool expectedSuccessIndication,
            T expectedValue,
            bool actualSuccessIndication,
            T actualValue,
            OperationResult actualOpRes)
        {
            Assert.AreEqual(expectedSuccessIndication, actualSuccessIndication);
            Assert.AreEqual(expectedValue, actualValue);
            Assert.AreEqual(opRes.Success, actualOpRes.Success);
            Assert.AreEqual(opRes.ErrorMessage, actualOpRes.ErrorMessage);
        }

        [TestMethod]
        public void SuccessfulOpRes_ImplicitCastToBool_True()
        {
            //Arrange
            var opRes = new OperationResultOf<int>(success: true, value: 10);

            //Act
            bool success = opRes;

            //Assert
            Assert.IsTrue(success);
        }

        [TestMethod]
        public void SuccessfulOpRes_ImplicitOperationResult_PropertiesMatch()
        {
            //Arrange
            var opResWithValue = new OperationResultOf<int>(success: true, value: 10);

            //Act
            OperationResult opRes = opResWithValue;

            //Assert
            Assert.AreEqual(opResWithValue.Success, opRes.Success);
            Assert.AreEqual(opResWithValue.ErrorMessage, opRes.ErrorMessage);
        }

        [TestMethod]
        public void FailedOpRes_ImplicitCastToBool_False()
        {
            //Arrange
            var opRes = new OperationResultOf<int>(success: false);

            //Act
            bool success = opRes;

            //Assert
            Assert.IsFalse(success);
        }

        [TestMethod]
        public void FailedOpRes_ImplicitOperationResult_PropertiesMatch()
        {
            //Arrange
            var opResWithValue = new OperationResultOf<int>(success: false,
                errorMessage: "Some error  message ");

            //Act
            OperationResult opRes = opResWithValue;

            //Assert
            Assert.AreEqual(opResWithValue.Success, opRes.Success);
            Assert.AreEqual(opResWithValue.ErrorMessage, opRes.ErrorMessage);
        }

        [TestMethod]
        public void ValueType_AsSuccessfulOpRes_ValueMatchingSuccessfulOpResult()
        {
            //Arrange
            const int value = 10;

            //Act
            var opRes = value.AsSuccessfulOpRes();

            //Assert
            Assert.IsTrue(opRes.Success);
            Assert.AreEqual(value, opRes.Value);
        }

        [TestMethod]
        public void ReferenceType_AsSuccessfulOpRes_ValueMatchingSuccessfulOpResult()
        {
            //Arrange
            var names = new[] { "1", "2" };

            //Act
            var opRes = names.AsSuccessfulOpRes();

            //Assert
            Assert.IsTrue(opRes.Success);
            Assert.AreSame(names, opRes.Value);
        }

        [TestMethod]
        public void NullReferenceType_AsSuccessfulOpRes_ValueMatchingSuccessfulOpResult()
        {
            //Arrange
            const string[] names = null;

            //Act
            var opRes = names.AsSuccessfulOpRes();

            //Assert
            Assert.IsTrue(opRes.Success);
            Assert.AreSame(names, opRes.Value);
        }
    }
}