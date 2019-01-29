using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Acapulco_Bot.Utils.Hash
{
    internal class SHA1
    {
        public static string Encode(string hashText)
        {
            byte[] source = new SHA1Managed().ComputeHash(Encoding.UTF8.GetBytes(hashText));
            return string.Join("", (from b in source select b.ToString("x2")).ToArray<string>());
        }
    }

}
