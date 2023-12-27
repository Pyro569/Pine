using System.IO;

class FileChecker
{
    public static void CheckFile(string path)
    {
        if (File.Exists(path))
        {
            //split the file path up and check if it is the correct file extension
            string[] File = path.Split('.');

            if (File.Length >= 3)
                Errors.ReturnIOError(200, "File name contains '.'");
            else
                if (File[1] != "pine")
                Errors.ReturnIOError(300, "Incorrect File Extension (." + File[1] + ")");
        }
        else
            Errors.ReturnIOError(100, "File cannot be found");
    }
}