using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ImageProcessing.Core
{
    public static class IndexedSequence
    {
        public static IIndexedSequence<T> Create<T>(List<T> items) {
            return new ListSequence<T>(items);
        }

        public static IIndexedSequence<T> Create<T>(IEnumerable<T> items) {
            if (items is List<T>)
                return new ListSequence<T>((List<T>) items);
            return new EnumerableSequence<T>(items);
        }

        #region Nested type: EnumerableSequence

        public class EnumerableSequence<T> : IIndexedSequence<T>
        {
            private readonly IEnumerable<T> _items;
            private List<T> _itemsAsList;

            public EnumerableSequence(IEnumerable<T> items) {
                _items = items;
            }

            public IEnumerator<T> GetEnumerator() {
                return _itemsAsList == null ? _items.GetEnumerator() : _itemsAsList.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator() {
                return GetEnumerator();
            }

            public T this[int i] {
                get {
                    if (_itemsAsList == null) _itemsAsList = _items.ToList();
                    return _itemsAsList[i];
                }
            }
        }

        #endregion

        #region Nested type: ListSequence

        internal class ListSequence<T> : IIndexedSequence<T>
        {
            private readonly List<T> _items;

            public ListSequence(List<T> items) {
                _items = items;
            }

            public IEnumerator<T> GetEnumerator() {
                return _items.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator() {
                return GetEnumerator();
            }

            public T this[int i] {
                get { return _items[i]; }
            }
        }

        #endregion
    }
}