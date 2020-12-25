namespace Roman.Ambinder.DataTypes.OperationResults.Aggregates.Evaluation.Common
{
    public interface IAggregatedOperationResultsEvaluator
    {
        OperationResult Evaluate<TValue>(OperationResultOf<TValue>[] operationResults);

        OperationResult Evaluate(OperationResult[] operationResults);
    }
}