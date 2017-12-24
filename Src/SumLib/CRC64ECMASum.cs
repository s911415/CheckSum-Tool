/*
The MIT License

Copyright (c) 2007-2008 Ixonos Plc
Copyright (c) 2007-2008 Kimmo Varis <kimmov@winmerge.org>

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/

using System;
using System.IO;
#if NUNIT
using NUnit.Framework;
#endif

namespace CheckSumTool.SumLib
{
    /// <summary>
    /// Calculates and verifies CRC64 checksums.
    /// </summary>
    public class CRC64ECMASum : ICheckSum
    {
        /// <summary>
        /// Calculate checksum from byte table data.
        /// </summary>
        /// <param name="data">Data as byte table.</param>
        /// <returns>Checksum.</returns>
        public byte[] Calculate(byte[] data)
        {
            MemoryStream ms = new MemoryStream(data);
            byte[] hexSum = Crc64ECMA.GetStreamCRC64(ms);
            ms.Close();
            return hexSum;
        }

        /// <summary>
        /// Calculate checksum from stream data.
        /// </summary>
        /// <param name="stream">Data as stream.</param>
        /// <returns>Checksum.</returns>
        public byte[] Calculate(Stream stream)
        {
            byte[] hexSum = Crc64ECMA.GetStreamCRC64(stream);
            return hexSum;
        }

        /// <summary>
        /// Verify given byte data against given checksum.
        /// </summary>
        /// <param name="data">Data to check as byte table.</param>
        /// <param name="sum">Checksum.</param>
        /// <returns>true if data matches checksum, false otherwise.</returns>
        public bool Verify(byte[] data, byte[] sum)
        {
            if (data == null)
                throw new ArgumentNullException("data");

            if (sum == null)
                throw new ArgumentNullException("sum");

            // Crc64 sum is 4 bytes
            if (sum.Length != 4)
                return false;

            MemoryStream msData = new MemoryStream(data);
            MemoryStream msSum = new MemoryStream(sum);
            byte[] hexSum = Crc64ECMA.GetStreamCRC64(msData);
            int ind = 0;
            bool valid = true;

            while (ind < hexSum.Length && valid)
            {
                if (hexSum[ind] != sum[ind])
                    valid = false;
                else
                    ++ind;
            }

            msData.Close();
            msSum.Close();
            return valid;
        }

        /// <summary>
        /// Verify given stream data against given checksum.
        /// </summary>
        /// <param name="stream">Stream data to check.</param>
        /// <param name="sum">Checksum.</param>
        /// <returns>true if data matches checksum, false otherwise.</returns>
        public bool Verify(Stream stream, byte[] sum)
        {
            if (sum == null)
                throw new ArgumentNullException("sum");

            // Crc64 sum is 8 bytes
            if (sum.Length != 8)
                return false;

            byte[] hexSum = Crc64ECMA.GetStreamCRC64(stream);

            for (int i = 0; i < hexSum.Length; i++)
            {
                if (hexSum[i] != sum[i])
                    return false;
            }

            return true;
        }

    }
}
