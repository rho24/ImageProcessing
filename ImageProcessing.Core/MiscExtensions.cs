using System.Collections.Generic;
using System.Linq;

namespace ImageProcessing.Core
{
    public static class MiscExtensions
    {
        public static IIndexedSequence<T> ToIndexedSequence<T>(this List<T> items) {
            return IndexedSequence.Create(items);
        }

        public static IIndexedSequence<T> ToIndexedSequence<T>(this IEnumerable<T> items) {
            return IndexedSequence.Create(items);
        }

        public static IIndexedSequence<IIndexedSequence<T>> ToIndexedSequence<T>(this IEnumerable<IEnumerable<T>> items) {
            return items.Select(ToIndexedSequence).ToIndexedSequence();
        }

        public static IIndexedSequence<IIndexedSequence<T>> ToIndexedSequence<T>(this List<List<T>> items) {
            return items.Select(ToIndexedSequence).ToIndexedSequence();
        }
    }
}