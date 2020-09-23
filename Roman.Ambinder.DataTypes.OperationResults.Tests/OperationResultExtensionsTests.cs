using Microsoft.VisualStudio.TestTools.UnitTesting;

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

        public interface ITestInterface
        {
            int Id { get; }
        }

        public class TestInterfaceImpl : ITestInterface
        {
            public TestInterfaceImpl(int id)
            {
                Id = id;
            }

            public int Id { get; }
        }
    }


}
