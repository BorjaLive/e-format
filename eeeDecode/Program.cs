using System;
using System.IO;
using System.Text;

namespace eeeDecode
{
    internal class Program
    {
        static readonly Dictionary<string, char> CONVERSION = new()
        {
            {"    I", 'a'},
            {"   II", 'b'},
            {"  III", 'c'},
            {"   IV", 'd'},
            {"    V", 'e'},
            {"   VI", 'f'},
            {"  VII", 'g'},
            {" VIII", 'h'},
            {"   IX", 'i'},
            {"    X", 'j'},
            {"   XI", 'k'},
            {"  XII", 'l'},
            {" XIII", 'm'},
            {"  XIV", 'n'},
            {"   XV", 'ñ'},
            {"  XVI", 'o'},
            {" XVII", 'p'},
            {"XVIII", 'q'},
            {"  XIX", 'r'},
            {"   XX", 's'},
            {"  XXI", 't'},
            {" XXII", 'u'},
            {"XXIII", 'v'},
            {" XXIV", 'w'},
            {"  XXV", 'x'},
            {" XXVI", 'y'},
            {"XXVII", 'z'},
            {"     ", ' '}
        };
        static readonly int BUFFER_SIZE = 1020;

        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: eeeDecode <input> <output>");
                return;
            }
            string inputFile = args[args.Length - 2], outputFile = args[args.Length - 1];

            using (FileStream input = new(inputFile, FileMode.Open, FileAccess.Read))
            {
                using (FileStream output = new(outputFile, FileMode.Create, FileAccess.Write))
                {
                    byte[] inBbuffer = new byte[BUFFER_SIZE];
                    byte[] outBbuffer = new byte[BUFFER_SIZE / 5];
                    int read;
                    while ((read = input.Read(inBbuffer, 0, inBbuffer.Length)) > 0)
                    {
                        Decode(inBbuffer, outBbuffer, read);
                        output.Write(outBbuffer, 0, read / 5);
                    }
                }
            }
        }

        static void Decode(byte[] input, byte[] output, int read)
        {
            string str = Encoding.ASCII.GetString(input);
            for (int i = 0; i < read / 5; i++)
            {
                output[i] = (byte)(CONVERSION[str.Substring(i * 5, 5)]);
            }
        }
    }
}