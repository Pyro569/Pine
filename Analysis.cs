class Analysis
{
    public static void AnalyzeTokens(List<string> Tokens)
    {
        for (int i = 0; i < Tokens.Count; i++)
        {
            switch (Tokens[i])
            {
                case "break":
                    if (Tokens[i + 1] == ";")
                    {
                        if (Tokens[i + 2] != "}")
                        {
                            //ReturnAnalyzedMistake("Unreachable code", new List<int>() { i, i + 1, i + 2, i + 3 }, Tokens);
                        }
                    }
                    break;
            }
        }
    }

    private static void ReturnAnalyzedMistake(string Mistake, List<int> TokenIndexicies, List<string> Tokens)
    {
        Console.Write("Analysis: " + Mistake + " at ");
        for (int i = 0; i < TokenIndexicies.Count; i++)
            Console.Write(Tokens[TokenIndexicies[i]]);
        Console.Write("\n\n");
    }
}