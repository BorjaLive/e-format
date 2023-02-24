using System.Diagnostics;

namespace eOpener
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Usage: eOpener <input>");
                return;
            }
            string inputFile = args[args.Length - 1];

            string tmpDir = GetTemporaryDirectory();
            string outputFile = Path.Combine(tmpDir, Path.GetFileNameWithoutExtension(inputFile));

            Process conversion = new Process();
            conversion.StartInfo.FileName = "eDecode";
            conversion.StartInfo.Arguments = $"\"{inputFile}\" \"{outputFile}\"";
            conversion.StartInfo.UseShellExecute = false;
            conversion.StartInfo.CreateNoWindow = true;
            conversion.Start();
            conversion.WaitForExit();

            Process opening = new Process();
            opening.StartInfo.FileName = "explorer";
            opening.StartInfo.Arguments = $"\"{outputFile}\"";
            opening.StartInfo.UseShellExecute = false;
            opening.StartInfo.CreateNoWindow = true;
            opening.StartInfo.WorkingDirectory = tmpDir;
            opening.Start();
            opening.WaitForExit();
        }

        static string GetTemporaryDirectory()
        {
            string tempDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(tempDirectory);
            return tempDirectory;
        }
    }
}