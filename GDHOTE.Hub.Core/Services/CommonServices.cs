using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GDHOTE.Hub.Core.Services
{
    public class CommonServices
    {

        public static string CreateHash(string input, EnumsService.HashTypes hashType, EnumsService.HashEncoding hashEncoding)
        {
            HashAlgorithm hashAlgorithm = null;

            switch (hashType)
            {
                case EnumsService.HashTypes.Sha256:
                    hashAlgorithm = (HashAlgorithm)new SHA256Managed();
                    break;
                case EnumsService.HashTypes.Sha512:
                    hashAlgorithm = (HashAlgorithm)new SHA512Managed();
                    break;
                case EnumsService.HashTypes.Sha1:
                    hashAlgorithm = (HashAlgorithm)new SHA1Managed();
                    break;
                case EnumsService.HashTypes.Sha384:
                    hashAlgorithm = (HashAlgorithm)new SHA384Managed();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(hashType), hashType, null);
            }


            var bytes = Encoding.UTF8.GetBytes(input.Trim());

            var hashBytes = hashAlgorithm.ComputeHash(bytes);

            string hashValue;

            switch (hashEncoding)
            {
                case EnumsService.HashEncoding.Base64:
                    hashValue = Convert.ToBase64String(hashBytes);
                    break;
                case EnumsService.HashEncoding.Hex:
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
