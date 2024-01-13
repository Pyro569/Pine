class IOFunctions
{
    public static void Write(List<string> Tokens, int i, List<string> ConvertedTokens, List<string> IntsDeclared, List<string> StringsDeclared, List<string> FloatsDeclared, List<string> BoolsDeclared)
    {
        if (!ConvertedTokens.Contains("\n#include <stdio.h>\n"))
            ConvertedTokens.Insert(0, "\n#include <stdio.h>\n");

        ConvertedTokens.Add("printf");
        if (IntsDeclared.Contains(Tokens[i + 2]))
        {
            ConvertedTokens.Add("(\"%d\"," + Tokens[i + 2] + "");
            for (int k = i; k < i + 2; k++)
                Tokens.Remove(Tokens[k]);
        }
        else if (StringsDeclared.Contains(Tokens[i + 2]))
        {
            ConvertedTokens.Add("(\"%s\"," + Tokens[i + 2] + "");
            for (int k = i; k < i + 2; k++)
                Tokens.Remove(Tokens[k]);
        }
        else if (FloatsDeclared.Contains(Tokens[i + 2]))
        {
            ConvertedTokens.Add("(\"%.20f\"," + Tokens[i + 2] + "");
            for (int k = i; k < i + 2; k++)
                Tokens.Remove(Tokens[k]);
        }
        else if (BoolsDeclared.Contains(Tokens[i + 2]))
        {
            ConvertedTokens.Add("(\"%d\"," + Tokens[i + 2] + "");
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

    public static void createFile(List<string> Tokens, int i, List<string> ConvertedTokens, List<string> StringsDeclared)
    {
        if (!ConvertedTokens.Contains("\n#include <stdio.h>\n"))
            ConvertedTokens.Insert(0, "\n#include <stdio.h>\n");

        if (!StringsDeclared.Contains("fptr"))
        {
            ConvertedTokens.Add("FILE *fptr;");
            StringsDeclared.Add("fptr");
        }
        ConvertedTokens.Add("fptr = fopen");

        int removeTo = 0;

        for (int j = i + 1; j < Tokens.Count; j++)
        {
            if (Tokens[j] != ")")
                ConvertedTokens.Add(Tokens[j]);
            else
            {
                removeTo = j - 1;
                break;
            }
        }

        ConvertedTokens[ConvertedTokens.Count - 1] += ", \"w\"";

        for (int j = removeTo; j > i; j--)
            Tokens.Remove(Tokens[j]);
    }

    public static void writeFile(List<string> Tokens, int i, List<string> ConvertedTokens, List<string> StringsDeclared)
    {
        if (!ConvertedTokens.Contains("\n#include <stdio.h>\n"))
            ConvertedTokens.Insert(0, "\n#include <stdio.h>\n");

        if (!StringsDeclared.Contains("fptr"))
        {
            ConvertedTokens.Add("FILE *fptr;");
            StringsDeclared.Add("fptr");
        }
        ConvertedTokens.Add("fptr = fopen");

        int removeTo = 0;

        for (int j = i + 1; j < Tokens.Count; j++)
        {
            if (Tokens[j] != ",")
                ConvertedTokens.Add(Tokens[j]);
            else
            {
                removeTo = j;
                break;
            }
        }

        ConvertedTokens[ConvertedTokens.Count - 1] += ", \"w\");";

        ConvertedTokens.Add("fprintf(fptr, ");

        for (int j = removeTo + 1; j < Tokens.Count; j++)
        {
            if (Tokens[j] != ")" && Tokens[j + 1] != ";")
                ConvertedTokens.Add(Tokens[j]);
            else if (Tokens[j] == ")" && Tokens[j + 1] != ";")
                ConvertedTokens.Add(Tokens[j]);
            else if (Tokens[j] != ")" && Tokens[j + 1] == ";")
                ConvertedTokens.Add(Tokens[j]);
            else
            {
                removeTo = j - 1;
                break;
            }
        }

        for (int j = removeTo; j > i; j--)
            Tokens.Remove(Tokens[j]);
    }
}