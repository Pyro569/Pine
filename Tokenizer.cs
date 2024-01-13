using Pine;

class Tokenizer
{
    private static List<string> FileContents = new List<string>();
    public static List<string> Tokens = new List<string>();
    private static void AddToken(string Token)
    {
        Tokens.Add(Token);
    }

    public static void Tokenize(string path)
    {
        Tokens.Clear();

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
                && FileContents[i][j] != ' ' && FileContents[i][j] != ',' && FileContents[i][j] != '\n' && FileContents[i][j] != ':')
                {
                    currentToken += FileContents[i][j];
                }
                else
                {
                    if (currentToken != "")
                        AddToken(currentToken);
                    if (FileContents[i][j].ToString() != "\n")
                        AddToken(FileContents[i][j].ToString());
                    currentToken = "";
                }
            }

            currentToken = "";
        }
        Converter.DataControlScan(Tokens);

        //start the conversion process of the tokens
        Converter.Convert(Tokens);
    }

    private static void ReadFile(string path)
    {
        FileContents.Clear();
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

    public static void IncludedFileTokenize(string path)
    {
        List<string> fileContents = new List<string>();
        List<string> newTokens = new List<string>();

        fileContents.Clear();
        //read the file into the file contents array
        try
        {
            using (StreamReader sr = new StreamReader(path))
            {
                fileContents.Add(sr.ReadToEnd());
            }
        }
        catch
        {
            Errors.ReturnIOError(400, "Cannot read file");
        }

        string currentToken = "";

        for (int i = 0; i < fileContents.Count; i++)
        {
            for (int j = 0; j < fileContents[i].Length; j++)
            {
                if (fileContents[i][j] != ';' && fileContents[i][j] != '"' && fileContents[i][j] != '(' && fileContents[i][j] != ')' && fileContents[i][j] != '{'
                && fileContents[i][j] != '}' && fileContents[i][j] != '=' && fileContents[i][j] != '<' && fileContents[i][j] != '>' && fileContents[i][j] != '+'
                && fileContents[i][j] != '-' && fileContents[i][j] != '*' && fileContents[i][j] != '/' && fileContents[i][j] != '#' && fileContents[i][j] != '!'
                && fileContents[i][j] != '%' && fileContents[i][j] != '&' && fileContents[i][j] != '|' && fileContents[i][j] != '[' && fileContents[i][j] != ']'
                && fileContents[i][j] != ' ' && fileContents[i][j] != ',' && fileContents[i][j] != '\n' && fileContents[i][j] != ':' && FileContents[i][j] != '\\'
                && FileContents[i][j] != '~')
                {
                    currentToken += fileContents[i][j];
                }
                else
                {
                    if (currentToken != "")
                        newTokens.Add(currentToken);
                    if (fileContents[i][j].ToString() != "\n")
                        newTokens.Add(fileContents[i][j].ToString());
                    currentToken = "";
                }
            }

            currentToken = "";
        }

        Converter.DataControlScan(newTokens);
    }
}