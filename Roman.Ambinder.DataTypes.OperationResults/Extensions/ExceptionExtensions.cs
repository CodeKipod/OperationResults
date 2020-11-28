using System;
using System.Runtime.CompilerServices;

namespace Roman.Ambinder.DataTypes.OperationResults
{
    public static class ExceptionExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ResolveErrorMessage(this Exception exception)
        {
            while (exception.InnerException != null)
                exception = exception.InnerException;

            return exception?.Message;
        }

        //=> (exception.InnerException ?? exception).Message;
    }
}