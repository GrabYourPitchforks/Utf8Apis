using System;
using System.Buffers;
using System.Collections.Generic;
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


        // Converts UTF8 -> UTF8, fixing up invalid sequences along the way.
        // (This will never return InvalidData.)
        public static OperationStatus ConvertToValidUtf8(
            ReadOnlySpan<Char8> input,
            Span<Char8> output,
            out int elementsConsumed,
            out int elementsWritten,
            bool isFinalChunk = true) => throw null;

        // Same as above, but input buffer has T=byte
        public static OperationStatus ConvertToValidUtf8(
            ReadOnlySpan<byte> input,
            Span<Char8> output,
            out int elementsConsumed,
            out int elementsWritten,
            bool isFinalChunk = true) => throw null;

        // If the input is valid UTF-8, returns the input as-is.
        // If the input is not valid UTF-8, replaces invalid sequences and returns
        // a Utf8String which is well-formed UTF-8.
        public static Utf8String ConvertToValidUtf8(Utf8String input) => throw null;

        // ROM<T>-based overload of above. Will return original input or allocate new buffer.
        public static ReadOnlyMemory<Char8> ConvertToValidUtf8(ReadOnlyMemory<Char8> input) => throw null;
    }

    // Contains helper methods for inspecting UTF-8 data.
    // Alternative naming: class Utf8Inspection in namespace System.Text.Utf8
    public static partial class Utf8
    {
        // Allows forward enumeration over scalars.
        // TODO: Should we also allow reverse enumeration? GetScalarsReverse?
        public static Utf8SpanUnicodeScalarEnumerator GetScalars(ReadOnlySpan<byte> utf8Text) => throw null;
        public static Utf8SpanUnicodeScalarEnumerator GetScalars(ReadOnlySpan<Char8> utf8Text) => throw null;

        // Returns true iff the input consists only of well-formed UTF-8 sequences.
        // This is O(n).
        public static bool IsWellFormedUtf8Sequence(ReadOnlySpan<byte> utf8Text) => throw null;
        public static bool IsWellFormedUtf8Sequence(ReadOnlySpan<Char8> utf8Text) => throw null;

        // Returns true iff the Utf8String consists only of well-formed UTF-8 sequences.
        // This will almost always be O(1) but may be O(n) in weird edge cases.
        public static bool IsWellFormedUtf8String(Utf8String utf8String) => throw null;
        
        // Given UTF-8 input text, returns the number of UTF-16 characters and the
        // number of scalars present. Returns false iff the input sequence is invalid
        // and the invalid sequence behavior is to fail.
        public static bool TryGetUtf16CharCount(
            ReadOnlySpan<Char8> utf8Text,
            out int utf16CharCount,
            out int scalarCount,
            InvalidSequenceBehavior replacementBehavior = InvalidSequenceBehavior.Fail) => throw null;

        // Given UTF-16 input text, returns the number of UTF-8 characters and the
        // number of scalars present. Returns false iff the input sequence is invalid
        // and the invalid sequence behavior is to fail *or* if the required number of
        // UTF-8 chars does not fit into an Int32.
        // TODO: Does that mean the 'out' type should be long?
        public static bool TryGetUtf8CharCount(
            ReadOnlySpan<char> utf16Text,
            out int utf8CharCount,
            out int scalarCount,
            InvalidSequenceBehavior replacementBehavior = InvalidSequenceBehavior.Fail) => throw null;

        // Returns the index of the first invalid UTF-8 sequence, or -1 if the input text
        // is well-formed UTF-8. Additionally returns the number of UTF-16 chars and the
        // number of scalars seen up until that point. (If returns -1, the two 'out' values
        // represent the values for the entire string.)
        public static int GetIndexOfFirstInvalidUtf8Sequence(
            ReadOnlySpan<Char8> utf8Text,
            out int utf16CharCount,
            out int scalarCount) => throw null;
    }

    // Allows streaming validation of input.
    // !! MUTABLE STRUCT !!
    public struct Utf8StreamingValidator
    {
        // Returns true iff all data seen up to now represents well-formed UTF-8,
        // or false iff any data consumed so far has an invalid UTF-8 sequence.
        public bool TryConsume(ReadOnlySpan<byte> data, bool isFinalChunk) => throw null;
    }
}
