using System;
using SQLEncoderLibrary;

namespace SQLEncoder
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            // encode hello world
            //string encodedText = encode("Hello World!");
            //Console.WriteLine($"The encoded result is {encodedText}");
            string encodedText = encode("' OR 1=1");
            Console.WriteLine($"The encoded result is {encodedText}");
            encodedText = encode("abc; TRUNCATE TABLE ABC --");
            Console.WriteLine($"The encoded result is {encodedText}");

            Console.Read();
        }

        static string encode(string input)
        {
            return new Encoder().EncodeForSql(input);

        }
    }
}
