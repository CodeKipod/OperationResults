using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Roman.Ambinder.DataTypes.OperationResults
{
    public static class OperationResultExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static OperationResultOf<T> AsSuccessfulOpRes<T>(this T value)
            => new OperationResultOf<T>(success: true, value: value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static OperationResultOf<TBase> AsSuccessfulOpRes<TBase, TImpl>(this TImpl value)
         where TImpl : TBase
            => new OperationResultOf<TBase>(success: true, value: value);


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static OperationResultOf<T> AsFailedOpResOf<T>(this string errorMessage)
            => new OperationResultOf<T>(success: false, value: default, errorMessage: errorMessage);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static OperationResult AggregateToSingleOpRes(
            this IEnumerable<OperationResult> operationResults)
        {
            StringBuilder errorMessagesBuilder = null;
            var allSuccessful = true;
            foreach (var opRes in operationResults)
            {
                if (!opRes)
                {
                    allSuccessful = false;
                    if (!string.IsNullOrWhiteSpace(opRes.ErrorMessage))
                    {
                        if (errorMessagesBuilder == null)
                            errorMessagesBuilder = new StringBuilder();

                        errorMessagesBuilder.AppendLine(opRes.ErrorMessage);
                    }
                }
            }

            return allSuccessful
                ? OperationResult.Successful
                : new OperationResult(success: false, errorMessage: errorMessagesBuilder?.ToString());
        }
    }
}