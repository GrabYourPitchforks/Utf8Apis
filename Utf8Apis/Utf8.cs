using System;
using System.Buffers;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace System.Text
{
    // Contains helper methods for performing UTF-8 data conversion.
    // Alternative naming: class Utf8Convert in namespace System.Text.Utf8
    public static partial class Utf8
    {
        /*
         * TRANSCODING ROUTINES
         */

        // Converts UTF8 -> UTF16.
        // Assumes the input is a standalone UTF-8 string.
        // Returns the slice of the destination buffer which was populated with data.
        // Throws on failure (malformed input, destination too small).
        public static Span<char> ConvertToUtf16(
            ReadOnlySpan<byte> inUtf8,
            Span<char> outUtf16,
            InvalidSequenceBehavior invalidSequenceBehavior = InvalidSequenceBehavior.Fail) => throw null;

        // A non-throwing form of ConvertToUtf16.
        // Follows normal OperationStatus-style conventions.
        public static OperationStatus ConvertToUtf16(
            ReadOnlySpan<byte> inUtf8,
            Span<char> outUtf16,
            out int bytesConsumed,
            out int charsWritten,
            InvalidSequenceBehavior invalidSequenceBehavior = InvalidSequenceBehavior.Fail,
            bool isFinalChunk = true) => throw null;

        // Converts to UTF-16 and returns a new System.String instance
        // rather than writing to a span-based output buffer.
        public static string ConvertToString(
            ReadOnlySpan<byte> inUtf8,
            InvalidSequenceBehavior invalidSequenceBehavior = InvalidSequenceBehavior.Fail) => throw null;

        // Converts UTF-16 -> UTF-8.
        // Assumes the input is a standalone UTF-16 string.
        // Returns the slice of the destination buffer which was populated with data.
        // Throws on failure (malformed input, destination too small).
        public static Span<byte> ConvertFromUtf16(
            ReadOnlySpan<char> inUtf16,
            Span<byte> outUtf8,
            InvalidSequenceBehavior invalidSequenceBehavior = InvalidSequenceBehavior.Fail) => throw null;

        // A non-throwing form of ConvertToUtf16.
        // Follows normal OperationStatus-style conventions.
        public static OperationStatus ConvertFromUtf16(
            ReadOnlySpan<char> inUtf16,
            Span<byte> outUtf8,
            out int charsConsumed,
            out int bytesWritten,
            InvalidSequenceBehavior invalidSequenceBehavior = InvalidSequenceBehavior.Fail,
            bool isFinalChunk = true) => throw null;

        /*
         * COMPARISON ROUTINES
         */

        // Returns true iff two UTF-8 strings are equal.
        // Caller can specify comparison mode (ordinal, case-sensitive, culture-aware).
        // Two malformed UTF-8 strings will only ever compare as equal using an ordinal comparer.
        public static bool AreEqual(
            ReadOnlySpan<byte> a,
            ReadOnlySpan<byte> b,
            StringComparison stringComparison = StringComparison.Ordinal) => throw null;

        // Returns true iff the specified UTF-8 and UTF-16 strings are equal.
        // Caller can specify comparison mode (ordinal, case-sensitive, culture-aware).
        // Returns false if either input string is malformed.
        public static bool AreEqual(
            ReadOnlySpan<byte> a,
            ReadOnlySpan<char> b,
            StringComparison stringComparison = StringComparison.Ordinal) => throw null;

        // Returns a comparison between two UTF-8 strings: negative if a should be sorted
        // before b, zero if a and b are equal, and positive if a should be sorted after b.
        // Caller can specify comparison mode (ordinal, case-sensitive, culture-aware).
        public static int Compare(
            ReadOnlySpan<byte> a,
            ReadOnlySpan<byte> b,
            StringComparison stringComparison = StringComparison.Ordinal) => throw null;

        /*
         * TRIM ROUTINES
         */

        // Trims whitespace characters from the beginning and end of the input.
        // Returns a slice of the original input.
        public static ReadOnlySpan<byte> Trim(
            ReadOnlySpan<byte> inUtf8) => throw null;

        // Trims the requested scalar value from the beginning and end of the input.
        // Returns a slice of the original input.
        public static ReadOnlySpan<byte> Trim(
            ReadOnlySpan<byte> inUtf8,
            UnicodeScalar trimScalar) => throw null;

        // Trims the requested scalar values from the beginning and end of the input.
        // Returns a slice of the original input.
        public static ReadOnlySpan<byte> Trim(
            ReadOnlySpan<byte> inUtf8,
            ReadOnlySpan<UnicodeScalar> trimScalars) => throw null;

        // There's no API to get the number of bytes trimmed. Instead, call TrimStart / TrimEnd
        // and compare the length of the returned string against the length of the input string,
        // and this will give the number of bytes trimmed from the start + end.
        //
        // (Assume there are TrimStart / TrimEnd equivalents of Trim.)

        /*
         * SPLIT ROUTINES
         */

        // Returns a ref struct that duck types an enumerator
        public static Utf8SpanSplitEnumerator Split(
            ReadOnlySpan<byte> inUtf8,
            UnicodeScalar splitScalar) => throw null;

        /*
         * TRANSFORMATION ROUTINES
         */

        // Converts UTF8 -> uppercase UTF8 using the supplied CultureInfo (mustn't be null).
        // Assumes the input is a standalone UTF-8 string.
        // Returns the slice of the destination buffer which was populated with data.
        // Throws on failure (malformed input, destination too small).
        public static Span<byte> ToUpper(
            ReadOnlySpan<byte> inUtf8,
            Span<byte> outUtf8,
            CultureInfo culture) => throw null;

        // Convenience method for ToUpper that flows through the invariant culture.
        public static Span<byte> ToUpperInvariant(
            ReadOnlySpan<byte> inUtf8,
            Span<byte> outUtf8) => throw null;

        // A non-throwing form of ToUpper.
        // Follows normal OperationStatus-style conventions.
        public static OperationStatus ToUpper(
            ReadOnlySpan<byte> inUtf8,
            Span<byte> outfUtf8,
            out int bytesConsumed,
            out int bytesWritten,
            CultureInfo culture,
            InvalidSequenceBehavior invalidSequenceBehavior = InvalidSequenceBehavior.Fail,
            bool isFinalChunk = true) => throw null;

        /*
         * VALIDATION AND INSPECTION
         */

        // Returns true iff the input string contains only valid UTF-8 sequences.
        public static bool IsWellFormed(
            ReadOnlySpan<byte> inUtf8) => throw null;

        // Returns the byte index of the first invalid UTF-8 sequence in the input stream,
        // or -1 if no invalid sequence is found. Also returns the UTF-16 char count and
        // total scalar count required to represent the input were it to be transcoded.
        // If the return value is non-negative, the UTF-16 char count and scalar count are
        // the counts up to (but not including) the first invalid UTF-8 sequence.
        public static int GetIndexOfFirstInvalidSequence(
            ReadOnlySpan<byte> inUtf8,
            out int utf16CharCount,
            out int scalarCount) => throw null;

        // Copies UTF-8 input to UTF-8 output, replacing any invalid sequences seen with
        // U+FFFD. The input is assumed to be a standalone string. Throws if the destination
        // buffer is too small to hold the resulting value. Returns the slice of the destination
        // buffer to which the output was written.
        public static Span<byte> ConvertToValidUtf8(
            ReadOnlySpan<byte> inUtf8,
            Span<byte> outUtf8) => throw null;

        // A non-throwing form of ConvertToValidUtf8.
        // Follows normal OperationStatus-style conventions.
        public static OperationStatus ConvertToValidUtf8(
            ReadOnlySpan<byte> inUtf8,
            Span<byte> outUtf8,
            out int bytesConsumed,
            out int bytesWritten,
            bool isFinalChunk = true) => throw null;

        /*
         * ENUMERATION
         */

        // Allows forward enumeration of scalar values from UTF-8 input.
        public static Utf8SpanUnicodeScalarEnumerator EnumerateForward(
            ReadOnlySpan<byte> inUtf8) => throw null;

        // Allows forward enumeration of scalar values from UTF-8 input.
        public static Utf8SpanUnicodeScalarReverseEnumerator EnumerateReverse(
            ReadOnlySpan<byte> inUtf8) => throw null;
    }

    // Allows streaming validation of input.
    // !! MUTABLE STRUCT !!
    // This type is mainly to support WebSockets and similar scenarios.
    public struct Utf8StreamingValidator
    {
        // Returns true iff all data seen up to now represents well-formed UTF-8,
        // or false iff any data consumed so far has an invalid UTF-8 sequence.
        public bool TryConsume(ReadOnlySpan<byte> data, bool isFinalChunk) => throw null;
    }
}
