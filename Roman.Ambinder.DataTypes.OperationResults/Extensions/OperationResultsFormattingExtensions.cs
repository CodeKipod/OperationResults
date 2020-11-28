using System.Runtime.CompilerServices;

namespace Roman.Ambinder.DataTypes.OperationResults
{
    public static class OperationResultsFormattingExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToFormattedStringWithCaller<T>(this in OperationResultOf<T> source, [CallerMemberName] string caller = null)
            => ((OperationResult)source).ToFormattedStringWithCaller(caller);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToFormattedStringWithCaller(this in OperationResult source, [CallerMemberName] string caller = null)
            => $"{caller}() - {source.ToFormattedString()}";

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToFormattedString(this in OperationResult source)
            => $"{(source.Success ? "Success" : string.IsNullOrWhiteSpace(source.ErrorMessage) ? "Failed" : $"Failed - {source.ErrorMessage}")}";

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToFormattedString<T>(this in OperationResultOf<T> source)
            => ((OperationResult)source).ToFormattedString();
    }
}