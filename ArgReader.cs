using System;

namespace Pine
{
    class ArgReader
    {
        public static void Main(string[] args)
        {
            for (int i = 0; i < args.Length; i++)
                switch (args[i])
                {
                    case "-e":
                        Console.WriteLine("Version 0.1");
                        break;
                    case "-b":
                        FileChecker.CheckFile(args[i + 1]);
                        Tokenizer.Tokenize(args[i + 1]);
                        GCCComp.Compile(args[i + 2]);
                        break;
                }
        }
    }
}