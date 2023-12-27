using System;
using System.Diagnostics;

namespace Pine
{
    class ArgReader
    {
        private static List<string> HelpfulHints = new List<string>(){
            "-b to build a .pine file",
            "-d to enter debug mode",
            "-e to get the compiler edition",
            "-br to run build a .pine file and then run the output"
        };

        public static bool IncludedFiles = false;
        public static List<string> IncludedFileNames = new List<string>();

        private static void CheckForIncludedFiles()
        {
            if (IncludedFiles)
            {
                for (int i = 0; i < IncludedFileNames.Count; i++)
                {
                    FileChecker.CheckFile(IncludedFileNames[i]);
                    Tokenizer.Tokenize(IncludedFileNames[i]);
                }
            }
        }

        private static void WriteTokens()
        {
            using (StreamWriter sw = new StreamWriter("Main.c"))
                for (int i = 0; i < Converter.ConvertedTokens.Count; i++)
                    sw.Write(Converter.ConvertedTokens[i]);
        }

        public static void Main(string[] args)
        {
            for (int i = 0; i < args.Length; i++)
                switch (args[i])
                {
                    case "-d":
                        GCCComp.DebugMode = true;
                        break;
                    case "-e":
                        Console.WriteLine("Version 0.1");
                        break;
                    case "-b":
                        FileChecker.CheckFile(args[i + 1]);
                        Tokenizer.Tokenize(args[i + 1]);
                        CheckForIncludedFiles();
                        WriteTokens();
                        GCCComp.Compile(args[i + 2]);
                        break;
                    case "-br":
                        FileChecker.CheckFile(args[i + 1]);
                        Tokenizer.Tokenize(args[i + 1]);
                        CheckForIncludedFiles();
                        WriteTokens();
                        GCCComp.Compile(args[i + 2]);
                        //requires small tiny micro sleep because the program keeps crashing when trying to run the binary
                        System.Threading.Thread.Sleep(250);
                        if (File.Exists(args[i + 2]))
                            Process.Start(args[i + 2]);
                        break;
                    case "help":
                        for (int j = 0; j < HelpfulHints.Count; j++)
                            Console.WriteLine(HelpfulHints[j]);
                        break;
                }
        }
    }
}