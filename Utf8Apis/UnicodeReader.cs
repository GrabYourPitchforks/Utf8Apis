using System;
using System.Buffers;
using System.Collections.Generic;
using System.Text;

namespace System.Text
{
    /// <summary>
    /// Provides low-level methods for reading data directly from Unicode strings.
    /// </summary>
    public static class UnicodeReader {
        public static SequenceValidity PeekFirstScalarUtf8(ReadOnlySpan<byte> utf8Data, out UnicodeScalar scalar, int sequenceLength) => throw null;
        public static SequenceValidity PeekFirstScalarUtf8(ReadOnlySpan<Char8> utf8Data, out UnicodeScalar scalar, int sequenceLength) => throw null;
        public static SequenceValidity PeekLastScalarUtf8(ReadOnlySpan<byte> utf8Data, out UnicodeScalar scalar, int sequenceLength) => throw null;
        public static SequenceValidity PeekLastScalarUtf8(ReadOnlySpan<Char8> utf8Data, out UnicodeScalar scalar, int sequenceLength) => throw null;

        public static SequenceValidity PeekFirstScalarUtf16(ReadOnlySpan<char> utf8Data, out UnicodeScalar scalar, int sequenceLength) => throw null;
        public static SequenceValidity PeekLastScalarUtf16(ReadOnlySpan<char> utf8Data, out UnicodeScalar scalar, int sequenceLength) => throw null;
    }
}
