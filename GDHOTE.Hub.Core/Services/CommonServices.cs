using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using GDHOTE.Hub.Core.Enumerables;

namespace GDHOTE.Hub.Core.Services
{
    public class CommonServices
    {

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
