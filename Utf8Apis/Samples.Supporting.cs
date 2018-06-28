using System;
using System.Buffers;
using System.Collections.Generic;
using System.Text;

class JsonEncoder
{
    public static JsonEncoder Default => throw null;

    public OperationStatus Encode(
        ReadOnlySpan<byte> inUtf8,
        Span<byte> outUtf8,
        out int bytesConsumed,
        out int bytesWritten,
        InvalidSequenceBehavior invalidSequenceBehavior = InvalidSequenceBehavior.Fail,
        bool isFinalChunk = true) => throw null;

    public RentedArray<byte> Encode(
        ReadOnlySpan<byte> inUtf8,
        InvalidSequenceBehavior invalidSequenceBehavior = InvalidSequenceBehavior.Fail,
        ArrayPool<byte> arrayPool = default) => throw null;
}
