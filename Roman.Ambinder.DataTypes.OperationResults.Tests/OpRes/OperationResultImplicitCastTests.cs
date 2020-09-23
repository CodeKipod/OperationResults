using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Roman.Ambinder.DataTypes.OperationResults.Tests.OpRes
{
    [TestClass]
    public class OperationResultImplicitCastTests
    {
        [TestMethod]
        public void SuccessfulOpRes_ImplicitCastToBool_True()
        {
            //Arrange 
            var opRes = new OperationResult(success: true);

            //Act 
            bool success = opRes;

            //Assert
            Assert.IsTrue(success);
        }

        [TestMethod]
        public void FailedOpRes_ImplicitCastToBool_False()
        {
            //Arrange 
            var opRes = new OperationResult(success: false);

            //Act 
            bool success = opRes;

            //Assert
            Assert.IsFalse(success);
        }
    }
}
