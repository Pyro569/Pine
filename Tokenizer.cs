class Tokenizer
{
    private static List<string> FileContents = new List<string>();
    public static List<string> Tokens = new List<string>();

    public static void Tokenize(string path)
    {
        ReadFile(path);

        string currentToken = "";

        //loop through each character of each line in the file and check for special chars and convert to tokens
        for (int i = 0; i < FileContents.Count; i++)
        {
            for (int j = 0; j < FileContents[i].Length; j++)
            {
                if (FileContents[i][j] != ';' && FileContents[i][j] != '"' && FileContents[i][j] != '(' && FileContents[i][j] != ')' && FileContents[i][j] != '{'
                && FileContents[i][j] != '}' && FileContents[i][j] != '=' && FileContents[i][j] != '<' && FileContents[i][j] != '>' && FileContents[i][j] != '+'
                && FileContents[i][j] != '-' && FileContents[i][j] != '*' && FileContents[i][j] != '/' && FileContents[i][j] != '#' && FileContents[i][j] != '!'
                && FileContents[i][j] != '%' && FileContents[i][j] != '&' && FileContents[i][j] != '|' && FileContents[i][j] != '[' && FileContents[i][j] != ']'
                && FileContents[i][j] != ' ' && FileContents[i][j] != ',' && FileContents[i][j] != '\n')
                {
                    currentToken += FileContents[i][j];
                }
                else
                {
                    if (currentToken != "")
                        Tokens.Add(currentToken);
                    if (FileContents[i][j].ToString() != "\n")
                        Tokens.Add(FileContents[i][j].ToString());
                    currentToken = "";
                }
            }

            currentToken = "";
        }

        //start the conversion process of the tokens
        Converter.Convert(Tokens);
    }

    private static void ReadFile(string path)
    {
        //read the file into the file contents array
        try
        {
            using (StreamReader sr = new StreamReader(path))
            {
                FileContents.Add(sr.ReadToEnd());
            }
        }
        catch
        {
            Errors.ReturnIOError(400, "Cannot read file");
        }
    }
}