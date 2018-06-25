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
        /*
         * ENUMERATION
         * 
         * Allows reading the first or the last scalar from a UTF-8 or UTF-16 input. Return value meanings:
         * 
         * ValidSequence - A well-formed UTF-8 or UTF-16 sequence was encountered and a scalar was parsed.
         * The sequence length parameter contains the number of code units to skip to get to the start of
         * the next sequence.
         * 
         * InvalidSequence - An invalid UTF-8 or UTF-16 sequence was encountered and a scalar was parsed.
         * The sequence length parameter contains the number of code units to skip to get to the start of
         * the next sequence (put another way, the number of bytes to skip to get past the remainder of
         * the invalid sequence). The returned scalar value will be U+FFFD.
         * 
         * IncompleteSequence - An incomplete UTF-8 or UTF-16 sequence was encountered, and more bytes are
         * needed to determine whether the sequence is valid or not. The returned scalar value will be U+FFFD,
         * and the sequence length parameter will be the length of the input. (Ths result is also returned for
         * an empty input string.)
         * 
         * The shape is set up this way to allow this naive conversion approach to work:
         * 
         * while (input.Length != 0) {
         *   PeekFirstScalarUtf8(input, out var scalar, out var sequenceLength);
         *   WriteToOutput(scalar);
         *   input = input.Slice(sequenceLength);
         * }
         * 
         * OperationStatus isn't used in this API because its values don't have the semantic meanings we want.
         * 
         */

        public static SequenceValidity PeekFirstScalarUtf8(ReadOnlySpan<byte> utf8Data, out UnicodeScalar scalar, int sequenceLength) => throw null;
        public static SequenceValidity PeekFirstScalarUtf8(ReadOnlySpan<Char8> utf8Data, out UnicodeScalar scalar, int sequenceLength) => throw null;
        public static SequenceValidity PeekLastScalarUtf8(ReadOnlySpan<byte> utf8Data, out UnicodeScalar scalar, int sequenceLength) => throw null;
        public static SequenceValidity PeekLastScalarUtf8(ReadOnlySpan<Char8> utf8Data, out UnicodeScalar scalar, int sequenceLength) => throw null;

        public static SequenceValidity PeekFirstScalarUtf16(ReadOnlySpan<char> utf8Data, out UnicodeScalar scalar, int sequenceLength) => throw null;
        public static SequenceValidity PeekLastScalarUtf16(ReadOnlySpan<char> utf8Data, out UnicodeScalar scalar, int sequenceLength) => throw null;
    }
}
