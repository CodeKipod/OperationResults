using Roman.Ambinder.DataTypes.OperationResults.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Roman.Ambinder.DataTypes.OperationResults
{
    public static class ReadonlyCollectionOpResExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadonlyCollectionOpResOf<T> AsSuccessfulReadonlyOpResCollectionOf<T>(this List<T> values)
          => new ReadonlyCollectionOpResOf<T>(success: true, values);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadonlyCollectionOpResOf<T> AsSuccessfulReadonlyOpResCollectionOf<T>(this ICollection<T> values)
        => new ReadonlyCollectionOpResOf<T>(success: true, values.ToArray());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadonlyCollectionOpResOf<T> AsSuccessfulReadonlyOpResCollectionOf<T>(this IList<T> values)
         => new ReadonlyCollectionOpResOf<T>(success: true, values.ToArray());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadonlyCollectionOpResOf<T> AsSuccessfulReadonlyOpResCollectionOf<T>(this T[] values)
         => new ReadonlyCollectionOpResOf<T>(success: true, values);
    }
}