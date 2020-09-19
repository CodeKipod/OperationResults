using System;

namespace Roman.Ambinder.DataTypes.OperationResults
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "U2U1004:Public value types should implement equality", Justification = "<Pending>")]
    public readonly struct OperationResultOf<T>
    {
        public OperationResultOf(Exception ex)
            : this(errorMessage: (ex.InnerException ?? ex).Message) { }

        public OperationResultOf(string errorMessage)
            : this(success: false, errorMessage: errorMessage) { }

        public OperationResultOf(bool success, in T value = default, string errorMessage = null)
        {
            Success = success;
            Value = value;
            ErrorMessage = errorMessage;
        }

        public static implicit operator bool(in OperationResultOf<T> opRes)
            => opRes.Success;

        public static implicit operator OperationResult(in OperationResultOf<T> opRes)
            => new OperationResult(opRes.Success, opRes.ErrorMessage);

        public override string ToString()
            => $"{nameof(Success)}: {Success}, {nameof(Value)}: {Value}, {nameof(ErrorMessage)}: {ErrorMessage}";

        /// <summary>
        ///
        /// </summary>
        public bool Success { get; }

        /// <summary>
        ///
        /// </summary>
        public T Value { get; }

        /// <summary>
        ///
        /// </summary>
        public string ErrorMessage { get; }
    }
}