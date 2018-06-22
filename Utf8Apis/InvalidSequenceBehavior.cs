using System;
using System.Collections.Generic;
using System.Text;

namespace System.Text
{
    /// <summary>
    /// Describes the remediation that the text processor should take when it
    /// encounters an invalid sequence in the input stream.
    /// </summary>
    public enum InvalidSequenceBehavior
    {
        /// <summary>
        /// The text processor should report failure and should not attempt to proceed
        /// past the invalid sequence.
        /// </summary>
        Fail,

        /// <summary>
        /// The text processor should treat the invalid sequence as if it had instead seen
        /// the scalar value U+FFFD, and the text processor should attempt to continue.
        /// </summary>
        SubstituteReplacementChar,
    }
}
