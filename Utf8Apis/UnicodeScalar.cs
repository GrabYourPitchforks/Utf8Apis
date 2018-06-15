using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;

namespace System.Text
{
    public readonly struct UnicodeScalar : IComparable<UnicodeScalar>, IEquatable<UnicodeScalar>
    {
        public UnicodeScalar(char ch) => throw null;
        public UnicodeScalar(int scalarValue) => throw null;
        public UnicodeScalar(uint scalarValue) => throw null;
        public static bool operator ==(UnicodeScalar a, UnicodeScalar b) => throw null;
        public static bool operator !=(UnicodeScalar a, UnicodeScalar b) => throw null;
        public static bool operator <(UnicodeScalar a, UnicodeScalar b) => throw null;
        public static bool operator <=(UnicodeScalar a, UnicodeScalar b) => throw null;
        public static bool operator >(UnicodeScalar a, UnicodeScalar b) => throw null;
        public static bool operator >=(UnicodeScalar a, UnicodeScalar b) => throw null;
        public bool IsAscii { get => throw null; } // = (Value < 0x80)
        public bool IsBmp { get => throw null; } // = (Value < 0x10000)
        public int Plane { get => throw null; } // = (Value >> 16)
        public static UnicodeScalar ReplacementChar { get => throw null; } // = U+FFFD
        public int Utf16SequenceLength { get => throw null; } // = 1..2
        public int Utf8SequenceLength { get => throw null; } // = 1..4
        public uint Value { get => throw null; }
        public int CompareTo(UnicodeScalar other) => throw null;
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static UnicodeScalar DangerousCreateWithoutValdation(uint scalarValue) => throw null;
        public override bool Equals(object obj) => throw null;
        public bool Equals(UnicodeScalar other) => throw null;
        public override int GetHashCode() => throw null;
        public static bool IsValid(int value) => throw null;
        public override string ToString() => throw null;
        public int ToUtf16(Span<char> output) => throw null;
        public int ToUtf8(Span<Char8> output) => throw null;
        public Utf8String ToUtf8String() => throw null;
        public static bool TryCreate(int value, out UnicodeScalar result) => throw null;
        public static bool TryCreate(uint value, out UnicodeScalar result) => throw null;

        /* OPTIONAL - these are analogs of APIs on System.Char */

        public static double GetNumericValue(UnicodeScalar s) => throw null;
        public static UnicodeCategory GetUnicodeCategory(UnicodeScalar s) => throw null;
        public static bool IsControl(UnicodeScalar s) => throw null;
        public static bool IsDigit(UnicodeScalar s) => throw null;
        public static bool IsLetter(UnicodeScalar s) => throw null;
        public static bool IsLetterOrDigit(UnicodeScalar s) => throw null;
        public static bool IsLower(UnicodeScalar s) => throw null;
        public static bool IsNumber(UnicodeScalar s) => throw null;
        public static bool IsPunctuation(UnicodeScalar s) => throw null;
        public static bool IsSeparator(UnicodeScalar s) => throw null;
        public static bool IsSymbol(UnicodeScalar s) => throw null;
        public static bool IsUpper(UnicodeScalar s) => throw null;
        public static bool IsWhiteSpace(UnicodeScalar s) => throw null;
        public static char ToLower(UnicodeScalar s, CultureInfo culture) => throw null;
        public static char ToLowerInvariant(UnicodeScalar s) => throw null;
        public static char ToUpper(UnicodeScalar s, CultureInfo culture) => throw null;
        public static char ToUpperInvariant(UnicodeScalar s) => throw null;
    }
}
