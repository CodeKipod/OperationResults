namespace Roman.Ambinder.DataTypes.OperationResults.Aggregates.Evaluation
{
    public static class AggreagetedOperationResultsEvaluators
    {
        public static IAggregatedOperationResultsEvaluator All { get; } =
            AggregatedOperationResultsAllSuccess.Instance;

        public static IAggregatedOperationResultsEvaluator Any { get; } =
            AggregatedOperationResultsAnySuccess.Instance;
    }
}
