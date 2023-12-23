class Converter
{
    public static List<string> ConvertedTokens = new List<string>();

    static bool inQuotation = false;

    public static void Convert(List<string> Tokens)
    {
        for (int i = 0; i < Tokens.Count; i++)
        {
            switch (Tokens[i])
            {
                case "(":
                    ConvertedTokens.Add("(");
                    break;
                case ")":
                    ConvertedTokens.Add(")");
                    break;
                case "\"":
                    ConvertedTokens.Add("\"");
                    if (inQuotation)
                        inQuotation = false;
                    else
                        inQuotation = true;
                    break;
                case ";":
                    ConvertedTokens.Add(";");
                    break;
                case ",":
                    ConvertedTokens.Add(",");
                    break;
                case " ":
                    ConvertedTokens.Add(" ");
                    break;
                case "{":
                    ConvertedTokens.Add("{");
                    break;
                case "}":
                    ConvertedTokens.Add("}");
                    break;
                case "fn":
                    if (Tokens[i + 2] == "main")
                        ConvertedTokens.Add("int");
                    else
                        ConvertedTokens.Add("void");
                    break;
                case "main":
                    ConvertedTokens.Add("main");
                    break;
                case "args":
                    ConvertedTokens.Add("int argc, char** argv");
                    break;
                case "import":
                    switch (Tokens[i + 2])
                    {
                        case "io":
                            ConvertedTokens.Add("#include <stdio.h>\n");
                            break;
                    }
                    break;
                case "write":
                    ConvertedTokens.Add("printf");
                    break;
                default:
                    if (inQuotation == true)
                        ConvertedTokens.Add(Tokens[i]);
                    break;
            }
        }

        using (StreamWriter sw = new StreamWriter("Main.cpp"))
        {
            for (int i = 0; i < ConvertedTokens.Count; i++)
            {
                sw.Write(ConvertedTokens[i]);
            }
        }
    }
}