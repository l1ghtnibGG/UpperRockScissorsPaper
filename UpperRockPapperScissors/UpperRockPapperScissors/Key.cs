using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace UpperRockPapperScissors
{
    internal class Key
    {
        private const int KeyLength = 32;

        public string GetKey() => Convert.ToHexString(RandomNumberGenerator.GetBytes(KeyLength));
    }
}
