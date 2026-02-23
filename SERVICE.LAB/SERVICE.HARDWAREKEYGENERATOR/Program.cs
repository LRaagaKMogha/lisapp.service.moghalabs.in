using System.Management;
using System.Security.Cryptography;
using System.Text;

public static class HardwareHelper
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Generating Hardware ID...");
            string hardwareId = HardwareHelper.GenerateHardwareId();
            Console.WriteLine();
            Console.WriteLine("Hardware ID:");
            Console.WriteLine(hardwareId);
            Console.WriteLine();
            Console.WriteLine("Press ENTER to exit...");
            Console.ReadLine();
        }
    }
    public static string GenerateHardwareId()
    {
        string cpu = GetWMI("Win32_Processor", "ProcessorId");
        string bios = GetWMI("Win32_BIOS", "SerialNumber");
        string board = GetWMI("Win32_BaseBoard", "SerialNumber");

        string raw = cpu + bios + board;

        using var sha = SHA256.Create();
        var hash = sha.ComputeHash(Encoding.UTF8.GetBytes(raw));

        return Convert.ToHexString(hash);
    }
    private static string GetWMI(string className, string property)
    {
        using var searcher = new ManagementObjectSearcher($"SELECT {property} FROM {className}");
        foreach (ManagementObject obj in searcher.Get())
        {
            return obj[property]?.ToString()?.Trim() ?? "";
        }
        return "";
    }
}