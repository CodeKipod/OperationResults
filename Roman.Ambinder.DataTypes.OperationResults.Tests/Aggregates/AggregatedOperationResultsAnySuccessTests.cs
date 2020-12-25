using Microsoft.VisualStudio.TestTools.UnitTesting;
using Roman.Ambinder.DataTypes.OperationResults.Aggregates.Evaluation;
using System.Text;
using Roman.Ambinder.DataTypes.OperationResults.Aggregates.Evaluation.Common;

namespace Roman.Ambinder.DataTypes.OperationResults.Tests.Aggregates
{
    [TestClass]
    public class AggregatedOperationResultsAnySuccessTests
    {
        [TestMethod]
        public void AllSuccessfulOpRes_Evaluate_SuccessfulOpRes()
        {
            //Arrange
            var evaluatorUnderTest = GetEvaluator();
            var operationResults = new[] { OperationResult.Successful, OperationResult.Successful };
            var expectedErrorMessage = GetExpectedErrorMessages(operationResults);
            const bool expectedSuccessValue = true;

            //Act
            var actualOpResAggregated = evaluatorUnderTest.Evaluate(operationResults);

            //Assert
            Assert.AreEqual(expectedSuccessValue, actualOpResAggregated.Success);
            Assert.AreEqual(expectedErrorMessage, actualOpResAggregated.ErrorMessage);
        }

        [TestMethod]
        public void AllFailedOpResNoErrorMessage_Evaluate_MatchingFailedOpRes()
        {
            //Arrange
            var evaluatorUnderTest = GetEvaluator();
            var operationResults = new[] { new OperationResult(false), new OperationResult(false) };
            var expectedErrorMessage = GetExpectedErrorMessages(operationResults);
            const bool expectedSuccessValue = false;

            //Act
            var actualOpResAggregated = evaluatorUnderTest.Evaluate(operationResults);

            //Assert
            Assert.AreEqual(expectedSuccessValue, actualOpResAggregated.Success);
            Assert.AreEqual(expectedErrorMessage, actualOpResAggregated.ErrorMessage);
        }

        [TestMethod]
        public void AllFailedOpResWithErrorMessage_Evaluate_MatchingSuccessfulOpRes()
        {
            //Arrange
            var evaluatorUnderTest = GetEvaluator();
            var operationResults = new[] { new OperationResult(false, "Error1"),
                new OperationResult(false, "Error2") };
            var expectedErrorMessage = GetExpectedErrorMessages(operationResults);
            const bool expectedSuccessValue = false;

            //Act
            var actualOpResAggregated = evaluatorUnderTest.Evaluate(operationResults);

            //Assert
            Assert.AreEqual(expectedSuccessValue, actualOpResAggregated.Success);
            Assert.AreEqual(expectedErrorMessage, actualOpResAggregated.ErrorMessage);
        }

        [TestMethod]
        public void SomeFailedSomeSuccessfulOpRes_Evaluate_MatchingFailedOpRes()
        {
            //Arrange
            var evaluatorUnderTest = GetEvaluator();
            var operationResults = new[] { new OperationResult(false, "Error1"),
                new OperationResult(false), OperationResult.Successful };
            var expectedErrorMessage = GetExpectedErrorMessages(operationResults);
            const bool expectedSuccessValue = true;

            //Act
            var actualOpResAggregated = evaluatorUnderTest.Evaluate(operationResults);

            //Assert
            Assert.AreEqual(expectedSuccessValue, actualOpResAggregated.Success);
            Assert.AreEqual(expectedErrorMessage, actualOpResAggregated.ErrorMessage);
        }

        private static string GetExpectedErrorMessages(OperationResult[] operationResults)
        {
            StringBuilder expectedErrorMessagesBuilder = null;
            foreach (var opRes in operationResults)
            {
                if (!opRes)
                {
                    if (!string.IsNullOrEmpty(opRes.ErrorMessage))
                    {
                        expectedErrorMessagesBuilder ??= new StringBuilder();

                        expectedErrorMessagesBuilder.AppendLine(opRes.ErrorMessage);
                    }
                }
            }

            return expectedErrorMessagesBuilder?.ToString();
        }

        private static IAggregatedOperationResultsEvaluator GetEvaluator()
            => AggregatedOperationResultsAnySuccess.Instance;
    }
}