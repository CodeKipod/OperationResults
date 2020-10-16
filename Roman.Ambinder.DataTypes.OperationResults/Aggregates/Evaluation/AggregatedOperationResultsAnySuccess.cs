using Roman.Ambinder.DataTypes.OperationResults.Aggregates.Evaluation;

namespace Roman.Ambinder.DataTypes.OperationResults.Aggregates.Evaluation
{
    public class AggregatedOperationResultsAnySuccess :
       BaseAggregatedOperationResultsEvaluator,
       IAggregatedOperationResultsEvaluator
    {
        private AggregatedOperationResultsAnySuccess() { }

        public OperationResult Evaluate(OperationResult[] operationResults)
          => Evaluate(operationResults, AggregationPolicyEnum.Any);

        public OperationResult Evaluate<TValue>(OperationResultOf<TValue>[] operationResults)
          => Evaluate(operationResults, AggregationPolicyEnum.Any);

        public static IAggregatedOperationResultsEvaluator Instance { get; } =
            new AggregatedOperationResultsAnySuccess();
    }
}
