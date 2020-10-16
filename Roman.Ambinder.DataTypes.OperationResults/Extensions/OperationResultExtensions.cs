using System;
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
        public static OperationResultOf<T> AsFailedOpResOf<T>(this Exception exception)
            => new OperationResultOf<T>(success: false, value: default,
                errorMessage: exception.ResolveErrorMessage());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static OperationResult AsFailedOpRes(this Exception exception)
            => new OperationResult(success: false,
                errorMessage: exception.ResolveErrorMessage());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static OperationResult AsFailedOpRes(this string errorMessage)
         => new OperationResult(success: false, errorMessage: errorMessage);

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


        public static void Analayze(this IEnumerable<OperationResult> operationResults,
          out StringBuilder errorsMessageBuilder,
          out int failedOpResCounter,
          out int successfulOpResCounter)
        {
            errorsMessageBuilder = null;
            failedOpResCounter = 0;
            successfulOpResCounter = 0;
            foreach (var opRes in operationResults)
            {
                if (!opRes)
                {
                    ++failedOpResCounter;
                    if (!string.IsNullOrEmpty(opRes.ErrorMessage))
                    {
                        if (errorsMessageBuilder == null)
                            errorsMessageBuilder = new StringBuilder();
                        errorsMessageBuilder.AppendLine(opRes.ErrorMessage);
                    }
                }
                else
                {
                    ++successfulOpResCounter;
                }
            }
        }

        public static void Analayze<TValue>(this IEnumerable<OperationResultOf<TValue>> operationResults,
            out StringBuilder errorsMessageBuilder,
            out int failedOpResCounter,
            out int successfulOpResCounter)
        {
            errorsMessageBuilder = null;
            failedOpResCounter = 0;
            successfulOpResCounter = 0;
            foreach (var opRes in operationResults)
            {
                if (!opRes)
                {
                    ++failedOpResCounter;
                    if (!string.IsNullOrEmpty(opRes.ErrorMessage))
                    {
                        if (errorsMessageBuilder == null)
                            errorsMessageBuilder = new StringBuilder();
                        errorsMessageBuilder.AppendLine(opRes.ErrorMessage);
                    }
                }
                else
                {
                    ++successfulOpResCounter;
                }
            }
        }
    }
}