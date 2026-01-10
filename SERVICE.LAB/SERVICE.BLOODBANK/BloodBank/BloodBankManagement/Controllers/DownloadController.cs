using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using BloodBankManagement.Services.BloodBankRegistrations;
using BloodBankManagement.Services.Downloads;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Shared;

namespace BloodBankManagement.Controllers
{
    [CustomAuthorize("BloodBankMgmt")]

    public class DownloadController : ApiController
    {
        private readonly IDownloadService downloadService;

        public DownloadController(IDownloadService _downloadService)
        {
            this.downloadService = _downloadService;
        }

        [HttpGet]
        [Route("download/{startDate}/{endDate}")]
        public async Task<IActionResult> Download(DateTime startDate, DateTime endDate)
        {
            var response = await downloadService.GenerateTransfusedLinesForEDI(startDate, endDate);
            if (!response.IsError)
            {
                var itemsToFlush = response.Value;

                var zipStream = new MemoryStream();
                using (var archive = new ZipArchive(zipStream, ZipArchiveMode.Create, true))
                {
                    foreach (KeyValuePair<string, List<string>> entry in itemsToFlush)
                    {
                        var memoryStream = CreateMemoryStream(entry.Value);
                        var zipentry = archive.CreateEntry($"{entry.Key}.txt", CompressionLevel.Fastest);
                        using (var entryStream = zipentry.Open())
                        {
                            memoryStream.CopyTo(entryStream);
                        }
                    }
                }

                zipStream.Seek(0, SeekOrigin.Begin);
                zipStream.Position = 0;
                Response.Headers.Add("Content-Disposition", "attachment; filename=\"edi.zip\"");
                Response.Headers.Add("Content-Type", "application/zip");

                return File(zipStream, "application/zip");
            }
            return Problem(response.Errors);
        }

        private static MemoryStream CreateMemoryStream(List<string> itemsToFlush)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            itemsToFlush.ForEach(row =>
               {
                   writer.WriteLine(row);
               });
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
      
    }
}