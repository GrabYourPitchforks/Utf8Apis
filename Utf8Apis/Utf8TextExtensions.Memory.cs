using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;

namespace System.Text
{
    // Memory-based overloads
    public static partial class Utf8TextExtensions
    {
        // Named like the String.CompareTo instance method, but has an API surface closer to the String.Compare static method
        public static int CompareTo(this ReadOnlyMemory<Char8> utf8Text, ReadOnlyMemory<Char8> other, CultureInfo culture, CompareOptions options) => throw null;
        public static int CompareTo(this ReadOnlyMemory<Char8> utf8Text, ReadOnlyMemory<Char8> other, StringComparison comparisonType) => throw null;

        public static void Contains(this ReadOnlyMemory<Char8> utf8Text, ReadOnlyMemory<Char8> value) => throw null;
        public static void Contains(this ReadOnlyMemory<Char8> utf8Text, ReadOnlyMemory<Char8> value, StringComparison comparisonType) => throw null;
        public static void Contains(this ReadOnlyMemory<Char8> utf8Text, UnicodeScalar value) => throw null;
        public static void Contains(this ReadOnlyMemory<Char8> utf8Text, UnicodeScalar value, StringComparison comparisonType) => throw null;

        // Also EndsWith
        // n.b. The method below masks MemoryExtensions.StartsWith<T>(this ROS<T>, ROS<T>) because we want to
        // use the proper culture. This same logic *does not* exist for string segments, which means String.StartsWith
        // and String.Span.StartsWith use different culture comparisons by default. We should address this.
        public static bool StartsWith(this ReadOnlyMemory<Char8> utf8Text, ReadOnlyMemory<Char8> value) => throw null;
        public static bool StartsWith(this ReadOnlyMemory<Char8> utf8Text, ReadOnlyMemory<Char8> value, StringComparison comparisonType) => throw null;

        // !! WARNING !!
        // ReadOnlyMemory<T> already implements Equals as "shallow equals", which means that we end up with the following behavior:
        //
        // ReadOnlyMemory<Char8> a = utf8"Hello world!".AsMemory();
        // ReadOnlyMemory<Char8> b = utf8"Hello world!".ToArray().AsMemory();
        //
        // Console.WriteLine(a.Equals(b)); // writes "false"
        // Console.WriteLine(a.Equals(b, StringComparison.Ordinal)); // writes "true"

        public static bool Equals(this ReadOnlyMemory<Char8> utf8Text, ReadOnlyMemory<Char8> value, StringComparison stringComparison) => throw null;

        // Also LastIndexOf{Any}
        // Same deal as StartsWith; the first overload masks the normal extension method on MemoryExtensions.
        public static int IndexOf(this ReadOnlyMemory<Char8> utf8Text, ReadOnlyMemory<Char8> value) => throw null;
        public static int IndexOf(this ReadOnlyMemory<Char8> utf8Text, ReadOnlyMemory<Char8> value, StringComparison stringComparison) => throw null;
        public static int IndexOf(this ReadOnlyMemory<Char8> utf8Text, UnicodeScalar value) => throw null;
        public static int IndexOf(this ReadOnlyMemory<Char8> utf8Text, UnicodeScalar value, StringComparison stringComparison) => throw null;
        public static int IndexOfAny(this ReadOnlyMemory<Char8> utf8Text, ReadOnlyMemory<UnicodeScalar> values, StringComparison stringComparison) => throw null;

        public static bool IsEmptyOrWhiteSpace(this ReadOnlyMemory<Char8> utf8Text) => throw null;

        // Also Trim and TrimEnd
        public static ReadOnlyMemory<Char8> Trim(this ReadOnlyMemory<Char8> utf8Text) => throw null;
        public static ReadOnlyMemory<Char8> Trim(this ReadOnlyMemory<Char8> utf8Text, UnicodeScalar trimScalar) => throw null;
        public static ReadOnlyMemory<Char8> Trim(this ReadOnlyMemory<Char8> utf8Text, ReadOnlySpan<UnicodeScalar> trimScalars) => throw null;

        public static Utf8String ToUtf8String(this ReadOnlyMemory<Char8> utf8Text) => throw null;

        // This would actually be an instance method on ROS<T>, but including here for API completeness.
        public static string ToString(this ReadOnlyMemory<Char8> utf8Text) => throw null;
        
        /*
         * PROJECTION APIS
         */

        // O(1) - this is just an unsafe cast
        public static ReadOnlyMemory<byte> AsBytes(this ReadOnlyMemory<Char8> utf8Text) => throw null;

        // O(1) - this is just an unsafe cast (non-validating)
        public static ReadOnlyMemory<Char8> AsUtf8(this ReadOnlyMemory<byte> utf8Text) => throw null;
    }
}
