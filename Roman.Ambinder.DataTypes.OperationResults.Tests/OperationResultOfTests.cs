using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Roman.Ambinder.DataTypes.OperationResults.Tests
{
    [TestClass]
    public class OperationResultOfTests
    {
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
