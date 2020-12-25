using System;
using System.Collections.Generic;
using Roman.Ambinder.DataTypes.OperationResults.Extensions;

namespace Roman.Ambinder.DataTypes.OperationResults.Collections
{
    public class ReadonlyCollectionOpResOf<TItem>
    {
        // [Obsolete("Deprecated due to readability concerns.Use extension method OperationResultExtensions.AsFailedOpResOf")]
        public ReadonlyCollectionOpResOf(Exception ex)
            : this(errorMessage: ex.ResolveErrorMessage()) { }

        //[Obsolete("Deprecated due to readability concerns.Use extension method OperationResultExtensions.AsFailedOpResOf")]
        public ReadonlyCollectionOpResOf(string errorMessage)
            : this(success: false, errorMessage: errorMessage) { }

        public ReadonlyCollectionOpResOf(bool success, in IReadOnlyCollection<TItem> values = default, string errorMessage = null)
        {
            Success = success;
            Values = values;
            ErrorMessage = errorMessage;
        }

        public static implicit operator bool(in ReadonlyCollectionOpResOf<TItem> opRes)
            => opRes.Success;

        public static implicit operator OperationResult(in ReadonlyCollectionOpResOf<TItem> opRes)
            => new OperationResult(opRes.Success, opRes.ErrorMessage);

        public override string ToString()
            => $"{nameof(Success)}: {Success}, {nameof(Values)}: {Values}, {nameof(ErrorMessage)}: {ErrorMessage}";

        /// <summary>
        ///
        /// </summary>
        public bool Success { get; }

        /// <summary>
        ///
        /// </summary>
        public IReadOnlyCollection<TItem> Values { get; }

        /// <summary>
        ///
        /// </summary>
        public string ErrorMessage { get; }
    }

}