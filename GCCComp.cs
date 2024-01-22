using System.Diagnostics;

class GCCComp
{
    public static bool DebugMode = false;

    public static void Compile(string BinaryName)
    {
        //check the operating system and adjust the commands ran based off that
        string OperatingSystem = System.Runtime.InteropServices.RuntimeInformation.OSDescription;

        Process.Start("gcc", "Main.c -w");
        while (!File.Exists("a.out"))
        {
            //do nothing at all until the a.out binary exists
        }


        try
        {
            //run the appropriate command based off the operating system that is being ran
            if (OperatingSystem.Contains("Linux"))
            {
                Process.Start("mv", " a.out " + BinaryName);
                if (!DebugMode)//if debug mode is not on remove the main.cpp file
                    Process.Start("rm", "Main.c");
                Process.Start("chmod", "+x " + BinaryName);
            }
            else if (OperatingSystem.Contains("Windows"))
            {
                Process.Start("ren a.out " + BinaryName);
                if (!DebugMode)
                    Process.Start("del", "Main.c");
            }

            Errors.NonFatalLog(0, "COMP", "Successfully compiled");
        }
        catch
        {
            Errors.ReturnIOError(500, "Cannot compile binary");
        }
    }
}