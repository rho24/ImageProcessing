using System.Collections.Generic;

namespace ImageProcessing.Core
{
    public interface IIndexedSequence<out T> : IEnumerable<T>
    {
        T this[int i] { get; }
    }
}