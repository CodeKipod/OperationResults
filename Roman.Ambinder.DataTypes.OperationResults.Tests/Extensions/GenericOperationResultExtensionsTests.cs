using Microsoft.VisualStudio.TestTools.UnitTesting;
using Roman.Ambinder.DataTypes.OperationResults.Tests.TestEntities;
using System;

namespace Roman.Ambinder.DataTypes.OperationResults.Tests.Extensions
{
    [TestClass]
    public class GenericOperationResultExtensionsTests
    {
        [TestMethod]
        public void ValueType_AsSuccessfulOpRes_SuccessfulOperationResultOf()
        {
            //Arrange
            const int value = 4;

            //Act
            var opRes = value.AsSuccessfulOpRes();

            //Assert
            Assert.IsTrue(opRes.Success);
            Assert.IsNull(opRes.ErrorMessage);
            Assert.AreEqual(value, opRes.Value);
        }

        [TestMethod]
        public void RefType_AsSuccessfulOpRes_SuccessfulOperationResultOf()
        {
            //Arrange
            const string value = "4";

            //Act
            var opRes = value.AsSuccessfulOpRes();

            //Assert
            Assert.IsTrue(opRes.Success);
            Assert.IsNull(opRes.ErrorMessage);
            Assert.AreEqual(value, opRes.Value);
        }

        [TestMethod]
        public void Exception_AsFailedOpResultOfValueType_MatchingFailedOperationResult()
        {
            //Arrange
            const string errorMessage = "Some error message 2";
            var exception = new Exception(errorMessage);

            //Act
            var opRes = exception.AsFailedOpResOf<int>();

            //Assert
            Assert.IsFalse(opRes.Success);
            Assert.AreEqual(errorMessage, opRes.ErrorMessage);
            Assert.AreEqual(default(int), opRes.Value);
        }

        [TestMethod]
        public void ExceptionWithInternalException_AsFailedOpResultOfValueType_MatchingFailedOperationResult()
        {
            //Arrange
            const string errorMessage = "Some error message 2";
            var exception = new Exception("external exception message", new Exception(errorMessage));

            //Act
            var opRes = exception.AsFailedOpResOf<int>();

            //Assert
            Assert.IsFalse(opRes.Success);
            Assert.AreEqual(errorMessage, opRes.ErrorMessage);
            Assert.AreEqual(default(int), opRes.Value);
        }

        [TestMethod]
        public void Exception_AsFailedOpResultOfRefType_MatchingFailedOperationResult()
        {
            //Arrange
            const string errorMessage = "Some error message 2";
            var exception = new Exception(errorMessage);

            //Act
            var opRes = exception.AsFailedOpResOf<string>();

            //Assert
            Assert.IsFalse(opRes.Success);
            Assert.AreEqual(errorMessage, opRes.ErrorMessage);
            Assert.AreEqual(default(string), opRes.Value);
        }

        [TestMethod]
        public void ExceptionWithInternalException_AsFailedOpResultOfRefType_MatchingFailedOperationResult()
        {
            //Arrange
            const string errorMessage = "Some error message";
            var exception = new Exception(errorMessage);

            //Act
            var opRes = exception.AsFailedOpResOf<string>();

            //Assert
            Assert.IsFalse(opRes.Success);
            Assert.AreEqual(errorMessage, opRes.ErrorMessage);
            Assert.AreEqual(default(string), opRes.Value);
        }

        [TestMethod]
        public void ErrorMessage_AsFailedOpResultOfRefType_MatchingFailedOperationResult()
        {
            //Arrange
            const string errorMessage = "Some error message";

            //Act
            var opRes = errorMessage.AsFailedOpResOf<string>();

            //Assert
            Assert.IsFalse(opRes.Success);
            Assert.AreEqual(errorMessage, opRes.ErrorMessage);
            Assert.AreEqual(default(string), opRes.Value);
        }

        [TestMethod]
        public void ErrorMessage_AsFailedOpResultOfValueType_MatchingFailedOperationResult()
        {
            //Arrange
            const string errorMessage = "Some error message";

            //Act
            var opRes = errorMessage.AsFailedOpResOf<int>();

            //Assert
            Assert.IsFalse(opRes.Success);
            Assert.AreEqual(errorMessage, opRes.ErrorMessage);
            Assert.AreEqual(default(int), opRes.Value);
        }

        [TestMethod]
        public void RefType_AsSuccessfulOpResOfBaseType_SuccessfulOperationResultOf()
        {
            //Arrange
            var value = new TestInterfaceImpl(4);

            //Act
            OperationResultOf<ITestInterface> opRes =
                value.AsSuccessfulOpRes<ITestInterface>();

            //Assert
            Assert.IsTrue(opRes.Success);
            Assert.IsNull(opRes.ErrorMessage);
            Assert.AreEqual(value, opRes.Value);
        }
    }
}