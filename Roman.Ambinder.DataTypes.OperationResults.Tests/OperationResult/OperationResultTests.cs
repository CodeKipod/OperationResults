using System;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Roman.Ambinder.DataTypes.OperationResults.Tests.OperationResult
{
    [TestClass]
    public class OperationResultTests
    {
        [TestMethod]
        public void CtorWithExceptionNoErrorMessage_FailedOpResWithMatchingErrorMessage()
        {
            //Arrange 
            var exception = new Exception();

            //Act 
            var opRes = new OperationResults.OperationResult(exception);

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
            var opRes = new OperationResults.OperationResult(exception);

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
            var opRes = new OperationResults.OperationResult(exception);

            //Assert
            Assert.IsFalse(opRes.Success);
            // ReSharper disable once PossibleNullReferenceException
            Assert.AreEqual(exception.InnerException.Message, opRes.ErrorMessage);
        }

        [TestMethod]
        public void SuccessfulOpRes_ImplicitCastToBool_True()
        {
            //Arrange 
            var opRes = new OperationResults.OperationResult(success: true);

            //Act 
            bool success = opRes;

            //Assert
            Assert.IsTrue(success);
        }

        [TestMethod]
        public void FailedOpRes_ImplicitCastToBool_False()
        {
            //Arrange 
            var opRes = new OperationResults.OperationResult(success: false);

            //Act 
            bool success = opRes;

            //Assert
            Assert.IsFalse(success);
        }

        [TestMethod]
        public void MultiSuccessfulOperationResults_AggregateToSingleOpRes_SuccessfulOpRes()
        {
            //Arrange 
            var operationResults = new[]
                {new OperationResults.OperationResult(true), new OperationResults.OperationResult(true), new OperationResults.OperationResult(true)};

            //Act 
            var opRes = operationResults.AggregateToSingleOpRes();

            //Arrange
            Assert.IsTrue(opRes.Success);
            Assert.IsNull(opRes.ErrorMessage);
        }

        [TestMethod]
        public void MultiSuccessfulAndFailedOperationResults_AggregateToSingleOpRes_FailedOpResWithCombinedErrorMessage()
        {
            //Arrange 
            var operationResults = new[]
                { 
                    new OperationResults.OperationResult(true),
                    new OperationResults.OperationResult(false,"Some error message 1"),
                    new OperationResults.OperationResult(true),
                    new OperationResults.OperationResult(false,"Some error message 2"),
                };
            var sb = new StringBuilder();
            foreach (var opRes in operationResults.Where(opRes => opRes.ErrorMessage != null))
                sb.AppendLine(opRes.ErrorMessage);
            var expectedErrorMessages = sb.ToString();

            //Act 
            var aggregatedOpRes = operationResults.AggregateToSingleOpRes();

            //Arrange
            Assert.IsFalse(aggregatedOpRes.Success);
            Assert.IsNotNull(aggregatedOpRes.ErrorMessage);
            Assert.AreEqual(expectedErrorMessages,aggregatedOpRes.ErrorMessage);
        }
    }
}
