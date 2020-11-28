using Roman.Ambinder.DataTypes.OperationResults.Aggregates.Evaluation;

namespace Roman.Ambinder.DataTypes.OperationResults.Aggregates
{
    public readonly struct AggregatedOperationResultsOf<TValue>
    {
        public bool Success { get; }
        public string ErrorMessage { get; }

        public OperationResultOf<TValue>[] OperationResults { get; }

        public AggregatedOperationResultsOf(OperationResultOf<TValue>[] operationResults,
             IAggregatedOperationResultsEvaluator operationResultsAggregateEvaluator)
        {
            var aggregateOpRes = operationResultsAggregateEvaluator.Evaluate(operationResults);

            Success = aggregateOpRes.Success;
            ErrorMessage = aggregateOpRes.ErrorMessage;
            OperationResults = operationResults;
        }

        public static implicit operator bool(in AggregatedOperationResultsOf<TValue> opResults)
            => opResults.Success;

        public override string ToString()
            => $"{nameof(OperationResults)}: {OperationResults?.Length} {nameof(Success)}: {Success}, {nameof(ErrorMessage)}: {ErrorMessage}";
    }
}