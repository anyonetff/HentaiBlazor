using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HentaiBlazor.Common
{
    public class DigestUtils
    {

        private static readonly MD5 _md5 = MD5.Create();

        private static readonly SHA1 _sha1 = SHA1.Create();

        public static byte[] Md5(byte[] buffer)
        {
            return _md5.ComputeHash(buffer);
        }

        public static byte[] Md5(Stream stream)
        {
            return _md5.ComputeHash(stream);
        }

        public static byte[] Md5(string data)
        {
            return Md5(Encoding.Default.GetBytes(data));
        }

        public static string Md5Hex(byte[] data)
        {
            return Convert.ToHexString(Md5(data));
        }

        public static string Md5Hex(Stream stream)
        {
            return Convert.ToHexString(Md5(stream));
        }

        public static string Md5Hex(string data)
        {
            return Convert.ToHexString(Md5(data));
        }

    }
}
