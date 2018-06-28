namespace System
{
    /// <summary>
    /// Represents an array segment where the backing array has been rented from an <see cref="System.Buffers.ArrayPool{T}"/>.
    /// </summary>
    public struct RentedArray<T> : IDisposable
    {
        public ArraySegment<T> AsArraySegment() => throw null;

        public Memory<T> AsMemory() => throw null;

        public Span<T> AsSpan() => throw null;

        public void Dispose() => throw null;
    }
}
