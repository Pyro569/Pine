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
                        GCCComp.Compile(args[i + 2]);
                        break;
                    case "-br":
                        FileChecker.CheckFile(args[i + 1]);
                        Tokenizer.Tokenize(args[i + 1]);
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