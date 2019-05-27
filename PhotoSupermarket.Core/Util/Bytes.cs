using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoSupermarket.Core.Util
{
    public class Bytes
    {
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
    }
}
