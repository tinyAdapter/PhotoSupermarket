using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoSupermarket.Core.Util
{
    public class Bytes
    {
        public static byte ReadByte(byte[] bytes, ref int index)
        {
            byte result = bytes[index];
            index += 1;
            return result;
        }

        public static ushort ReadUShort(byte[] bytes, ref int index)
        {
            ushort result;
            if (BitConverter.IsLittleEndian)
            {
                result = BitConverter.ToUInt16(bytes, index);
            }
            else
            {
                byte[] valueBytes = new byte[] { bytes[index + 1], bytes[index] };
                result = BitConverter.ToUInt16(bytes, index);
            }
            index += 2;
            return result;
        }

        public static uint ReadUInt(byte[] bytes, ref int index)
        {
            uint result;
            if (BitConverter.IsLittleEndian)
            {
                result = BitConverter.ToUInt32(bytes, index);
            }
            else
            {
                byte[] valueBytes = new byte[] { bytes[index + 1], bytes[index] };
                result = BitConverter.ToUInt32(bytes, index);
            }
            index += 4;
            return result;
        }

        public static int ReadInt(byte[] bytes, ref int index)
        {
            int result;
            if (BitConverter.IsLittleEndian)
            {
                result = BitConverter.ToInt32(bytes, index);
            }
            else
            {
                byte[] valueBytes = new byte[] { bytes[index + 1], bytes[index] };
                result = BitConverter.ToInt32(bytes, index);
            }
            index += 4;
            return result;
        }

        public static void ToBytes(byte[] bytes, byte value, ref int index)
        {
            bytes[index] = value;
            index += 1;
        }

        public static void ToBytes(byte[] bytes, ushort value, ref int index)
        {
            byte[] valueBytes = BitConverter.GetBytes(value);
            if (!BitConverter.IsLittleEndian)
            {
                Array.Reverse(valueBytes);
            }
            bytes[index++] = valueBytes[0];
            bytes[index++] = valueBytes[1];
        }

        public static void ToBytes(byte[] bytes, uint value, ref int index)
        {
            byte[] valueBytes = BitConverter.GetBytes(value);
            if (!BitConverter.IsLittleEndian)
            {
                Array.Reverse(valueBytes);
            }
            bytes[index++] = valueBytes[0];
            bytes[index++] = valueBytes[1];
            bytes[index++] = valueBytes[2];
            bytes[index++] = valueBytes[3];
        }

        public static void ToBytes(byte[] bytes, int value, ref int index)
        {
            byte[] valueBytes = BitConverter.GetBytes(value);
            if (!BitConverter.IsLittleEndian)
            {
                Array.Reverse(valueBytes);
            }
            bytes[index++] = valueBytes[0];
            bytes[index++] = valueBytes[1];
            bytes[index++] = valueBytes[2];
            bytes[index++] = valueBytes[3];
        }
    }
}
