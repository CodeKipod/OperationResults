using Microsoft.VisualStudio.TestTools.UnitTesting;
using Roman.Ambinder.DataTypes.OperationResults.Extensions;

namespace Roman.Ambinder.DataTypes.OperationResults.Tests.Extensions
{
    [TestClass]
    public class OperationResultsFormattingExtensionsTests
    {
        [TestMethod]
        public void SuccessfulOpRes_ToFormattedString_MatchingString()
        {
            //Arrange
            var opRes = OperationResult.Successful;
            const string expectedText = "Success";

            //Act
            var text = opRes.ToFormattedString();

            //Assert
            Assert.AreEqual(expectedText, text);
        }

        [TestMethod]
        public void FailedOpResWithErrorMessage_ToFormattedString_MatchingString()
        {
            //Arrange
            var opRes = new OperationResult(false, "Error message");
            const string expectedText = "Failed - Error message";

            //Act
            var text = opRes.ToFormattedString();

            //Assert
            Assert.AreEqual(expectedText, text);
        }

        [TestMethod]
        public void FailedOpResNoErrorMessage_ToFormattedString_MatchingString()
        {
            //Arrange
            var opRes = new OperationResult(false);
            const string expectedText = "Failed";

            //Act
            var text = opRes.ToFormattedString();

            //Assert
            Assert.AreEqual(expectedText, text);
        }
    }
}