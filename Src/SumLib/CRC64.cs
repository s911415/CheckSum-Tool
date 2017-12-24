using System;
using System.IO;
#if NUNIT
using NUnit.Framework;
#endif

namespace CheckSumTool
{
    /// <summary>
    /// http://blog.csdn.net/wangjia184/article/details/8784601
    /// </summary>
    public abstract class Crc64
    {

        protected ulong crc = 0; // crc data checksum so far.

        /// <summary>
        /// Returns the CRC32 data checksum computed so far.
        /// </summary>
        public virtual byte[] Value
        {
            get
            {
                return UIntAsByteArray(crc);
            }
        }

        /// <summary>
        /// Convert uint type number to byte array.
        /// </summary>
        /// <param name="number">Number to convert.</param>
        /// <returns>Number as byte array.</returns>
        protected virtual byte[] UIntAsByteArray(ulong number)
        {
            byte[] hexSum = new byte[8];
            hexSum[0] = (byte)((number & 0xff00000000000000) >> 56);
            hexSum[1] = (byte)((number & 0x00ff000000000000) >> 48);
            hexSum[2] = (byte)((number & 0x0000ff0000000000) >> 40);
            hexSum[3] = (byte)((number & 0x000000ff00000000) >> 32);
            hexSum[4] = (byte)((number & 0x00000000ff000000) >> 24);
            hexSum[5] = (byte)((number & 0x0000000000ff0000) >> 16);
            hexSum[6] = (byte)((number & 0x000000000000ff00) >> 8);
            hexSum[7] = (byte)((number & 0x00000000000000ff));
            return hexSum;
        }

        public static byte[] GetStreamCRC64(Stream stream, Crc64 crc64)
        {
            if ( stream == null )
                throw new ArgumentNullException("stream");

            if ( !stream.CanRead )
                throw new ArgumentException("stream is not readable.");

            stream.Position = 0;
            byte[] buf = new byte[4096];
            int len = 0;

            while ( (len = stream.Read(buf, 0, buf.Length)) != 0 )
            {
                crc64.Update(buf, 0, len);
            }

            stream.Position = 0;
            return crc64.Value;
        }

        public static byte[] GetFileCRC64(string path, Crc64 crc64)
        {
            if ( path == null )
                throw new ArgumentNullException("path");

            byte[] buf = new byte[4096];
            int len = 0;

            using ( FileStream fs = new FileStream(path, FileMode.Open) )
            {
                while ( (len = fs.Read(buf, 0, buf.Length)) != 0 )
                {
                    crc64.Update(buf, 0, len);
                }
            }

            return crc64.Value;
        }

        /// <summary>
        /// Resets the CRC64 data checksum as if no update was ever called.
        /// </summary>
        public void Reset()
        {
            crc = 0;
        }

        /// <summary>
        /// Updates the checksum with the int bval.
        /// </summary>
        /// <param name = "bval">
        /// the byte is taken as the lower 8 bits of bval
        /// </param>
        public abstract void Update(ulong bval);


        /// <summary>
        /// Updates the checksum with the bytes taken from the array.
        /// </summary>
        /// <param name="buffer">
        /// buffer an array of bytes
        /// </param>
        public virtual void Update(byte[] buffer)
        {
            Update(buffer, 0, buffer.Length);
        }

        /// <summary>
        /// Adds the byte array to the data checksum.
        /// </summary>
        /// <param name = "buf">
        /// the buffer which contains the data
        /// </param>
        /// <param name = "off">
        /// the offset in the buffer where the data starts
        /// </param>
        /// <param name = "len">
        /// the length of the data
        /// </param>
        public abstract void Update(byte[] buf, int off, int len);
    }
}
