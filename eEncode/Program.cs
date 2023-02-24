using System;
using System.IO;
using System.Text;

namespace eEncode
{
    internal class Program
    {
        static readonly string[] CONVERSION = new[] {
            "    cero",
            "     uno",
            "     dos",
            "    tres",
            "  cuatro",
            "   cinco",
            "    seis",
            "   siete",
            "    ocho",
            "   nueve",
            "    diez",
            "    once",
            "    doce",
            "   trece",
            " catorce",
            "  quince"
        };
        static readonly int BUFFER_SIZE = 1024;

        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: eEncode <input> <output>");
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
                sb.Append(CONVERSION[input[i] >> 4]);
                sb.Append(CONVERSION[input[i] & 0x0f]);
            }
            return Encoding.ASCII.GetBytes(sb.ToString());
        }
    }
}