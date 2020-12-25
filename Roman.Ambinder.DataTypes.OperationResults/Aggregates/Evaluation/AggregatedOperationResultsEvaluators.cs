using Roman.Ambinder.DataTypes.OperationResults.Aggregates.Evaluation.Common;

namespace Roman.Ambinder.DataTypes.OperationResults.Aggregates.Evaluation
{
    public static class AggregatedOperationResultsEvaluators
    {
        public static IAggregatedOperationResultsEvaluator All { get; } =
            AggregatedOperationResultsAllSuccess.Instance;

        public static IAggregatedOperationResultsEvaluator Any { get; } =
            AggregatedOperationResultsAnySuccess.Instance;
    }
}