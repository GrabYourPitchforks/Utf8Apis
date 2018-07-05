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
    // The class isn't directly indexable or enumerable, instead relying on the developer to
    // go through one of the AsBytes / AsScalars / AsSpan APIs.
    //
    // Whenever length / index / offset / count / etc. occurs in these APIs, it's in terms of number
    // of Char8 elements. (Or, "byte length" if you prefer.)
    public sealed unsafe class Utf8String : IComparable<Utf8String>, IConvertible, IEquatable<Utf8String>
    {
        /*
         * CONSTRUCTORS
         * All public ctors are validating ctors.
         * Complexity is O(n) for memcpy and O(n) for validation.
         * Behavior given invalid input: bad sequences replaced with U+FFFD.
         * Scroll down further in the file for static factories that suppress validation.
         */

        // For null-terminated UTF-8 and UTF-16 sequences.
        // If not null-terminated, wrap (ptr, length) in a Span and call the Span-based ctors.
        public Utf8String(byte* value) => throw null;
        public Utf8String(Char8* value) => throw null;
        public Utf8String(char* value) => throw null;

        // For non null-terminated UTF-8 and UTF-16 sequences.
        public Utf8String(ReadOnlySpan<byte> value) => throw null;
        public Utf8String(ReadOnlySpan<Char8> value) => throw null;
        public Utf8String(ReadOnlySpan<char> value) => throw null;

        // For discoverability / ease of use, equivalent to ROS<char>-based ctor
        public Utf8String(String value) => throw null;

        // For creating from a sequence of discontiguous buffers containing UTF-8 data
        public Utf8String(ReadOnlySequence<byte> value) => throw null;

        /*
         * COMPARISON
         * All equality / comparison methods which don't explicitly take a StringComparison
         * are ordinal by default. This differs slightly from System.String but is self-consistent
         * within the Utf8String class.
         */

        public static bool operator ==(Utf8String a, Utf8String b) => throw null;
        public static bool operator !=(Utf8String a, Utf8String b) => throw null;

        public int CompareTo(Utf8String other) => throw null; // for IComparable<Utf8String>

        public int Compare(Utf8String a, Utf8String b, StringComparison comparisonType) => throw null;
        public int CompareOrdinal(Utf8String a, Utf8String b) => throw null;

        /*
         * PROJECTION
         * n.b. No implicit or explicit cast from Utf8String <-> String.
         * Reason for this is that the cast would have O(n) complexity, which would be
         * potentially surprising for developers. Use ToString() / ToUtf8String() instead.
         */

        public ReadOnlySpan<byte> AsBytes() => throw null;
        public ReadOnlyMemory<Char8> AsMemory() => throw null;
        public ScalarSequence AsScalars() => throw null; // custom struct enumerable
        public ReadOnlySpan<Char8> AsSpan() => throw null;

        // static readonly field, not property or const, to match String.Empty
        public static readonly Utf8String Empty;

        // Length (in Char8s)
        public int Length { get => throw null; }

        /*
         * CONCAT
         * This set of overloads may change based on how language and compiler support for '+' works
         * with Utf8String instances, including whether struct-based builder types come online.
         * Let's go with this for now pending how those other features shake out.
         */

        public static Utf8String Concat(IEnumerable<Utf8String> values) => throw null;
        public static Utf8String Concat(Utf8String str0, Utf8String str1) => throw null;
        public static Utf8String Concat(Utf8String str0, Utf8String str1, Utf8String str2) => throw null;
        public static Utf8String Concat(Utf8String str0, Utf8String str1, Utf8String str2, Utf8String str3) => throw null;
        public static Utf8String Concat(params Utf8String[] values) => throw null;

        // If we had a language feature which allowed Concat(params ReadOnlySpan<...>) as a parameter, we'd
        // probably have an overload for that as well. We'd probably also want that on the String type.

        public static Utf8String Concat(ReadOnlySpan<Char8> str0, ReadOnlySpan<Char8> str1) => throw null;
        public static Utf8String Concat(ReadOnlySpan<Char8> str0, ReadOnlySpan<Char8> str1, ReadOnlySpan<Char8> str2) => throw null;
        public static Utf8String Concat(ReadOnlySpan<Char8> str0, ReadOnlySpan<Char8> str1, ReadOnlySpan<Char8> str2, ReadOnlySpan<Char8> str3) => throw null;

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
        public static Utf8String CreateWithoutValidation<TState>(ReadOnlySpan<byte> Char8) => throw null;
        public static Utf8String CreateFromBytesWithoutValidation<TState>(int length, TState state, SpanAction<byte, TState> action) => throw null;
        public static Utf8String CreateFromBytesWithoutValidation<TState>(ReadOnlySpan<byte> value) => throw null;

        public bool EndsWith(UnicodeScalar value) => throw null;
        public bool EndsWith(Utf8String value, StringComparison comparisonType) => throw null;
        public bool EndsWith(ReadOnlySpan<Char8> value, StringComparison comparisonType) => throw null;

        // When transcoding is required, comparison is by ordinal scalar, and invalid subsequences immediately return failure.
        // Example: the UTF-8 string [ C1 80 ] will *never* match any UTF-16 string.

        public override bool Equals(object value) => throw null;
        public bool Equals(Utf8String value) => throw null;
        public bool Equals(string value) => throw null;
        public static bool Equals(Utf8String a, Utf8String b) => throw null;
        public static bool Equals(Utf8String a, Utf8String b, StringComparison comparisonType) => throw null;

        public override int GetHashCode() => throw null;
        public int GetHashCode(StringComparison comparisonType) => throw null;

        // Used for pinning. Typed as 'byte' instead of 'Char8' because the scenario for calling this
        // is p/invoke, and we don't want to require a reinterpret_cast.

        [EditorBrowsable(EditorBrowsableState.Never)]
        public ref readonly byte GetPinnableReference() => throw null;

        public int IndexOf(UnicodeScalar value) => throw null;
        public int IndexOf(UnicodeScalar value, int startIndex) => throw null;
        public int IndexOf(UnicodeScalar value, int startIndex, int count) => throw null;
        public int IndexOf(Utf8String value, StringComparison comparisonType) => throw null;
        public int IndexOf(Utf8String value, int startIndex, StringComparison comparisonType) => throw null;
        public int IndexOf(Utf8String value, int startIndex, int count, StringComparison comparisonType) => throw null;

        // n.b. String.IndexOfAny takes char[], we take span (to avoid allocations)
        public int IndexOfAny(ReadOnlySpan<UnicodeScalar> value) => throw null;
        public int IndexOfAny(ReadOnlySpan<UnicodeScalar> value, int startIndex) => throw null;
        public int IndexOfAny(ReadOnlySpan<UnicodeScalar> value, int startIndex, int count) => throw null;

        public Utf8String Insert(int startIndex, Utf8String value) => throw null;

        public static bool IsNullOrEmpty(Utf8String value) => throw null;
        public static bool IsNullOrWhiteSpace(Utf8String value) => throw null;

        public static Utf8String Join<T>(UnicodeScalar separator, IEnumerable<T> values) => throw null;
        public static Utf8String Join<T>(UnicodeScalar separator, ReadOnlySpan<T> values) => throw null;
        public static Utf8String Join<T>(Utf8String separator, IEnumerable<T> values) => throw null;
        public static Utf8String Join<T>(Utf8String separator, ReadOnlySpan<T> values) => throw null;

        public int LastIndexOf(UnicodeScalar value) => throw null;
        public int LastIndexOf(UnicodeScalar value, int startIndex) => throw null;
        public int LastIndexOf(UnicodeScalar value, int startIndex, int count) => throw null;
        public int LastIndexOf(Utf8String value, StringComparison comparisonType) => throw null;
        public int LastIndexOf(Utf8String value, int startIndex, StringComparison comparisonType) => throw null;
        public int LastIndexOf(Utf8String value, int startIndex, int count, StringComparison comparisonType) => throw null;

        public int LastIndexOfAny(ReadOnlySpan<UnicodeScalar> value) => throw null;
        public int LastIndexOfAny(ReadOnlySpan<UnicodeScalar> value, int startIndex) => throw null;
        public int LastIndexOfAny(ReadOnlySpan<UnicodeScalar> value, int startIndex, int count) => throw null;

        // There's a risk Normalize / IsNormalized will fall below the cutline due to time constraints, but here's what
        // it would look like if we implemented them.

        public bool IsNormalized() => throw null;
        public bool IsNormalized(NormalizationForm normalizationForm) => throw null;
        public Utf8String Normalize() => throw null;
        public Utf8String Normalize(NormalizationForm normalizationForm) => throw null;

        // n.b. Padding takes Char8 instead of UnicodeScalar. This prevents a situation where the amount of padding
        // required isn't wholly divisible by the number of code units in the padding character.

        public Utf8String PadLeft(int totalWidth) => throw null;
        public Utf8String PadLeft(int totalWidth, Char8 paddingChar) => throw null;

        public Utf8String PadRight(int totalWidth) => throw null;
        public Utf8String PadRight(int totalWidth, Char8 paddingChar) => throw null;

        public Utf8String Remove(int startIndex) => throw null;
        public Utf8String Remove(int startIndex, int count) => throw null;

        public Utf8String Replace(UnicodeScalar oldValue, UnicodeScalar newValue) => throw null;
        public Utf8String Replace(Utf8String oldValue, Utf8String newValue, StringComparison comparisonType) => throw null;

        // n.b. Utf8String.Split returns its results in an array, just like String.Split. There will be non-allocating
        // Split APIs hanging off of ROM<Char8> / ROS<Char8> and other types for more advanced use cases.

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
        // result is invalid UTF-8, we won't stop them.

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
        // and moves on to the next subsequence. There are separate APIs to distinguish
        // between "U+FFFD due to invalid data" and "the string really contained U+FFFD."
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
