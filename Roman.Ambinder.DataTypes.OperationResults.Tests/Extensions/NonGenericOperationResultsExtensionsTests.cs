using System;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Roman.Ambinder.DataTypes.OperationResults.Tests.Extensions
{
    [TestClass]
    public class NonGenericOperationResultsExtensionsTests
    {
        [TestMethod]
        public void ErrorMessage_AsFailedOpResult_MatchingFailedOperationResult()
        {
            //Arrange
            const string errorMessage = "Some error message";

            //Act 
            var opRes = errorMessage.AsFailedOpRes();

            //Assert
            Assert.IsFalse(opRes.Success);
            Assert.AreEqual(errorMessage, opRes.ErrorMessage);
        }


        [TestMethod]
        public void Exception_AsFailedOpResult_MatchingFailedOperationResult()
        {
            //Arrange
            const string errorMessage = "Some error message";
            var exception = new Exception(errorMessage);

            //Act 
            var opRes = exception.AsFailedOpRes();

            //Assert
            Assert.IsFalse(opRes.Success);
            Assert.AreEqual(errorMessage, opRes.ErrorMessage);
        }

        [TestMethod]
        public void ExceptionWithInternalException_AsFailedOpResult_MatchingFailedOperationResult()
        {
            //Arrange
            const string errorMessage = "Some error message 2";
            var exception = new Exception("external exception message", new Exception(errorMessage));

            //Act 
            var opRes = exception.AsFailedOpRes();

            //Assert
            Assert.IsFalse(opRes.Success);
            Assert.AreEqual(errorMessage, opRes.ErrorMessage);
        }

        [TestMethod]
        public void MultiSuccessfulOperationResults_AggregateToSingleOpRes_SuccessfulOpRes()
        {
            //Arrange 
            var operationResults = new[]
                {new OperationResult(true), new OperationResult(true), new OperationResult(true)};

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
                new OperationResult(success: true),
                new OperationResult(success: false,errorMessage: "Some error message 1"),
                new OperationResult(success: true),
                new OperationResult(success: false,errorMessage: "Some error message 2"),
            };
            var sb = new StringBuilder();
            foreach (var opRes in operationResults.Where(predicate: opRes => opRes.ErrorMessage != null))
                sb.AppendLine(value: opRes.ErrorMessage);
            var expectedErrorMessages = sb.ToString();

            //Act 
            var aggregatedOpRes = operationResults.AggregateToSingleOpRes();

            //Arrange
            Assert.IsFalse(condition: aggregatedOpRes.Success);
            Assert.IsNotNull(value: aggregatedOpRes.ErrorMessage);
            Assert.AreEqual(expected: expectedErrorMessages, actual: aggregatedOpRes.ErrorMessage);
        }
    }
}
