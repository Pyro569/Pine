class IOFunctions
{
    public static void Write(List<string> Tokens, int i, List<string> ConvertedTokens, List<string> IntsDeclared, List<string> StringsDeclared, List<string> FloatsDeclared, List<string> BoolsDeclared)
    {
        if (!ConvertedTokens.Contains("\n#include <stdio.h>\n"))
            ConvertedTokens.Insert(0, "\n#include <stdio.h>\n");

        ConvertedTokens.Add("printf");
        if (IntsDeclared.Contains(Tokens[i + 2]))
        {
            ConvertedTokens.Add("(\"%d\\n\"," + Tokens[i + 2] + "");
            for (int k = i; k < i + 2; k++)
                Tokens.Remove(Tokens[k]);
        }
        else if (StringsDeclared.Contains(Tokens[i + 2]))
        {
            ConvertedTokens.Add("(\"%s\\n\"," + Tokens[i + 2] + "");
            for (int k = i; k < i + 2; k++)
                Tokens.Remove(Tokens[k]);
        }
        else if (FloatsDeclared.Contains(Tokens[i + 2]))
        {
            ConvertedTokens.Add("(\"%.20f\\n\"," + Tokens[i + 2] + "");
            for (int k = i; k < i + 2; k++)
                Tokens.Remove(Tokens[k]);
        }
        else if (BoolsDeclared.Contains(Tokens[i + 2]))
        {
            ConvertedTokens.Add("(\"%d\\n\"," + Tokens[i + 2] + "");
            for (int k = i; k < i + 2; k++)
                Tokens.Remove(Tokens[k]);
        }
    }

    public static void input(List<string> Tokens, int i, List<string> ConvertedTokens, List<string> IntsDeclared, List<string> StringsDeclared, List<string> FloatsDeclared)
    {
        if (!ConvertedTokens.Contains("\n#include <stdio.h>\n"))
            ConvertedTokens.Insert(0, "\n#include <stdio.h>\n");

        ConvertedTokens.Add("scanf");
        if (IntsDeclared.Contains(Tokens[i + 2]))
        {
            ConvertedTokens.Add("(\"%d\",&" + Tokens[i + 2] + "");
            for (int k = i; k < i + 2; k++)
                Tokens.Remove(Tokens[k]);
        }
        else if (StringsDeclared.Contains(Tokens[i + 2]))
        {
            ConvertedTokens.Add("(\"%s\",&" + Tokens[i + 2] + "");
            for (int k = i; k < i + 2; k++)
                Tokens.Remove(Tokens[k]);
        }
        else if (FloatsDeclared.Contains(Tokens[i + 2]))
        {
            ConvertedTokens.Add("(\"%.20f\",&" + Tokens[i + 2] + "");
            for (int k = i; k < i + 2; k++)
                Tokens.Remove(Tokens[k]);
        }
    }


}