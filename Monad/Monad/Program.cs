using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaybeAsASumType;

namespace Monad
{
    class Program
    {
        

        static void Main(string[] args)
        {
            Test1();
            Test2();
            Test3();
        }

        static void Test1()
        {
            var contents = GetLogContents(1);

            if (contents is Maybe<string>.Some some)
            {
                Console.WriteLine(some.Value);
            }
            else
            {
                Console.WriteLine("Log file not found");
            }
        }

        static void Test2()
        {
            var contents = GetLogContents(1);

            if (contents.TryGetValue(out var value))
            {
                Console.WriteLine(value);
            }
            else
            {
                Console.WriteLine("Log file not found");
            }
        }

        static void Test3()
        {
            var contents = GetLogContents(1);

            contents.Match(some: value =>
                {
                    Console.WriteLine(value);
                },
                none: () =>
                {
                    Console.WriteLine("Log file not found");
                });
        }

        static Maybe<string> GetLogContents(int id)
        {
            var filename = "c:\\logs\\" + id + ".log";

            if (File.Exists(filename))
                return File.ReadAllText(filename);

            return Maybe.None;
        }
    }
}
