using System;
using System.Buffers;
using System.Collections.Generic;
using System.Text;

namespace System.Text
{
    /// <summary>
    /// Represents the validity of a sequence returned by <see cref="UnicodeReader"/>'s peek methods.
    /// </summary>
    public enum SequenceValidity
    {
        /// <summary>
        /// The input buffer contained an incomplete sequence.
        /// </summary>
        /// <remarks>
        /// <see cref="IncompleteSequence"/> is distinct from <see cref="InvalidSequence"/> in two ways.
        /// First, <see cref="IncompleteSequence"/> can only occur at the end of a string. Additionally,
        /// <see cref="IncompleteSequence"/> means that the end of the string may contain the start of
        /// a valid sequence, but it cannot be determined for sure until more data is read.
        /// <see cref="IncompleteSequence"/> is also returned for empty buffers.
        /// </remarks>
        IncompleteSequence = 0,

        /// <summary>
        /// The input buffer contained an invalid sequence.
        /// </summary>
        InvalidSequence = 1,

        /// <summary>
        /// The input buffer contained a valid sequence.
        /// </summary>
        ValidSequence = 2,
    }
}
