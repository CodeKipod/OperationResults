using System;
using System.Runtime.CompilerServices;

namespace Roman.Ambinder.DataTypes.OperationResults
{
    public readonly struct NamedOperationResult
    {
        public NamedOperationResult(Exception ex,
             [CallerMemberName] string operationName = null) :
            this(errorMessage: ex.ResolveErrorMessage(), operationName: operationName)
        { }

        public NamedOperationResult(string errorMessage,
           [CallerMemberName] string operationName = null) :
          this(success: false, errorMessage: errorMessage, operationName: operationName)
        { }

        public NamedOperationResult(bool success, string errorMessage = null,
            [CallerMemberName] string operationName = null)
        {
            OperationName = operationName;
            Success = success;
            ErrorMessage = errorMessage;
        }

        public override string ToString()
           => $"{nameof(OperationName)}:{OperationName}, {nameof(Success)}: {Success}, {nameof(ErrorMessage)}: {ErrorMessage}";

        public static implicit operator bool(in NamedOperationResult wrapper)
            => wrapper.Success;

        public static implicit operator OperationResult(in NamedOperationResult wrapper)
            => new OperationResult(wrapper.Success, wrapper.ErrorMessage);

        public string OperationName { get; }

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