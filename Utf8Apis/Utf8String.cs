using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;

// n.b. System namespace
namespace System
{
    // Represents a string whose internal representation consists of UTF-8 subsequences.
    // Like the String class, developers are *strongly discouraged* from creating instances of
    // this type that have invalid UTF-8 subsequences (and our APIs try to encourage good hygiene
    // in this regard), but instances of this type are *not guaranteed* to consist only of well-
    // formed UTF-8 subsequences. The APIs hanging off this type have well-defined, predictable
    // behavior regardless of whether the UTF-8 string contains invalid subsequences.
    //
    // Open question: Do we want this to be indexable or enumerable? The Bytes and Scalars property
    // allow indexing and enumeration. Making the type indexable might give developers a false
    // impression of what the can actually do with the return values.
    //
    // Whenever length / index / offset / count / etc. occurs in these APIs, it's in terms of number
    // of Char8 elements. (Or, "byte length" if you prefer.)
    //
    // n.b. not IComparable because comparison implies lexical (culture-aware) ordering.
    public sealed unsafe class Utf8String : IConvertible, IEquatable<Utf8String>
    {
        // All public ctors are validating ctors.
        // O(n) for memcpy and O(n) for validation.
        // For transcoding (UTF-16 -> UTF-8), validation cannot be skipped.
        // Behavior given invalid input: fixes up bad sequences on-the-fly.
        public Utf8String(byte* value) => throw null;
        public Utf8String(ReadOnlySpan<byte> value) => throw null;
        public Utf8String(char* value) => throw null;
        public Utf8String(string value) => throw null;
        public Utf8String(ReadOnlySpan<char> value) => throw null;

        // Ordinal (byte-by-byte) comparison
        public static bool operator ==(Utf8String a, Utf8String b) => throw null;
        public static bool operator !=(Utf8String a, Utf8String b) => throw null;

        // Matches the implicit cast from String -> ReadOnlySpan<char>
        public static implicit operator ReadOnlySpan<Char8>(Utf8String value) => throw null;

        // n.b. No implicit or explicit cast from Utf8String <-> String.
        // Reason for this is that such an operation would be O(n), and casts should really
        // be constant-time operations. Use ToString() / ToUtf8String() instead.

        // Returns access to the raw bytes (as a span)
        public ReadOnlySpan<byte> Bytes { get => throw null; }

        // Returns access to the raw Char8s (as a span)
        // (I dislike this name. Perhaps it's not even necessary due to the implicit cast?)
        public ReadOnlySpan<Char8> Char8s { get => throw null; }

        // Returns access to the scalars (as an enumerable sequence)
        public ScalarSequence Scalars { get => throw null; }

        // static readonly field, not property or const, to match String.Empty
        public static readonly Utf8String Empty;

        // Length (in Char8s)
        public int Length { get => throw null; }

        public ReadOnlyMemory<Char8> AsMemory() => throw null;

        public static int Compare(Utf8String strA, Utf8String strB, StringComparison comparisonType) => throw null;
        public static int CompareOrdinal(Utf8String strA, Utf8String strB) => throw null;

        public static Utf8String Concat(IEnumerable<Utf8String> values) => throw null;
        public static Utf8String Concat(Utf8String str0, Utf8String str1) => throw null;
        public static Utf8String Concat(Utf8String str0, Utf8String str1, Utf8String str2) => throw null;
        public static Utf8String Concat(Utf8String str0, Utf8String str1, Utf8String str2, Utf8String str3) => throw null;
        public static Utf8String Concat(params Utf8String[] values) => throw null;

        // If we had a language feature which allowed Concat(params ReadOnlySpan<...>) as a parameter, we'd
        // probably have an overload for that as well. We'd probably also want that on the String type.

        // First overload is ordinal; all other overloads require an explicit comparison to be specified.
        // Open question: String.Contains has no overload which takes a ROS<> parameter - do we need one?
        public bool Contains(UnicodeScalar value) => throw null;
        public bool Contains(UnicodeScalar value, StringComparison comparisonType) => throw null;
        public bool Contains(Utf8String value, StringComparison comparisonType) => throw null;
        public bool Contains(ReadOnlySpan<Char8> value, StringComparison comparisonType) => throw null;

        // "CreateFromBytes" is renamed so that type inference doesn't fail if the developer
        // passes an untyped lambda as the third parameter. O(n) for memcpy + O(n) for validation.
        // Behavior given invalid input: fixes up invalid sequences on-the-fly.
        public static Utf8String Create<TState>(int length, TState state, SpanAction<Char8, TState> action) => throw null;
        public static Utf8String CreateFromBytes<TState>(int length, TState state, SpanAction<byte, TState> action) => throw null;

        // Non-validating factory method. O(n) for memcpy.
        // Developers should exercise caution when calling this API, taking into account such considerations as
        // (a) whether the input came from a trustworthy source,
        // (b) which component the constructed instance will be passed to, and
        // (c) the behavior such component might exhibit if faced with invalid sequences.
        //
        // As an example of such a scenario that requires further scrutiny, consider a forum that allows users to
        // sign up for new accounts and post messages. Forum administrators use a web interface to perform such
        // tasks as deleting abusive accounts, moving messages, and so forth. If a malicious user attempts to sign
        // up with a username that contains an invalid UTF-8 sequence, and if such sequence round-trips through the
        // messages database, the username that appears in the page's HTML (as a string) might be different than the
        // username that actually exists in the database (as an arbitrary byte sequence). One potential consequence
        // of this is that if such user starts posting abusive messages, admins will be powerless to do anything via
        // the web interface since the "delete account" API will return "user does not exist", instead requiring the
        // IT administrator to go directly to the database and purge the abuser's account.

        public static Utf8String CreateWithoutValidation<TState>(int length, TState state, SpanAction<Char8, TState> action) => throw null;
        public static Utf8String CreateFromBytesWithoutValidation<TState>(int length, TState state, SpanAction<byte, TState> action) => throw null;
        public static Utf8String CreateFromBytesWithoutValidation<TState>(ReadOnlySpan<byte> value) => throw null;

        public bool EndsWith(UnicodeScalar value) => throw null;
        public bool EndsWith(Utf8String value, StringComparison comparisonType) => throw null;
        public bool EndsWith(ReadOnlySpan<Char8> value, StringComparison comparisonType) => throw null;

        // Ordinal comparison.
        // When transcoding is required, comparison is by ordinal scalar, and invalid subsequences immediately return failure.
        // Example: the UTF-8 string [ C1 80 ] will *never* match any UTF-16 string.
        public override bool Equals(object value) => throw null;
        public bool Equals(Utf8String value) => throw null;
        public bool Equals(string value) => throw null;
        public static bool Equals(Utf8String a, Utf8String b, StringComparison comparisonType) => throw null;
        public static bool Equals(Utf8String a, string b, StringComparison comparisonType) => throw null;

        public override int GetHashCode() => throw null;
        public int GetHashCode(StringComparison comparisonType) => throw null;

        // Used for pinning. Typed as 'byte' instead of 'Char8' because the scenario for calling this
        // is p/invoke, and we don't want to require a reinterpret_cast.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public ref readonly byte GetPinnableReference() => throw null;

        // Open question: if IndexOf has overloads that take (startIndex, count), should Contains have
        // such overloads as well? Additionally, do we want IndexOf overloads that take a ReadOnlySpan<Char8>?
        public int IndexOf(UnicodeScalar value) => throw null;
        public int IndexOf(UnicodeScalar value, int startIndex) => throw null;
        public int IndexOf(UnicodeScalar value, int startIndex, int count) => throw null;
        public int IndexOf(Utf8String value, StringComparison comparisonType) => throw null;
        public int IndexOf(Utf8String value, int startIndex, StringComparison comparisonType) => throw null;
        public int IndexOf(Utf8String value, int startIndex, int count, StringComparison comparisonType) => throw null;

        // Open question: String.IndexOfAny takes array parameters - is it ok for us to take ReadOnlySpan?
        public int IndexOfAny(ReadOnlySpan<UnicodeScalar> value) => throw null;
        public int IndexOfAny(ReadOnlySpan<UnicodeScalar> value, int startIndex) => throw null;
        public int IndexOfAny(ReadOnlySpan<UnicodeScalar> value, int startIndex, int count) => throw null;

        public Utf8String Insert(int startIndex, Utf8String value) => throw null;

        public static bool IsNullOrEmpty(Utf8String value) => throw null;
        public static bool IsNullOrWhiteSpace(Utf8String value) => throw null;

        // How will we want a Join method to work without requiring transcoding to UTF-16 as an intermediate step?

        public static Utf8String Join<T>(UnicodeScalar separator, IEnumerable<T> values) where T : IUtf8Stringable => throw null;
        public static Utf8String Join<T>(UnicodeScalar separator, ReadOnlySpan<T> values) where T : IUtf8Stringable => throw null;
        public static Utf8String Join<T>(Utf8String separator, IEnumerable<T> values) where T : IUtf8Stringable => throw null;
        public static Utf8String Join<T>(Utf8String separator, ReadOnlySpan<T> values) where T : IUtf8Stringable => throw null;

        public int LastIndexOf(UnicodeScalar value) => throw null;
        public int LastIndexOf(UnicodeScalar value, int startIndex) => throw null;
        public int LastIndexOf(UnicodeScalar value, int startIndex, int count) => throw null;
        public int LastIndexOf(Utf8String value, StringComparison comparisonType) => throw null;
        public int LastIndexOf(Utf8String value, int startIndex, StringComparison comparisonType) => throw null;
        public int LastIndexOf(Utf8String value, int startIndex, int count, StringComparison comparisonType) => throw null;

        public int LastIndexOfAny(ReadOnlySpan<UnicodeScalar> value) => throw null;
        public int LastIndexOfAny(ReadOnlySpan<UnicodeScalar> value, int startIndex) => throw null;
        public int LastIndexOfAny(ReadOnlySpan<UnicodeScalar> value, int startIndex, int count) => throw null;

        // Normalization APIs may have to take low priority since we need to make significant changes
        // to the CultureInfo class in order to support all of this.
        public bool IsNormalized() => throw null;
        public bool IsNormalized(NormalizationForm normalizationForm) => throw null;
        public Utf8String Normalize() => throw null;
        public Utf8String Normalize(NormalizationForm normalizationForm) => throw null;

        // Open question: What should the behavior be if we'd end up splitting the UTF-8 representation
        // of the scalar in order to achieve the correct padding? I'm leaning toward forbidding non-
        // ASCII characters for the padding char, even if there's enough room.
        public Utf8String PadLeft(int totalWidth) => throw null;
        public Utf8String PadLeft(int totalWidth, UnicodeScalar paddingChar) => throw null;

        public Utf8String PadRight(int totalWidth) => throw null;
        public Utf8String PadRight(int totalWidth, UnicodeScalar paddingChar) => throw null;

        public Utf8String Remove(int startIndex) => throw null;
        public Utf8String Remove(int startIndex, int count) => throw null;

        public Utf8String Replace(UnicodeScalar oldValue, UnicodeScalar newValue) => throw null;
        public Utf8String Replace(Utf8String oldValue, Utf8String newValue, StringComparison comparisonType) => throw null;

        public Utf8String[] Split(UnicodeScalar separator) => throw null;
        public Utf8String[] Split(UnicodeScalar separator, int count) => throw null;
        public Utf8String[] Split(UnicodeScalar separator, int count, StringSplitOptions options) => throw null;
        public Utf8String[] Split(ReadOnlySpan<UnicodeScalar> separator) => throw null;
        public Utf8String[] Split(ReadOnlySpan<UnicodeScalar> separator, int count) => throw null;
        public Utf8String[] Split(ReadOnlySpan<UnicodeScalar> separator, int count, StringSplitOptions options) => throw null;
        public Utf8String[] Split(Utf8String separator) => throw null;
        public Utf8String[] Split(Utf8String separator, int count) => throw null;
        public Utf8String[] Split(Utf8String separator, int count, StringSplitOptions options) => throw null;

        public bool StartsWith(UnicodeScalar value) => throw null;
        public bool StartsWith(Utf8String value, StringComparison comparisonType) => throw null;

        // The natural way to use Substring is first to call IndexOf(...), then to substring on the index
        // that is returned. Since the parameter passed to IndexOf is generally a literal or some other value
        // under the developer's control, this means that the natural way of calling Substring shouldn't
        // inadvertently lead to splitting the string in the middle of a UTF-8 sequence. (This same argument
        // holds for the String class.)
        //
        // If the developer wants to go out of their way to substring a valid string in such a way that the
        // result is invalid UTF-8, we won't stop them. But we *can* detect this in O(1) time if necessary.

        public Utf8String Substring(int startIndex) => throw null;
        public Utf8String Substring(int startIndex, int length) => throw null;

        // No ToLower() method - method name contains 'invariant' or culture must be specified
        public Utf8String ToLowerInvariant() => throw null;
        public Utf8String ToLower(CultureInfo culture) => throw null;

        public override string ToString() => throw null;
        public string ToString(IFormatProvider provider) => throw null;

        public Utf8String ToUpperInvariant() => throw null;
        public Utf8String ToUpper(CultureInfo culture) => throw null;

        public Utf8String Trim() => throw null;
        public Utf8String Trim(UnicodeScalar trimScalar) => throw null;
        public Utf8String Trim(ReadOnlySpan<UnicodeScalar> trimScalars) => throw null;

        public Utf8String TrimEnd() => throw null;
        public Utf8String TrimEnd(UnicodeScalar trimScalar) => throw null;
        public Utf8String TrimEnd(ReadOnlySpan<UnicodeScalar> trimScalars) => throw null;

        public Utf8String TrimStart() => throw null;
        public Utf8String TrimStart(UnicodeScalar trimScalar) => throw null;
        public Utf8String TrimStart(ReadOnlySpan<UnicodeScalar> trimScalars) => throw null;

        TypeCode IConvertible.GetTypeCode() => throw null;
        bool IConvertible.ToBoolean(IFormatProvider provider) => throw null;
        byte IConvertible.ToByte(IFormatProvider provider) => throw null;
        char IConvertible.ToChar(IFormatProvider provider) => throw null;
        DateTime IConvertible.ToDateTime(IFormatProvider provider) => throw null;
        decimal IConvertible.ToDecimal(IFormatProvider provider) => throw null;
        double IConvertible.ToDouble(IFormatProvider provider) => throw null;
        short IConvertible.ToInt16(IFormatProvider provider) => throw null;
        int IConvertible.ToInt32(IFormatProvider provider) => throw null;
        long IConvertible.ToInt64(IFormatProvider provider) => throw null;
        sbyte IConvertible.ToSByte(IFormatProvider provider) => throw null;
        float IConvertible.ToSingle(IFormatProvider provider) => throw null;
        object IConvertible.ToType(Type conversionType, IFormatProvider provider) => throw null;
        ushort IConvertible.ToUInt16(IFormatProvider provider) => throw null;
        uint IConvertible.ToUInt32(IFormatProvider provider) => throw null;
        ulong IConvertible.ToUInt64(IFormatProvider provider) => throw null;
        public readonly struct ScalarSequence : IEnumerable<UnicodeScalar>
        {
            public ScalarEnumerator GetEnumerator() => throw null;

            IEnumerator<UnicodeScalar> IEnumerable<UnicodeScalar>.GetEnumerator() => throw null;
            IEnumerator IEnumerable.GetEnumerator() => throw null;
        }

        // If the enumerator sees an invalid UTF-8 subsequence, it returns U+FFFD
        // and moves on to the next subsequence. This means that the developer cannot
        // differentiate the case where the string contained U+FFFD from the case where
        // the string contained an invalid subsequence. There are other APIs to support
        // this scenario.
        public struct ScalarEnumerator : IEnumerator<UnicodeScalar>
        {
            public UnicodeScalar Current => throw null;
            public void Dispose() => throw null;
            public bool MoveNext() => throw null;
            public void Reset() => throw null;

            object IEnumerator.Current => throw null;
        }
    }
}
