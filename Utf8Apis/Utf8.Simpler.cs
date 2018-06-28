using System;
using System.Buffers;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace System.Text
{
    // "Simpler" classes for performing UTF-8 operations
    public static partial class Utf8
    {
        public static RentedArray<char> ConvertToUtf16(
            ReadOnlySpan<byte> inUtf8,
            InvalidSequenceBehavior invalidSequenceBehavior = InvalidSequenceBehavior.Fail,
            ArrayPool<char> arrayPool = default) => throw null;

        public static RentedArray<byte> ConvertFromUtf16(
            ReadOnlySpan<char> inUtf16,
            InvalidSequenceBehavior invalidSequenceBehavior = InvalidSequenceBehavior.Fail,
            ArrayPool<byte> arrayPool = default) => throw null;

        public static RentedArray<byte> ToLower(
            ReadOnlySpan<byte> inUtf8,
            CultureInfo culture,
            ArrayPool<byte> arrayPool = default) => throw null;

        public static RentedArray<byte> ToLowerInvariant(
            ReadOnlySpan<byte> inUtf8,
            ArrayPool<byte> arrayPool = default) => throw null;
    }
}
