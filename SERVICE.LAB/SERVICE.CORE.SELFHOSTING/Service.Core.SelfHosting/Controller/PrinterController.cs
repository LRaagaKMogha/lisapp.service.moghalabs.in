using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Template;
using Service.Common.Core;
using AppConfig = System.Configuration.ConfigurationManager;

namespace Service.Core.SelfHosting
{

    [ApiController]
    [Route("api/[controller]")]
    public class DeviceController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly Service.Common.Core.Logger _logger;

        public DeviceController(IConfiguration config)
        {
            _logger = new Logger(null);
            _config = config;
        }

        [HttpGet("test")]
        public string Test()
        {
            _logger.LogWrite("Connected");
            return "Connected";
        }

        [HttpPost("PrintBarcode")]
        public string PrintBarcode(List<Dictionary<string, string>> barcodeItem)
        {
            return ProcessPrint(barcodeItem, "SampleBarcode", "#BarcodeNo#");
        }

        [HttpPost("PrintSampleAccessionBarcode")]
        public string PrintSampleAccessionBarcode(List<Dictionary<string, string>> barcodeItem)
        {
            return ProcessPrint(barcodeItem, "SampleAccessionBarcode", "#BarcodeNo#");
        }

        [HttpPost("PrintPatientBarcode")]
        public string PrintPatientBarcode(List<Dictionary<string, string>> barcodeItem)
        {
            return ProcessPrint(barcodeItem, "PatientBarcode", "#BarcodeNo#");
        }

        [HttpPost("PrintRCHNoBarcode")]
        public string PrintRCHNoBarcode(List<Dictionary<string, string>> barcodeItem)
        {
            return ProcessPrint(barcodeItem, "RCRHBarcode", "#BarcodeNo#");
        }

        [HttpPost("PrintSlideBarcode")]
        public string PrintSlideBarcode(List<Dictionary<string, string>> barcodeItem)
        {
            return ProcessPrint(barcodeItem, "SlideBarcode", "#BarcodeNo#");
        }

        [HttpPost("PrintSpecimenBarcode")]
        public string PrintSpecimenBarcode(List<Dictionary<string, string>> barcodeItem)
        {
            return ProcessPrint(barcodeItem, "SpecimenBarcode", "#BarcodeNo#");
        }

        [HttpPost("PrintHistoSlideBarcode")]
        public string PrintHistoSlideBarcode(List<Dictionary<string, string>> barcodeItem)
        {
            return ProcessPrint(barcodeItem, "HistoSlideBarcode", "#BarcodeNo#");
        }

        [HttpPost("PrintCytoSlideBarcode")]
        public string PrintCytoSlideBarcode(List<Dictionary<string, string>> barcodeItem)
        {
            return ProcessPrint(barcodeItem, "CytoSlideBarcode", "#BarcodeNo#");
        }

        [HttpPost("PrintPapSlideBarcode")]
        public string PrintPapSlideBarcode(List<Dictionary<string, string>> barcodeItem)
        {
            return ProcessPrint(barcodeItem, "PapSlideBarcode", "#BarcodeNo#");
        }

        // ================= CORE METHOD =================
        private string ProcessPrint(List<Dictionary<string, string>> barcodeItem, string prnKey, string barcodeKey)
        {
            string result = string.Empty;

            try
            {
                if (barcodeItem == null || barcodeItem.Count == 0)
                    return "No data";

                _logger.LogWrite($"Start - {DateTime.Now}");

                var section = _config.GetSection(prnKey);

                string prnFile = section["PrnFile"];
                string baseExportFile = section["ExportPRNFile"];
                string printerName = section["PrinterName"];

                if (string.IsNullOrWhiteSpace(prnFile) ||
                    string.IsNullOrWhiteSpace(baseExportFile) ||
                    string.IsNullOrWhiteSpace(printerName))
                    return "Configuration missing";

                if (!System.IO.File.Exists(prnFile))
                    return $"PRN template not found: {prnFile}";

                // Ensure export directory exists
                var exportDirectory = Path.GetDirectoryName(baseExportFile);
                if (string.IsNullOrWhiteSpace(exportDirectory))
                    return "Invalid export path";

                Directory.CreateDirectory(exportDirectory);

                foreach (var itemSet in barcodeItem)
                {
                    string content = System.IO.File.ReadAllText(prnFile);
                    string barcodeNo = string.Empty;

                    // Always start fresh per loop
                    string currentExportFile = baseExportFile;

                    foreach (var item in itemSet)
                    {
                        if (item.Key == "#PRNType#")
                        {
                            // Insert value before extension
                            var fileNameWithoutExt = Path.GetFileNameWithoutExtension(baseExportFile);
                            var extension = Path.GetExtension(baseExportFile);

                            var newFileName = $"{fileNameWithoutExt}_{item.Value}{extension}";
                            currentExportFile = Path.Combine(exportDirectory, newFileName);
                        }

                        content = content.Replace(item.Key, item.Value);

                        if (item.Key == barcodeKey)
                            barcodeNo = item.Value;
                    }

                    // If no PRNType, use original path
                    if (currentExportFile == baseExportFile)
                    {
                        currentExportFile = baseExportFile;
                    }

                    if (!System.IO.File.Exists(currentExportFile))
                    {
                        _logger.LogWrite($"FILE NOT FOUND BEFORE PRINT: {currentExportFile}");
                        return "File missing before print";
                    }

                    System.IO.File.WriteAllText(currentExportFile, content);

                    bool printStatus = RawPrinterHelper.SendFileToPrinter(printerName, currentExportFile, barcodeNo);

                    if (!printStatus)
                    {
                        _logger.LogWrite($"PRINT FAILED - {barcodeNo} | File: {currentExportFile}");
                    }
                    else
                    {
                        _logger.LogWrite($"Printed - {barcodeNo} | File: {currentExportFile}");
                    }
                }

                result = "Printed Successfully";
                _logger.LogWrite(result);
                _logger.LogWrite($"End - {DateTime.Now}");
            }
            catch (Exception ex)
            {
                result = ex.Message;
                _logger.LogWrite("Error - " + ex.ToString());
            }

            return result;
        }
    }
}