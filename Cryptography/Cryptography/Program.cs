using static System.Console;
using System.Security.Cryptography;

namespace Cryptography
{
    class Program
    {
        public static string ContainerName { get; private set; }

        static void Main(string[] args)
        {
        }

        private static byte[] SignMessage(string message, out byte[] hashValue)
        {
            SHA256
            using (var sha256 = SHA256Managed.Create())
            {

            }
        }
    }
}
