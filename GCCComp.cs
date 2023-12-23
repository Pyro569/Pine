using System.Diagnostics;

class GCCComp
{
    public static void Compile(string BinaryName)
    {
        //check the operating system and adjust the commands ran based off that
        string OperatingSystem = System.Runtime.InteropServices.RuntimeInformation.OSDescription;

        System.Threading.Thread.Sleep(1500);
        Process.Start("gcc", "Main.cpp -w -lstdc++");

        try
        {
            //run the appropriate command based off the operating system that is being ran
            if (OperatingSystem.Contains("Linux"))
            {
                Process.Start("mv", " a.out " + BinaryName);
                //Process.Start("rm", "Main.cpp");
            }
            else if (OperatingSystem.Contains("Windows"))
                Process.Start("ren a.out " + BinaryName);
        }
        catch
        {
            Errors.ReturnIOError(500, "Cannot compile binary");
        }
    }
}