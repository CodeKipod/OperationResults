namespace Roman.Ambinder.DataTypes.OperationResults.Tests.TestEntities
{
    public class TestInterfaceImpl : ITestInterface
    {
        public TestInterfaceImpl(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }
}