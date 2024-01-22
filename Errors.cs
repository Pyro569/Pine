using System.Diagnostics;
using System.Net.Mail;

class Errors
{
    public static List<int> ErrorCodesThrown = new List<int>();
    public static List<string> ErrorMessagesThrown = new List<string>();

    public static void ReturnIOError(int errorCode, string errorMessage)
    {
        Console.WriteLine(Convert.ToString(errorCode) + " IO: " + errorMessage);
        AddThrownErrorToList(errorCode, "IO " + errorMessage);
        LogErrors();
        Environment.Exit(errorCode);
    }

    public static void ReturnGenericError(int errorCode, string errorMessage)
    {
        Console.WriteLine(Convert.ToString(errorCode) + " GEN: " + errorMessage);
        AddThrownErrorToList(errorCode, "GEN " + errorMessage);
        LogErrors();
        Environment.Exit(errorCode);
    }

    public static void ReturnVariableError(int errorCode, string errorMessage)
    {
        Console.WriteLine(Convert.ToString(errorCode) + " VAR: " + errorMessage);
        AddThrownErrorToList(errorCode, "VAR " + errorMessage);
        LogErrors();
        Environment.Exit(errorCode);
    }

    public static void NonFatalLog(int errorCode, string logType, string logMessage)
    {
        AddThrownErrorToList(errorCode, logType + " " + logMessage);
        LogErrors();
    }

    public static void AddThrownErrorToList(int errorCode, string errorMessage)
    {
        ErrorCodesThrown.Add(errorCode);
        ErrorMessagesThrown.Add(errorMessage);
    }

    public static void LogErrors()
    {
        //create a file called ErrorsThrown.pidb in the location where the main compiler binary is and then write each and every errors to it in a specific format

        //format should look like this
        /*
            Date Time
            System Info
            MemUsed

            Code Type Message
        */

        //create and open the ErrorsThrown.pidb file then write the date/time and system info
        StreamWriter pidbFile = new StreamWriter("log.pidb");

        pidbFile.WriteLine(DateTime.Now.ToString("M/d/yyyy") + " " + DateTime.Now.ToString("h:mm:ss tt"));
        pidbFile.WriteLine(System.Runtime.InteropServices.RuntimeInformation.OSDescription);

        //get the memory used by the application in megabytes
        int memUsedInBytes = Convert.ToInt32(Process.GetCurrentProcess().PrivateMemorySize64);
        int memUsedInMegabytes = memUsedInBytes / 1000000;

        pidbFile.WriteLine(memUsedInMegabytes.ToString() + "Mb\n");

        //add each error and message to the log
        for (int i = 0; i < ErrorCodesThrown.Count; i++)
        {
            pidbFile.Write(ErrorCodesThrown[i].ToString());
            pidbFile.Write(" " + ErrorMessagesThrown[i]);
            pidbFile.Write("\n");
        }

        pidbFile.Close();
    }
}