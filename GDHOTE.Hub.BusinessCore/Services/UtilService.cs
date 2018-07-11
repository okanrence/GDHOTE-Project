using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using GDHOTE.Hub.CoreObject.Enumerables;

namespace GDHOTE.Hub.BusinessCore.Services
{
    public class UtilService
    {
        public static string HashSha512(string randomString)
        {
            SHA512Managed crypt = new SHA512Managed();
            string hash = String.Empty;
            byte[] crypto = crypt.ComputeHash(Encoding.ASCII.GetBytes(randomString), 0, Encoding.ASCII.GetByteCount(randomString));
            foreach (byte theByte in crypto)
            {
                hash += theByte.ToString("x2");
            }
            return hash;
        }
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public static string CreateHash(string input, HashTypes hashType, HashEncoding hashEncoding)
        {
            HashAlgorithm hashAlgorithm = null;

            switch (hashType)
            {
                case HashTypes.Sha256:
                    hashAlgorithm = new SHA256Managed();
                    break;
                case HashTypes.Sha512:
                    hashAlgorithm = new SHA512Managed();
                    break;
                case HashTypes.Sha1:
                    hashAlgorithm = new SHA1Managed();
                    break;
                case HashTypes.Sha384:
                    hashAlgorithm = new SHA384Managed();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(hashType), hashType, null);
            }


            var bytes = Encoding.UTF8.GetBytes(input.Trim());

            var hashBytes = hashAlgorithm.ComputeHash(bytes);

            string hashValue;

            switch (hashEncoding)
            {
                case HashEncoding.Base64:
                    hashValue = Convert.ToBase64String(hashBytes);
                    break;
                case HashEncoding.Hex:
                    var hex = BitConverter.ToString(hashBytes);
                    hashValue = hex.Replace("-", "");
                    break;
                default:
                    hashValue = Encoding.UTF8.GetString(hashBytes);
                    break;
            }
            return hashValue;
        }


    }
}
