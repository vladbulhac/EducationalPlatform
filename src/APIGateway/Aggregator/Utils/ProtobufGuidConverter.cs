using System;
using System.Buffers.Binary;

namespace EducationaInstitutionAPI.Utils
{
    /// <summary>
    /// Contains methods that are used to convert <see cref="Guid"/> to the protocol buffer language and vice versa
    /// </summary>
    /// <remarks>Both methods contain the code found at the following link: <see cref="https://github.com/protocolbuffers/protobuf/issues/2224"/></remarks>
    public static class ProtobufGuidConverter
    {
        /// <summary>
        /// An extension method that converts a <see cref="Guid"/> to the protocol buffer language equivalent <see cref="Uuid"/>
        /// </summary>
        public static void EncodeGuid(this Guid identifier, out UInt64 High64, out UInt64 Low64)
        {
            Span<byte> bytes = stackalloc byte[16];
            identifier.TryWriteBytes(bytes);

            // MSB -> LSB: time_low (32 bits) | time_mid (16 bits) | time_hi_and_version (16 bits).
            High64 = ((ulong)BinaryPrimitives.ReadUInt32LittleEndian(bytes.Slice(0, 4)) << 32) // time_low
                | ((ulong)BinaryPrimitives.ReadUInt16LittleEndian(bytes.Slice(4, 2)) << 16) // time_mid
                | BinaryPrimitives.ReadUInt16LittleEndian(bytes.Slice(6, 2)); // time_hi_and_version

            // MSB -> LSB: clock_seq_hi_and_reserved (8 bits) | clock_seq_low (8 bits) | node (48 bits).
            Low64 = BinaryPrimitives.ReadUInt64BigEndian(bytes.Slice(8, 8));
        }

        /// <summary>
        /// Converts the protocol buffer language <see cref="Uuid"/> to a <see cref="Guid"/>
        /// </summary>
        public static Guid DecodeGuid(UInt64 High64, UInt64 Low64)
        {
            Span<byte> bytes = stackalloc byte[16];
            BinaryPrimitives.WriteUInt32LittleEndian(bytes.Slice(0, 4), (uint)(High64 >> 32));
            BinaryPrimitives.WriteUInt16LittleEndian(bytes.Slice(4, 2), (ushort)((High64 >> 16) & 0xFFFF));
            BinaryPrimitives.WriteUInt16LittleEndian(bytes.Slice(6, 2), (ushort)(High64 & 0xFFFF));
            BinaryPrimitives.WriteUInt64BigEndian(bytes.Slice(8, 8), Low64);
            return new Guid(bytes);
        }
    }
}