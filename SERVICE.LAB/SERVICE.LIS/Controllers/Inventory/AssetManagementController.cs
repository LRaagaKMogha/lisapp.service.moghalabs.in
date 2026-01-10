using Dev.IRepository;
using Dev.IRepository.Inventory;
using DEV.Common;
using DEV.Model;
using DEV.Model.Inventory.Master;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DEV.API.SERVICE.Controllers.Inventory
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class AssetManagementController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IAssetManagementRepository  _AssetManagementRepository;
        private readonly IMasterRepository _IMasterRepository;

        public AssetManagementController(IAssetManagementRepository AssetMangementRepository,IConfiguration config,IMasterRepository IMasterRepository)
        {
            _AssetManagementRepository = AssetMangementRepository;
            _config = config;
            _IMasterRepository = IMasterRepository;
        }

        [CustomAuthorize("INVMASTERS")]
        [HttpPost]
        [Route("api/Instrument/InsertInstrumentDetail")]

        public int InsertInstrumentDetails(postAssetManagementDTO objManufacturer)
        {
            int result = 0;
            try
            {
                result = _AssetManagementRepository.InsertInstrumentDetails(objManufacturer);
                string _CacheKey = CacheKeys.CommonMaster + "INSTRUMENT" + objManufacturer.venueNo + objManufacturer.venueBranchno;
                MemoryCacheRepository.RemoveItem(_CacheKey);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "InsertInstrumentDetails", ExceptionPriority.Low, ApplicationType.APPSERVICE, objManufacturer.venueNo, objManufacturer.userNo, 0);
            }
            return result;
        }

        [CustomAuthorize("INVMASTERS")]
        [HttpPost]
        [Route("api/Instrument/GetInstrumentDetail")]
        public List<GetAssetManagementResponse> GetInstrumentDetail(AssetManagementRequest masterRequest)
        {
            List<GetAssetManagementResponse> objResult = new List<GetAssetManagementResponse>();
            try
            {
                objResult = _AssetManagementRepository.GetInstrumentDetail(masterRequest);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GetInstrumentDetail", ExceptionPriority.Low, ApplicationType.APPSERVICE, masterRequest.venueNo, 0, 0);
            }
            return objResult;
        }

        [CustomAuthorize("INVMASTERS")]
        [HttpPost]
        [Route("api/AssetManagement/UploadInstrumentFilesss")]
        public Uploadpdffres Uploadfile(Uploadpdffreq req)
        {
            Uploadpdffres result = new Uploadpdffres();
            try
            {
                int venueno = (int)req.venueno;
                int venuebranchno = (int)req.venuebranchno;
                string instrumentname = req.instrumentname;
                int instrumentno = (int)req.instrumentno;
                string Actualfilename = req.Actualfilename;
                string base64data = req.Actualbinarydata;
                string format = req.filetype;

                string foldername = $"{venueno}\\{venuebranchno}\\{instrumentname}";
                var objAppSettingResponse = _IMasterRepository.GetSingleAppSetting("UploadPathInit");
                string uploadpath = objAppSettingResponse?.ConfigValue ?? "";

                string newPath = Path.Combine(uploadpath, foldername);
                if (!Directory.Exists(newPath)) Directory.CreateDirectory(newPath);

                if (!string.IsNullOrEmpty(base64data))
                {
                    string fileName = $"{venueno}_{venuebranchno}_{instrumentname}.{format}";
                    string fullPath = Path.Combine(newPath, fileName);
                    byte[] fileBytes = Convert.FromBase64String(base64data);
                    System.IO.File.WriteAllBytes(fullPath, fileBytes);
                }

                result.status = 1;
                result.message = "Successfully Uploaded";
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "UploadInstrumentFile", ExceptionPriority.Low, ApplicationType.APPSERVICE, 0, 0, 0);
            }
            return result;
        }

        [CustomAuthorize("INVMASTERS")]
        [HttpGet]
        [Route("api/AssetManagement/GetInstrumentPDF")]
        public IActionResult GetInstrumentPDF(int venueNo, int venueBranchNo, string instrumentName)
        {
            var objAppSettingResponse = _IMasterRepository.GetSingleAppSetting("UploadPathInit");
            string basePath = objAppSettingResponse?.ConfigValue ?? "";
            string folderPath = Path.Combine(basePath, $"{venueNo}\\{venueBranchNo}\\{instrumentName}");
            if (!Directory.Exists(folderPath))
            {
                return NotFound($"Folder not found: {folderPath}");
            }

            string filePath = Directory.GetFiles(folderPath, "*.pdf").FirstOrDefault();

            if (filePath != null && System.IO.File.Exists(filePath))
            {
                var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                return File(stream, "application/pdf", Path.GetFileName(filePath)); 
            }

            return NotFound("File not found");
        }
    }
}