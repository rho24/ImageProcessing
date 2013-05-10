using System.Collections;
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

        public static IEnumerable<T> Cache<T>(this IEnumerable<T> sequence) {
            return new CachedEnumerable<T>(sequence);
        }
    }

    public class CachedEnumerable<T> : IEnumerable<T>
    {
        private readonly IEnumerable<T> _sequence;
        private List<T> _list;

        public CachedEnumerable(IEnumerable<T> sequence) {
            _sequence = sequence;
        }

        public IEnumerator<T> GetEnumerator() {
            if (_list == null)
                _list = _sequence.ToList();
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
    }
}