using Roman.Ambinder.DataTypes.OperationResults.Aggregates.Evaluation.Common;

namespace Roman.Ambinder.DataTypes.OperationResults.Aggregates.Evaluation
{
    public class AggregatedOperationResultsAllSuccess :
        BaseAggregatedOperationResultsEvaluator,
        IAggregatedOperationResultsEvaluator
    {
        private AggregatedOperationResultsAllSuccess()
        {
        }

        public OperationResult Evaluate(OperationResult[] operationResults)
            => Evaluate(operationResults, AggregationPolicyEnum.All);

        public OperationResult Evaluate<TValue>(OperationResultOf<TValue>[] operationResults)
             => Evaluate(operationResults, AggregationPolicyEnum.All);

        public static IAggregatedOperationResultsEvaluator Instance { get; } =
            new AggregatedOperationResultsAllSuccess();
    }
}