namespace Roman.Ambinder.DataTypes.OperationResults.Aggregates.Evaluation
{
    public abstract class BaseAggregatedOperationResultsEvaluator
    {
        public OperationResult Evaluate(OperationResult[] operationResults,
            AggregationPolicyEnum aggregationPolicy)
        {
            operationResults.Analayze(out var errorsMessageBuilder,
             out var failedOpResCounter,
             out var successfulOpResCounter);

            var success = failedOpResCounter == 0 ||
                ResolveSuccessByPolicy(operationResults, successfulOpResCounter, aggregationPolicy);

            return new OperationResult(success, errorMessage: errorsMessageBuilder?.ToString());
        }

        private static bool ResolveSuccessByPolicy(OperationResult[] operationResults,
            int successfulOpResCounter,
            AggregationPolicyEnum aggregationPolicy)
        {
            if (aggregationPolicy == AggregationPolicyEnum.Any)
                return successfulOpResCounter > 0;

            return successfulOpResCounter == operationResults.Length;
        }

        public OperationResult Evaluate<TValue>(OperationResultOf<TValue>[] operationResults,
           AggregationPolicyEnum aggregationPolicy)
        {
            operationResults.Analayze(out var errorsMessageBuilder,
             out var failedOpResCounter,
             out var successfulOpResCounter);

            var success = failedOpResCounter == 0 ||
                ResolveSuccessByPolicy(operationResults, successfulOpResCounter, aggregationPolicy);

            return new OperationResult(success, errorMessage: errorsMessageBuilder?.ToString());
        }

        private static bool ResolveSuccessByPolicy<TValue>(OperationResultOf<TValue>[] operationResults,
            int successfulOpResCounter,
            AggregationPolicyEnum aggregationPolicy)
        {
            if (aggregationPolicy == AggregationPolicyEnum.Any)
                return successfulOpResCounter > 0;

            return successfulOpResCounter == operationResults.Length;
        }
    }
}