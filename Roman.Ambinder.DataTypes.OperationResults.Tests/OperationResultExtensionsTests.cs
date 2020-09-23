using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Roman.Ambinder.DataTypes.OperationResults.Tests.TestEntities;

namespace Roman.Ambinder.DataTypes.OperationResults.Tests
{
    [TestClass]
    public class OperationResultExtensionsTests
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
                new OperationResult(true),
                new OperationResult(false,"Some error message 1"),
                new OperationResult(true),
                new OperationResult(false,"Some error message 2"),
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
            Assert.AreEqual(expectedErrorMessages, aggregatedOpRes.ErrorMessage);
        }

    
    }
}
