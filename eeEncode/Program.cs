using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace eeEncode
{
    internal class Program
    {
        static readonly Dictionary<char, string> CONVERSION = new()
        {
            {'a', "        a"},
            {'b', "       be"},
            {'c', "       ce"},
            {'d', "       de"},
            {'e', "        e"},
            {'f', "      efe"},
            {'g', "       ge"},
            {'h', "    hache"},
            {'i', "        i"},
            {'j', "     jota"},
            {'k', "       ka"},
            {'l', "      ele"},
            {'m', "      eme"},
            {'n', "      ene"},
            {'ñ', "      eñe"},
            {'o', "        o"},
            {'p', "       pe"},
            {'q', "       cu"},
            {'r', "     erre"},
            {'s', "      ese"},
            {'t', "       te"},
            {'u', "        u"},
            {'v', "      uve"},
            {'w', "uve doble"},
            {'x', "    equis"},
            {'y', " i griega"},
            {'z', "     zeta"},
            {' ', "         "}
        };
        static readonly int BUFFER_SIZE = 1024;

        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: eeEncode <input> <output>");
                return;
            }
            string inputFile = args[args.Length - 2], outputFile = args[args.Length - 1];

            using (FileStream input = new(inputFile, FileMode.Open, FileAccess.Read))
            {
                using (FileStream output = new(outputFile, FileMode.Create, FileAccess.Write))
                {
                    byte[] buffer = new byte[BUFFER_SIZE];
                    int read;
                    while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        output.Write(Encode(buffer, read));
                    }
                }
            }
        }

        static byte[] Encode(byte[] input, int read)
        {
            StringBuilder sb = new();
            for (int i = 0; i < read; i++)
            {
                //Console.WriteLine(input[i]);
                //Console.WriteLine("h:"+ (input[i] >> 4));
                //Console.WriteLine("l:"+(input[i] & 0x0f));
                sb.Append(CONVERSION[(char)input[i]]);
            }
            return Encoding.ASCII.GetBytes(sb.ToString());
        }
    }
}