using Roman.Ambinder.DataTypes.OperationResults.Aggregates.Evaluation.Common;

namespace Roman.Ambinder.DataTypes.OperationResults.Aggregates
{
    public readonly struct AggregatedOperationResults
    {
        public bool Success { get; }
        public string ErrorMessage { get; }

        public OperationResult[] OperationResults { get; }

        public AggregatedOperationResults(OperationResult[] operationResults,
          IAggregatedOperationResultsEvaluator operationResultsAggregateEvaluator)
        {
            var aggregateOpRes = operationResultsAggregateEvaluator.Evaluate(operationResults);

            Success = aggregateOpRes.Success;
            ErrorMessage = aggregateOpRes.ErrorMessage;
            OperationResults = operationResults;
        }

        public static implicit operator bool(in AggregatedOperationResults opRes) => opRes.Success;

        public override string ToString()
            => $"{nameof(OperationResults)}: {OperationResults?.Length} {nameof(Success)}: {Success}, {nameof(ErrorMessage)}: {ErrorMessage}";
    }
}