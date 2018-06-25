using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO;
using System.Text;

static class Samples
{
    static void JsonSerialize_Helper(ReadOnlySpan<char> input, IBufferWriter<byte> writer, Span<byte> scratch1, Span<byte> scratch2)
    {
        // Transcode UTF-16 -> UTF-8 in a loop.
        var utf16InputToEncode = input;
        var utf8TranscodingOperationStatus = default(OperationStatus);
        do
        {
            utf8TranscodingOperationStatus = Utf8.ConvertFromUtf16(utf16InputToEncode, scratch1, out var utf16CharsConsumed, out var utf8BytesTranscoded);
            if (utf8TranscodingOperationStatus == OperationStatus.InvalidData)
            {
                throw new Exception("Bad input.");
            }
            utf16InputToEncode = utf16InputToEncode.Slice(utf16CharsConsumed);

            // Now that we've properly transcoded UTF-16 -> UTF-8, JSON-encode it and write it to the output.
            var utf8JsonInputToEncode = scratch1.Slice(0, utf8BytesTranscoded);
            var jsonEncodingOperationStatus = default(OperationStatus);
            do
            {
                jsonEncodingOperationStatus = JsonEncoder.Default.Encode(utf8JsonInputToEncode, scratch2, out var utf8BytesConsumed, out var jsonEncodedBytesWritten);
                if (jsonEncodingOperationStatus == OperationStatus.InvalidData)
                {
                    throw new Exception("Bad input.");
                }
                utf8JsonInputToEncode = utf8JsonInputToEncode.Slice(utf8BytesConsumed);

                writer.Write(scratch2.Slice(0, jsonEncodedBytesWritten));
            } while (jsonEncodingOperationStatus != OperationStatus.Done);
        } while (utf8TranscodingOperationStatus != OperationStatus.Done);
    }

    // Writes an IDictionary<string, string> as a UTF-8 JSON object
    static void JsonSerialize(IDictionary<string, string> dictionary, IBufferWriter<byte> writer)
    {
        byte[] scratch1 = ArrayPool<byte>.Shared.Rent(1024);
        byte[] scratch2 = ArrayPool<byte>.Shared.Rent(1024);
        try
        {
            writer.Write(utf8"{");

            foreach (var entry in dictionary)
            {
                writer.Write(utf8"\"");
                JsonSerialize_Helper(entry.Key, writer, scratch1, scratch2);
                writer.Write(utf8"\":\"");
                JsonSerialize_Helper(entry.Key, writer, scratch1, scratch2);
                writer.Write(utf8"\"");
            }

            writer.Write(utf8"}");
        }
        finally
        {
            ArrayPool<byte>.Shared.Return(scratch1);
            ArrayPool<byte>.Shared.Return(scratch2);
        }
    }
}
