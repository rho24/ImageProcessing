using System;
using System.Collections;
using System.Collections.Generic;

namespace ImageProcessing.Core
{
    public class RememberingEnumerable<T> : IEnumerable<T>
    {
        private readonly RememberingEnumerator _enumerator;

        public RememberingEnumerable(IEnumerable<T> sequence) {
            _enumerator = new RememberingEnumerator(sequence.GetEnumerator());
        }

        public IEnumerator<T> GetEnumerator() {
            return _enumerator;
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        public void FinallyDispose() {
            _enumerator.FinallyDispose();
        }

        #region Nested type: RememberingEnumerator

        public class RememberingEnumerator : IEnumerator<T>
        {
            private readonly IEnumerator<T> _enumerator;

            public RememberingEnumerator(IEnumerator<T> enumerator) {
                _enumerator = enumerator;
            }

            public void Dispose() {

            }

            public bool MoveNext() {
                return _enumerator.MoveNext();
            }

            public void Reset() {
                throw new NotImplementedException();
            }

            public T Current {
                get { return _enumerator.Current; }
            }

            object IEnumerator.Current {
                get { return Current; }
            }

            public void FinallyDispose() {
                _enumerator.Dispose();
            }
        }

        #endregion
    }
}