using System;
using System.Runtime.CompilerServices;
using Roman.Ambinder.DataTypes.OperationResults.Extensions;

namespace Roman.Ambinder.DataTypes.OperationResults
{
    public readonly struct NamedOperationResultOf<T>
    {
        public NamedOperationResultOf(Exception ex,
            [CallerMemberName] string operationName = null) :
           this(errorMessage: ex.ResolveErrorMessage(), operationName: operationName)
        { }

        public NamedOperationResultOf(string errorMessage,
           [CallerMemberName] string operationName = null) :
          this(success: false, value: default, errorMessage: errorMessage, operationName: operationName)
        { }

        public NamedOperationResultOf(bool success, T value,
            string errorMessage = null,
            [CallerMemberName] string operationName = null)
        {
            OperationName = operationName;
            Success = success;
            Value = value;
            ErrorMessage = errorMessage;
        }

        public override string ToString()
             => $"{nameof(OperationName)}:{OperationName}, {nameof(Success)}: {Success}, {nameof(Value)}: {Value}, {nameof(ErrorMessage)}: {ErrorMessage}";


        public static implicit operator bool(in NamedOperationResultOf<T> wrapper)
          => wrapper.Success;

        public static implicit operator OperationResult(in NamedOperationResultOf<T> wrapper)
            => new OperationResult(wrapper.Success, wrapper.ErrorMessage);

        /// <summary>
        ///
        /// </summary>
        public string OperationName { get; }

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