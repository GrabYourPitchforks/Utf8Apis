using System;
using System.Collections.Generic;
using System.Text;

namespace System
{
    // Interface that allows a type to mark itself as "I can turn into a Utf8String instance."
    // Open question: Do we want a format provider right now? That's more of a UI concern.
    public interface IUtf8Stringable
    {
        Utf8String ToUtf8String();
    }
}
