using Pine;

class Converter
{
    public static List<string> ConvertedTokens = new List<string>();
    private static List<string> IntsDeclared = new List<string>();
    private static List<string> StringsDeclared = new List<string>();
    private static List<string> FloatsDeclared = new List<string>();
    private static List<string> FunctionsDeclared = new List<string>();
    private static List<string> BoolsDeclared = new List<string>();
    private static List<string> MethodsUsed = new List<string>();

    static bool inQuotation = false;
    public static bool includedFile = false;

    private static void AddToken(string Token)
    {
        if (!includedFile)
            ConvertedTokens.Add(Token);
        else
            for (int i = 0; i < ConvertedTokens.Count; i++)
            {
                if (ConvertedTokens[i] == "main")
                    ConvertedTokens.Insert(i, Token);
            }
    }

    public static void DataControlScan(List<string> Tokens)
    {
        for (int i = 0; i < Tokens.Count; i++)
            if (Tokens[i] == "fn")
            {
                if (Tokens[i + 2] != "main")
                {
                    FunctionsDeclared.Add(Tokens[i + 2]);
                }
            }
            else if (Tokens[i] == "int")
                IntsDeclared.Add(Tokens[i + 2]);
            else if (Tokens[i] == "string")
                StringsDeclared.Add(Tokens[i + 2]);
            else if (Tokens[i] == "float")
                StringsDeclared.Add(Tokens[i + 2]);
            else if (Tokens[i] == "bool")
                BoolsDeclared.Add(Tokens[i + 2]);
    }

    public static void Convert(List<string> Tokens)
    {
        for (int i = 0; i < Tokens.Count; i++)
        {
            if (i == Tokens.Count - 1 && ArgReader.IncludedFiles == false)
                ArgReader.IncludedFiles = true;
            else if (i == Tokens.Count - 1 && ArgReader.IncludedFiles == true)
                ArgReader.IncludedFiles = false;

            switch (Tokens[i])
            {
                case "true":
                    AddToken("true");
                    break;
                case "false":
                    AddToken("false");
                    break;
                case ">":
                    AddToken(">");
                    break;
                case "<":
                    AddToken("<");
                    break;
                case "!":
                    AddToken("!");
                    break;
                case "(":
                    AddToken("(");
                    break;
                case ")":
                    AddToken(")");
                    break;
                case "\"":
                    AddToken("\"");
                    if (inQuotation)
                        inQuotation = false;
                    else
                        inQuotation = true;
                    break;
                case "\\":
                    if (Tokens[i + 1] == "\"")
                        AddToken("\\\"");
                    break;
                case ";":
                    AddToken(";");
                    if (MethodsUsed.Count >= 1)
                    {
                        if (MethodsUsed[MethodsUsed.Count - 1] == "writeLine")
                            AddToken("printf(\"\\n\");");
                        if (MethodsUsed[MethodsUsed.Count - 1] == "createFile")
                            AddToken("fclose(fptr);");
                    }
                    break;
                case ",":
                    AddToken(",");
                    break;
                case " ":
                    AddToken(" ");
                    break;
                case "{":
                    AddToken("{");
                    break;
                case "}":
                    AddToken("}");
                    break;
                case "=":
                    AddToken("=");
                    break;
                case "+":
                    if (MethodsUsed[MethodsUsed.Count - 1] != "writeLine")
                        AddToken("+");
                    break;
                case "-":
                    AddToken("-");
                    break;
                case "*":
                    AddToken("*");
                    break;
                case ":":
                    AddToken(":");
                    break;
                case "[":
                    AddToken("[");
                    break;
                case "]":
                    AddToken("]");
                    break;
                case "&":
                    AddToken("&");
                    break;
                case "fn":
                    if (Tokens[i + 2] == "main")
                        AddToken("int");
                    else
                    {
                        AddToken("void ");
                        AddToken(Tokens[i + 2]);
                        FunctionsDeclared.Add(Tokens[i + 2]);
                        Tokens.Remove(Tokens[i + 2]);
                    }
                    break;
                case "main":
                    AddToken("main");
                    break;
                case "args":
                    AddToken("int argc, char** argv");
                    IntsDeclared.Add("argc");
                    StringsDeclared.Add("argv");
                    break;
                case "import":
                    Imports.Import(Tokens, i, ConvertedTokens);
                    break;
                case "write":
                    IOFunctions.Write(Tokens, i, ConvertedTokens, IntsDeclared, StringsDeclared, FloatsDeclared, BoolsDeclared);
                    MethodsUsed.Add("write");
                    break;
                case "writeLine":
                    IOFunctions.Write(Tokens, i, ConvertedTokens, IntsDeclared, StringsDeclared, FloatsDeclared, BoolsDeclared);
                    MethodsUsed.Add("writeLine");
                    break;
                case "createFile":
                    IOFunctions.createFile(Tokens, i, ConvertedTokens, StringsDeclared);
                    MethodsUsed.Add("createFile");
                    break;
                case "writeFile":
                    IOFunctions.writeFile(Tokens, i, ConvertedTokens, StringsDeclared);
                    MethodsUsed.Add("writeFile");
                    break;
                case "const":
                    AddToken("const");
                    break;
                case "int":
                    AddToken("int ");
                    if (Tokens[i + 1] == " ")
                    {
                        AddToken(Tokens[i + 2]);
                        IntsDeclared.Add(Tokens[i + 2]);
                        Tokens.Remove(Tokens[i + 2]);
                        MethodsUsed.Add("int");
                    }
                    break;
                case "string":
                    AddToken("char ");
                    if (Tokens[i + 1] == " ")
                    {
                        AddToken(Tokens[i + 2] + "[255]");
                        if (Tokens[i + 3] == "[")
                        {
                            AddToken("[255");
                            Tokens.Remove(Tokens[i + 3]);
                        }
                        StringsDeclared.Add(Tokens[i + 2]);
                        Tokens.Remove(Tokens[i + 2]);
                        MethodsUsed.Add("string");
                    }
                    break;
                case "float":
                    AddToken("float ");
                    if (Tokens[i + 1] == " ")
                    {
                        AddToken(Tokens[i + 2]);
                        FloatsDeclared.Add(Tokens[i + 2]);
                        Tokens.Remove(Tokens[i + 2]);
                        MethodsUsed.Add("float");
                    }
                    break;
                case "bool":
                    AddToken("bool ");
                    if (Tokens[i + 1] == " ")
                    {
                        AddToken(Tokens[i + 2]);
                        BoolsDeclared.Add(Tokens[i + 2]);
                        Tokens.Remove(Tokens[i + 2]);
                    }

                    if (!ConvertedTokens.Contains("\n#include <stdbool.h>\n"))
                        ConvertedTokens.Insert(0, "\n#include <stdbool.h>\n");
                    break;
                case "if":
                    AddToken("if");
                    break;
                case "else":
                    AddToken("else");
                    break;
                case "while":
                    AddToken("while");
                    break;
                case "for":
                    AddToken("for");
                    break;
                case "switch":
                    AddToken("switch");
                    break;
                case "case":
                    AddToken("case");
                    break;
                case "break":
                    AddToken("break");
                    break;
                case "/":
                    if (Tokens[i + 1] == "/")
                    {
                        for (int k = i; k < Tokens.Count; k++)
                            if (Tokens[k] == "*" && Tokens[k + 1] == "/")
                                Tokens.Remove(Tokens[k]);
                            else
                                break;
                    }
                    else
                        AddToken("/");
                    break;
                case "return":
                    AddToken("return");
                    break;
                case "input":
                    IOFunctions.input(Tokens, i, ConvertedTokens, IntsDeclared, StringsDeclared, FloatsDeclared);
                    MethodsUsed.Add("input");
                    break;
                case "C":
                    if (Tokens[i + 1] == "{")
                    {
                        int tokensToRemove = 0;
                        for (int z = i + 2; z < Tokens.Count; z++)
                        {
                            if (Tokens[z] != "}" && Tokens[z + 1] != ";")
                            {
                                AddToken(Tokens[z]);
                                tokensToRemove += 1;
                            }
                            else if (Tokens[z] == "}" && Tokens[z + 1] == ";")
                            {
                                tokensToRemove += 2;
                                break;
                            }
                        }

                        for (int k = tokensToRemove; k > i; k--)
                            Tokens.Remove(Tokens[i + k]);
                    }
                    break;
                case "include":
                    string fileName = Tokens[i + 2];
                    fileName = fileName.Replace(";", "");
                    ArgReader.IncludedFileNames.Add(fileName);
                    Tokens.RemoveRange(i + 1, i + 3);
                    Tokenizer.IncludedFileTokenize(fileName);
                    break;
                default:
                    if (inQuotation == true)
                        AddToken(Tokens[i]);
                    else if (Tokens[i].All(char.IsDigit))
                        AddToken(Tokens[i]);
                    else if (IntsDeclared.Contains(Tokens[i]))
                        AddToken(Tokens[i]);
                    else if (FloatsDeclared.Contains(Tokens[i]))
                        AddToken(Tokens[i]);
                    else if (FunctionsDeclared.Contains(Tokens[i]))
                        AddToken(Tokens[i]);
                    else if (BoolsDeclared.Contains(Tokens[i]))
                        AddToken(Tokens[i]);
                    //write the string variable or reallocate the value
                    else if (StringsDeclared.Contains(Tokens[i]))
                        //too much string logic so it has been broken into a separate class
                        StringLogic.StringTokenLogic(Tokens, i, ConvertedTokens);
                    //check if the number is a proper float number i.e only one decimal point
                    else if (Tokens[i].Contains('.'))
                    {
                        string[] splitToken = Tokens[i].Split(".");
                        if (splitToken[0].All(char.IsDigit) && splitToken[1].All(char.IsDigit) && splitToken.Length == 2)
                            AddToken(Tokens[i]);
                        else
                            Errors.ReturnVariableError(100, "Floating point error (Contains more than one decimal point)");
                    }
                    else if (Tokens[i].All(char.IsDigit))
                        AddToken(Tokens[i]);
                    break;
            }
        }

        //Analysis.AnalyzeTokens(Tokens);
    }
}