using System;
using System.Management;
using System.Security.Cryptography;
using System.Text;

namespace Service.API.Controllers
{
    public class HardwareHelper
    {
        public string GenerateHardwareId()
        {
            string cpu = GetWMI("Win32_Processor", "ProcessorId");
            string bios = GetWMI("Win32_BIOS", "SerialNumber");
            string board = GetWMI("Win32_BaseBoard", "SerialNumber");

            string raw = cpu + bios + board;

            using var sha = SHA256.Create();
            var hash = sha.ComputeHash(Encoding.UTF8.GetBytes(raw));

            return Convert.ToHexString(hash);
        }
        private string GetWMI(string className, string property)
        {
            using var searcher = new ManagementObjectSearcher($"SELECT {property} FROM {className}");
            foreach (ManagementObject obj in searcher.Get())
            {
                return obj[property]?.ToString()?.Trim() ?? "";
            }
            return "";
        }
    }
}
