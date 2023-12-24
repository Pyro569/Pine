class StringLogic
{
    public static void StringTokenLogic(List<string> Tokens, int i, List<string> ConvertedTokens)
    {
        if (Tokens[i + 2] == "=" || Tokens[i + 2] == "!")
        {
            if (Tokens[i + 3] != "=")
            {
                if (!ConvertedTokens.Contains("#include <string.h>"))
                    ConvertedTokens.Insert(0, "#include <string.h>\n");

                ConvertedTokens.Add("strcpy(" + Tokens[i] + ",\"");
                int stopPoint = 0;
                for (int z = i + 5; z < Tokens.Count; z++)
                    if (Tokens[z] != "\"")
                        ConvertedTokens.Add(Tokens[z]);
                    else
                    {
                        ConvertedTokens.Add("\")");
                        stopPoint = z;
                        break;
                    }

                for (int k = stopPoint; k > i; k--)
                    Tokens.Remove(Tokens[k]);
            }
            else
            {
                if (!ConvertedTokens.Contains("#include <string.h>"))
                    ConvertedTokens.Insert(0, "#include <string.h>\n");

                ConvertedTokens.Add("0 ");
                if (Tokens[i + 2] == "!")
                    ConvertedTokens.Add("!");
                else
                    ConvertedTokens.Add("=");

                ConvertedTokens.Add("= strcmp(" + Tokens[i] + ",");

                int stopPoint = i + 5;
                if (Tokens[i + 5] == "\"")
                {
                    ConvertedTokens.Add("\"");
                    for (int z = i + 6; z < Tokens.Count; z++)
                    {
                        if (Tokens[z] != "\"")
                            ConvertedTokens.Add(Tokens[z]);
                        else
                        {
                            ConvertedTokens.Add(Tokens[z]);
                            stopPoint = z;
                            break;
                        }
                    }
                    ConvertedTokens.Add(")");
                }
                else
                {
                    ConvertedTokens.Add(Tokens[i + 5]);
                }

                for (int k = i; k < stopPoint; k++)
                    Tokens.Remove(Tokens[k]);
            }
        }
        else if (Tokens[i + 2] == "+" && Tokens[i + 3] == "=")
        {
            if (!ConvertedTokens.Contains("#include <cstring>"))
                ConvertedTokens.Insert(0, "#include <cstring>\n");

            int stopPoint = i + 8;
            ConvertedTokens.Add("strcat(" + Tokens[i] + ",\"");
            for (int k = i + 6; k < Tokens.Count; k++)
            {
                if (Tokens[k] != "\"")
                    ConvertedTokens.Add(Tokens[k]);
                else
                {
                    ConvertedTokens.Add(Tokens[k]);
                    stopPoint = k;
                    break;
                }
            }
            ConvertedTokens.Add(");");

            for (int z = stopPoint + 1; z > i; z--)
                Tokens.Remove(Tokens[z]);
        }
        else
            ConvertedTokens.Add(Tokens[i]);
    }
}