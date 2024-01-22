class stdLib
{
    public static void DetermineLib(List<string> Tokens, int i, List<string> ConvertedTokens)
    {
        switch (Tokens[i + 2])
        {
            case "math":
                for (int z = 0; z < Math.MathScript.Count; z++)
                {
                    int placement = i + 3;
                    ConvertedTokens.Add(Math.MathScript[z]);
                }
                break;
        }

        Converter.IntsDeclared.Add("sqrt");
    }
}