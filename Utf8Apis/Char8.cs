using System;
using System.Collections.Generic;
using System.Text;

// n.b. System namespace
namespace System
{
    // Represents a UTF-8 code unit.
    // Unlike Byte / Char, is not IFormattable or IConvertible.
    // Does not expose "inspect this character" APIs; those are on the UnicodeScalar type.
    // TODO: Does this need to be serializable? Perhaps not.
    public readonly struct Char8 : IComparable<Char8>, IEquatable<Char8>
    {
        // Equality and comparison operators
        public static bool operator <(Char8 a, Char8 b) => throw null;
        public static bool operator <=(Char8 a, Char8 b) => throw null;
        public static bool operator >(Char8 a, Char8 b) => throw null;
        public static bool operator >=(Char8 a, Char8 b) => throw null;
        public static bool operator ==(Char8 a, Char8 b) => throw null;
        public static bool operator !=(Char8 a, Char8 b) => throw null;

        // TODO: How do conversion operators work?
        // We need to make sure these support 'checked' / 'unchecked' language syntax.
        // Also, is it possible to write "if (myChar8Value == 0)" without an explicit cast?
        // Need to follow up with the language team to figure out just what explicit / implicit
        // operators need to be defined.

        // IComparable
        public int CompareTo(Char8 other) => throw null;

        public override bool Equals(object obj) => throw null;

        public bool Equals(Char8 other) => throw null;

        public override int GetHashCode() => throw null;

        public override string ToString() => throw null;
    }
}
