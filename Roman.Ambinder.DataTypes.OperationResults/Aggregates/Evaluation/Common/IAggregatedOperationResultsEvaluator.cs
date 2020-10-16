using System.Threading;

namespace Roman.Ambinder.DataTypes.OperationResults.Aggregates.Evaluation
{
    public interface IAggregatedOperationResultsEvaluator
    {
        OperationResult Evaluate<TValue>(OperationResultOf<TValue>[] operationResults);

        OperationResult Evaluate(OperationResult[] operationResults);
    }
}
