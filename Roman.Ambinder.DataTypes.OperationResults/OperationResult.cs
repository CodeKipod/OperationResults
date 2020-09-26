using System;

namespace Roman.Ambinder.DataTypes.OperationResults
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance",
        "U2U1004:Public value types should implement equality",
        Justification = "<Pending>")]
    public readonly struct OperationResult
    {
        // ReSharper disable once InconsistentNaming
        private static readonly OperationResult _successful = new OperationResult(success: true);

        public static ref readonly OperationResult Successful => ref _successful;

        //[Obsolete("Deprecated due to readability concerns. Use extension method OperationResultExtensions.AsFailedOpRes")]
        public OperationResult(Exception ex)
            : this(errorMessage: ex.ResolveErrorMessage()) { }

       // [Obsolete("Deprecated due to readability concerns. Use extension method OperationResultExtensions.AsFailedOpRes")]
        public OperationResult(string errorMessage)
            : this(success: false, errorMessage) { }

        public OperationResult(bool success, string errorMessage = null)
        {
            Success = success;
            ErrorMessage = errorMessage;
        }

        public static implicit operator bool(in OperationResult opRes) => opRes.Success;

        public override string ToString()
            => $"{nameof(Success)}: {Success}, {nameof(ErrorMessage)}: {ErrorMessage}";

        /// <summary>
        ///
        /// </summary>
        public bool Success { get; }

        /// <summary>
        ///
        /// </summary>
        public string ErrorMessage { get; }
    }
}