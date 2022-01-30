using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RoboKiwi.Functions.Helpers
{
    class CryptoHelper
    {
        public static async Task<bool> VerifyHmacSha512(string key, string message, string hmac)
        {
            await using var stream = new MemoryStream(Encoding.UTF8.GetBytes(message));
            using var crypto = new HMACSHA512(Encoding.UTF8.GetBytes(key));
            
            var verifyBytes = await crypto.ComputeHashAsync(stream);
            var verify = verifyBytes.ToHexStringLower(0, verifyBytes.Length);

            return string.Equals(hmac, verify);
        }
    }
}
