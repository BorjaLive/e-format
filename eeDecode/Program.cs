using System;
using System.IO;
using System.Text;

namespace eeDecode
{
    internal class Program
    {
        static readonly Dictionary<string, char> CONVERSION = new()
        {
            {"        a", 'a'},
            {"       be", 'b'},
            {"       ce", 'c'},
            {"       de", 'd'},
            {"        e", 'e'},
            {"      efe", 'f'},
            {"       ge", 'g'},
            {"    hache", 'h'},
            {"        i", 'i'},
            {"     jota", 'j'},
            {"       ka", 'k'},
            {"      ele", 'l'},
            {"      eme", 'm'},
            {"      ene", 'n'},
            {"      eñe", 'ñ'},
            {"        o", 'o'},
            {"       pe", 'p'},
            {"       cu", 'q'},
            {"     erre", 'r'},
            {"      ese", 's'},
            {"       te", 't'},
            {"        u", 'u'},
            {"      uve", 'v'},
            {"uve doble", 'w'},
            {"    equis", 'x'},
            {" i griega", 'y'},
            {"     zeta", 'z'},
            {"         ", ' '}
        };
        static readonly int BUFFER_SIZE = 1017;

        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: eeDecode <input> <output>");
                return;
            }
            string inputFile = args[args.Length - 2], outputFile = args[args.Length - 1];

            using (FileStream input = new(inputFile, FileMode.Open, FileAccess.Read))
            {
                using (FileStream output = new(outputFile, FileMode.Create, FileAccess.Write))
                {
                    byte[] inBbuffer = new byte[BUFFER_SIZE];
                    byte[] outBbuffer = new byte[BUFFER_SIZE / 9];
                    int read;
                    while ((read = input.Read(inBbuffer, 0, inBbuffer.Length)) > 0)
                    {
                        Decode(inBbuffer, outBbuffer, read);
                        output.Write(outBbuffer, 0, read / 9);
                    }
                }
            }
        }

        static void Decode(byte[] input, byte[] output, int read)
        {
            string str = Encoding.ASCII.GetString(input);
            for (int i = 0; i < read / 9; i++)
            {
                //Console.WriteLine((byte)(CONVERSION[str.Substring(i * 16, 8)] << 4 | CONVERSION[str.Substring(i * 16 + 8, 8)]));
                //Console.WriteLine("h:"+ str.Substring(i * 16, 8));
                //Console.WriteLine("l:"+ str.Substring(i * 16 + 8, 8));
                output[i] = (byte)(CONVERSION[str.Substring(i * 9, 9)]);
            }
        }
    }
}