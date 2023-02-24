using System;
using System.IO;
using System.Text;

namespace eDecode
{
    internal class Program
    {
        static readonly Dictionary<string, byte> CONVERSION = new() { { "    cero", 0 }, { "     uno", 1 }, { "     dos", 2 }, { "    tres", 3 }, { "  cuatro", 4 }, { "   cinco", 5 }, { "    seis", 6 }, { "   siete", 7 }, { "    ocho", 8 }, { "   nueve", 9 }, { "    diez", 10 }, { "    once", 11 }, { "    doce", 12 }, { "   trece", 13 }, { " catorce", 14 }, { "  quince", 15 } };
        static readonly int BUFFER_SIZE = 1024;

        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: eDecode <input> <output>");
                return;
            }
            string inputFile = args[args.Length - 2], outputFile = args[args.Length - 1];

            using (FileStream input = new(inputFile, FileMode.Open, FileAccess.Read))
            {
                using (FileStream output = new(outputFile, FileMode.Create, FileAccess.Write))
                {
                    byte[] inBbuffer = new byte[BUFFER_SIZE];
                    byte[] outBbuffer = new byte[BUFFER_SIZE / 16];
                    int read;
                    while ((read = input.Read(inBbuffer, 0, inBbuffer.Length)) > 0)
                    {
                        Decode(inBbuffer, outBbuffer, read);
                        output.Write(outBbuffer, 0, read / 16);
                    }
                }
            }
        }

        static void Decode(byte[] input, byte[] output, int read)
        {
            string str = Encoding.ASCII.GetString(input);
            for (int i = 0; i < read / 16; i++)
            {
                //Console.WriteLine((byte)(CONVERSION[str.Substring(i * 16, 8)] << 4 | CONVERSION[str.Substring(i * 16 + 8, 8)]));
                //Console.WriteLine("h:"+ str.Substring(i * 16, 8));
                //Console.WriteLine("l:"+ str.Substring(i * 16 + 8, 8));
                output[i] = (byte)(CONVERSION[str.Substring(i * 16, 8)] << 4 | CONVERSION[str.Substring(i * 16 + 8, 8)]);
            }
        }
    }
}