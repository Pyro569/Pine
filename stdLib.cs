class stdLib
{
    public static void DetermineLib(List<string> Tokens, int i, List<string> ConvertedTokens)
    {
        switch (Tokens[i + 2])
        {
            case "math":
                for (int z = 0; z < Math.MathScript.Count; z++)
                    ConvertedTokens.Add(Math.MathScript[z]);

                Converter.IntsDeclared.Add("sqrt");
                Converter.IntsDeclared.Add("pow");
                break;
        }
    }
}