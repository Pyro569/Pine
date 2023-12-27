class Imports
{
    public static void Import(List<string> Tokens, int i, List<string> ConvertedTokens)
    {
        switch (Tokens[i + 2])
        {
            case "io":
                ConvertedTokens.Add("\n#include <stdio.h>\n");
                break;
            case "bool":
                ConvertedTokens.Add("\n#include <stdbool.h>\n");
                Tokens.Remove(Tokens[i + 2]);
                break;
        }
    }
}