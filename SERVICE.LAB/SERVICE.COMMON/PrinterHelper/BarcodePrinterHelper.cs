using System;
using System.IO;
using System.Runtime.InteropServices;
namespace DEV.Common
{
    public class RawPrinterHelper
    {
        // Structure and API declarions:
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public class Docinfoa
        {
            [MarshalAs(UnmanagedType.LPStr)]
            public string? pDocName;
            [MarshalAs(UnmanagedType.LPStr)]
            public string? pOutputFile;
            [MarshalAs(UnmanagedType.LPStr)]
            public string? pDataType;
        }
        [DllImport("winspool.Drv", EntryPoint = "OpenPrinterA", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        private static extern bool OpenPrinter([MarshalAs(UnmanagedType.LPStr)] string szPrinter, out IntPtr hPrinter, IntPtr pd);

        [DllImport("winspool.Drv", EntryPoint = "ClosePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        private static extern bool ClosePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "StartDocPrinterA", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        private static extern bool StartDocPrinter(IntPtr hPrinter, Int32 level, [In, MarshalAs(UnmanagedType.LPStruct)] Docinfoa di);

        [DllImport("winspool.Drv", EntryPoint = "EndDocPrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        private static extern bool EndDocPrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "StartPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        private static extern bool StartPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "EndPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        private static extern bool EndPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "WritePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        private static extern bool WritePrinter(IntPtr hPrinter, IntPtr pBytes, Int32 dwCount, out Int32 dwWritten);

        // SendBytesToPrinter()
        // When the function is given a printer name and an unmanaged array
        // of bytes, the function sends those bytes to the print queue.
        // Returns true on success, false on failure.
        public static bool SendBytesToPrinter(string szPrinterName, IntPtr pBytes, Int32 dwCount, string PrinterLable)
        {
            Int32 dwError = 0, dwWritten = 0;
            IntPtr hPrinter = new IntPtr(0);
            Docinfoa di = new Docinfoa();
            bool bSuccess = false; // Assume failure unless you specifically succeed.

            try
            {
                di.pDocName = PrinterLable;
                di.pDataType = "RAW";

                // Open the printer.
                if (OpenPrinter(szPrinterName.Normalize(), out hPrinter, IntPtr.Zero))
                {
                    // Start a document.
                    if (StartDocPrinter(hPrinter, 1, di))
                    {
                        // Start a page.
                        if (StartPagePrinter(hPrinter))
                        {
                            // Write your bytes.
                            bSuccess = WritePrinter(hPrinter, pBytes, dwCount, out dwWritten);
                            EndPagePrinter(hPrinter);
                        }
                        EndDocPrinter(hPrinter);
                    }
                    ClosePrinter(hPrinter);
                }
                // If you did not succeed, GetLastError may give more information
                // about why not.
                if (!bSuccess)
                {
                    dwError = Marshal.GetLastWin32Error();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "RawPrinterHelper.SendBytesToPrinter", ExceptionPriority.Medium, ApplicationType.APPSERVICE, 0, 0, 0);
            }
            return bSuccess;
        }

        public static bool SendFileToPrinter(string szPrinterName, string szFileName, string PrinterLable)
        {
            FileStream fs;
            BinaryReader br;
            bool bSuccess = false;
            // Open the file.
            using (FileStream stream = new FileStream(szFileName, FileMode.Open))
            {
                // Use stream
                try
                {
                    fs = stream;
                    br = new BinaryReader(fs);
                    Byte[] bytes = new Byte[fs.Length];
                    bSuccess = false;
                    // Your unmanaged pointer.
                    int nLength;

                    nLength = Convert.ToInt32(fs.Length);
                    // Read the contents of the file into the array.
                    bytes = br.ReadBytes(nLength);
                    // Allocate some unmanaged memory for those bytes.
                    IntPtr pUnmanagedBytes = Marshal.AllocCoTaskMem(nLength);
                    // Copy the managed byte array into the unmanaged array.
                    Marshal.Copy(bytes, 0, pUnmanagedBytes, nLength);
                    // Send the unmanaged bytes to the printer.
                    bSuccess = SendBytesToPrinter(szPrinterName, pUnmanagedBytes, nLength, PrinterLable);
                    // Free the unmanaged memory that you allocated earlier.
                    Marshal.FreeCoTaskMem(pUnmanagedBytes);
                }
                catch (Exception ex)
                {
                    MyDevException.Error(ex, "RawPrinterHelper.SendFileToPrinter", ExceptionPriority.Medium, ApplicationType.APPSERVICE, 0, 0, 0);
                }
            }
            return bSuccess;
        }

    }
}
