using System.Security.Cryptography;
using System.Text;

namespace UpperRockPapperScissors
{
    internal class Hmac
    {
        private readonly HMACSHA256 _hmac;

        public Hmac(string key, string computerMotion) => _hmac = new HMACSHA256(Encoding.UTF8.GetBytes(key + computerMotion.ToUpper()));

        public string ComputeHash() => Convert.ToHexString(_hmac.ComputeHash(_hmac.Key));
    }
}
