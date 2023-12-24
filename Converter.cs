class Converter
{
    public static List<string> ConvertedTokens = new List<string>();
    private static List<string> IntsDeclared = new List<string>();
    private static List<string> StringsDeclared = new List<string>();
    private static List<string> FloatsDeclared = new List<string>();
    private static List<string> FunctionsDeclared = new List<string>();
    private static List<string> BoolsDeclared = new List<string>();

    static bool inQuotation = false;

    public static void Convert(List<string> Tokens)
    {
        for (int i = 0; i < Tokens.Count; i++)
        {
            switch (Tokens[i])
            {
                case "true":
                    ConvertedTokens.Add("true");
                    break;
                case "false":
                    ConvertedTokens.Add("false");
                    break;
                case ">":
                    ConvertedTokens.Add(">");
                    break;
                case "<":
                    ConvertedTokens.Add("<");
                    break;
                case "!":
                    ConvertedTokens.Add("!");
                    break;
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
                case "=":
                    ConvertedTokens.Add("=");
                    break;
                case "+":
                    ConvertedTokens.Add("+");
                    break;
                case "-":
                    ConvertedTokens.Add("-");
                    break;
                case "*":
                    ConvertedTokens.Add("*");
                    break;
                case "fn":
                    if (Tokens[i + 2] == "main")
                        ConvertedTokens.Add("int");
                    else
                    {
                        ConvertedTokens.Add("void ");
                        ConvertedTokens.Add(Tokens[i + 2]);
                        FunctionsDeclared.Add(Tokens[i + 2]);
                        Tokens.Remove(Tokens[i + 2]);
                    }
                    break;
                case "main":
                    ConvertedTokens.Add("main");
                    break;
                case "args":
                    ConvertedTokens.Add("int argc, char** argv");
                    break;
                case "import":
                    Imports.Import(Tokens, i, ConvertedTokens);
                    break;
                case "write":
                    IOFunctions.Write(Tokens, i, ConvertedTokens, IntsDeclared, StringsDeclared, FloatsDeclared, BoolsDeclared);
                    break;
                case "int":
                    ConvertedTokens.Add("int ");
                    if (Tokens[i + 1] == " ")
                    {
                        ConvertedTokens.Add(Tokens[i + 2]);
                        IntsDeclared.Add(Tokens[i + 2]);
                        Tokens.Remove(Tokens[i + 2]);
                    }
                    break;
                case "string":
                    ConvertedTokens.Add("char ");
                    if (Tokens[i + 1] == " ")
                    {
                        ConvertedTokens.Add(Tokens[i + 2] + "[255]");
                        StringsDeclared.Add(Tokens[i + 2]);
                        Tokens.Remove(Tokens[i + 2]);
                    }
                    break;
                case "float":
                    ConvertedTokens.Add("float ");
                    if (Tokens[i + 1] == " ")
                    {
                        ConvertedTokens.Add(Tokens[i + 2]);
                        FloatsDeclared.Add(Tokens[i + 2]);
                        Tokens.Remove(Tokens[i + 2]);
                    }
                    break;
                case "bool":
                    ConvertedTokens.Add("bool ");
                    if (Tokens[i + 1] == " ")
                    {
                        ConvertedTokens.Add(Tokens[i + 2]);
                        BoolsDeclared.Add(Tokens[i + 2]);
                        Tokens.Remove(Tokens[i + 2]);
                    }
                    break;
                case "if":
                    ConvertedTokens.Add("if");
                    break;

                case "else":
                    ConvertedTokens.Add("else");
                    break;

                case "while":
                    ConvertedTokens.Add("while");
                    break;
                case "for":
                    ConvertedTokens.Add("for");
                    break;
                case "/":
                    if (Tokens[i + 1] == "/")
                    {
                        for (int k = i; k < Tokens.Count; k++)
                        {
                            if (Tokens[k] != "/" && Tokens[k + 1] != "/")
                            {
                                Tokens.Remove(Tokens[k]);
                            }
                            else
                                break;
                        }
                    }
                    else
                        ConvertedTokens.Add("/");
                    break;
                case "return":
                    ConvertedTokens.Add("return");
                    break;
                case "input":
                    IOFunctions.input(Tokens, i, ConvertedTokens, IntsDeclared, StringsDeclared, FloatsDeclared);
                    break;
                default:
                    if (inQuotation == true)
                        ConvertedTokens.Add(Tokens[i]);
                    else if (Tokens[i].All(char.IsDigit))
                        ConvertedTokens.Add(Tokens[i]);
                    else if (IntsDeclared.Contains(Tokens[i]))
                        ConvertedTokens.Add(Tokens[i]);
                    else if (FloatsDeclared.Contains(Tokens[i]))
                        ConvertedTokens.Add(Tokens[i]);
                    else if (FunctionsDeclared.Contains(Tokens[i]))
                        ConvertedTokens.Add(Tokens[i]);
                    else if (BoolsDeclared.Contains(Tokens[i]))
                        ConvertedTokens.Add(Tokens[i]);
                    //write the string variable or reallocate the value
                    else if (StringsDeclared.Contains(Tokens[i]))
                    {
                        //too much string logic so it has been broken into a separate class
                        StringLogic.StringTokenLogic(Tokens, i, ConvertedTokens);
                    }
                    //check if the number is a proper float number i.e only one decimal point
                    else if (Tokens[i].Contains('.'))
                    {
                        string[] splitToken = Tokens[i].Split(".");
                        if (splitToken[0].All(char.IsDigit) && splitToken[1].All(char.IsDigit) && splitToken.Length == 2)
                            ConvertedTokens.Add(Tokens[i]);
                    }

                    break;
            }
        }

        //write all of the converted tokens to the Main.cpp file
        using (StreamWriter sw = new StreamWriter("Main.cpp"))
        {
            for (int i = 0; i < ConvertedTokens.Count; i++)
            {
                sw.Write(ConvertedTokens[i]);
            }
        }
    }
}