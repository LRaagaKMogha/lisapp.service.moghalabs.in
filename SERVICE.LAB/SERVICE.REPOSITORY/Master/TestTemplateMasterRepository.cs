using Dev.IRepository.Master;
using DEV.Common;
using Service.Model;
using Service.Model.EF;
using Service.Model.EF.DocumentUpload;
using Service.Model.Master;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Service.Model.DocumentUploadDTO;
namespace Dev.Repository.Master
{
    public class TestTemplateMasterRepository : ITestTemplateMasterRepository
    {
        private IConfiguration _config;
        public TestTemplateMasterRepository(IConfiguration config) { _config = config; }
        public List<GetTestTemplateMasterRes> GetTestTemplateMasterList(GetTestTemplateMasterReq req)
        {
            List<GetTestTemplateMasterRes> objResult = new List<GetTestTemplateMasterRes>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _DeptNo = new SqlParameter("DeptNo", req.DeptNo);
                    var _VenueNo = new SqlParameter("VenueNo", req.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", req.VenueBranchNo);
                    var _TestNo = new SqlParameter("TestNo", req.TestNo);
                    var _PageIndex = new SqlParameter("PageIndex", req.PageIndex);
                    var _PageSize = new SqlParameter("PageSize", req.PageSize);

                    objResult = context.GetTestTemplateMasterList
                        .FromSqlRaw(
                            "EXEC dbo.pro_GetTestTemplateMaster @DeptNo, @VenueNo, @VenueBranchNo, @TestNo, @PageIndex, @PageSize",
                            _DeptNo, _VenueNo, _VenueBranchNo, _TestNo, _PageIndex, _PageSize
                        ).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TemplateTestRepository.GetTestTemplateMasterList " + req.TestNo, ExceptionPriority.Low, ApplicationType.REPOSITORY, req.VenueNo, req.VenueBranchNo, 0);
            }
            return objResult;
        }
        public GetEditTemplateTestMasterResponseDto GetEditTemplateTestMaster(GetEditTemplateTestMasterRequestDto req)
        {
            var obj = new GetEditTemplateTestMasterResponseDto();

            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _TestNo = new SqlParameter("TestNo", req.TestNo);
                    var _VenueNo = new SqlParameter("VenueNo", req.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", req.VenueBranchNo);
                    var _IsApproval = new SqlParameter("IsApproval", req.IsApproval);
                    var _TemplateApprovalNo = new SqlParameter("TemplateApprovalNo", (object)req.TemplateApprovalNo ?? DBNull.Value);
                    var lst = context.Set<GetEditTemplateTestMasterRawDto>()
                        .FromSqlRaw(
                            "EXEC dbo.pro_GetEditTemplateTestMaster @TestNo, @VenueNo, @VenueBranchNo, @IsApproval,@TemplateApprovalNo",
                            _TestNo, _VenueNo, _VenueBranchNo, _IsApproval, _TemplateApprovalNo
                        )
                        .AsEnumerable()
                        .FirstOrDefault();
                    if (lst != null && !string.IsNullOrWhiteSpace(lst.testTemplate))
                    {
                        obj.TestTemplate = JsonConvert.DeserializeObject<List<TemplateItemDto>>(lst.testTemplate);
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex,$"TemplateTestRepository.GetEditTemplateTestMaster - Test No. : {req.TestNo}",ExceptionPriority.Low,
                 ApplicationType.REPOSITORY,req.VenueNo,req.VenueBranchNo,0);
            }
            return obj;
        }
        //public TemplatePathRes InsertTemplatePath(TemplatePathReq req)
        //{
        //    TemplatePathRes result = new TemplatePathRes();
        //    int templateNo = req.templateNo;
        //    int templateApprovalNo = req.templateApprovalNo ?? 0;
        //    bool isApproval = req.isApproval;
        //    bool IsTestTemplateApproveMode=req.IsTestTemplateApproveMode;
        //    string templateName = req.templateName;
        //    string contentBody = "";

        //    try
        //    {
        //        MasterRepository _IMasterRepository = new MasterRepository(_config);
        //        AppSettingResponse objAppSettingResponse = _IMasterRepository.GetSingleAppSetting("TemplateMasterFilePath");

        //        string basePath = objAppSettingResponse != null &&
        //                          !string.IsNullOrEmpty(objAppSettingResponse.ConfigValue)
        //                          ? objAppSettingResponse.ConfigValue
        //                          : "";
        //        //string filePath = Path.Combine(basePath, req.VenueNo.ToString(), req.testNo.ToString(), templateNo + ".rtf");
        //        string fileName = isApproval ? templateApprovalNo + ".rtf" : templateNo + ".rtf";
        //        string filePath = Path.Combine(basePath, req.VenueNo.ToString(), req.testNo.ToString(), fileName);


        //        if (!File.Exists(filePath))
        //        {
        //            result.status = 0;
        //            result.message = "Template file not found";
        //            return result;
        //        }
        //        contentBody = File.ReadAllText(filePath);
        //        try
        //        {
        //            if (templateNo > 0 || templateApprovalNo > 0)
        //            {
        //                using (var context = new DocumentContext(_config.GetConnectionString(ConfigKeys.DocumentDBConnection)))
        //                {
        //                    if (isApproval == false)
        //                    {
        //                        var existingTemplate = context.TemplateContent_Test
        //                            .FirstOrDefault(x => x.TemplateNo == templateNo && x.Status == true);

        //                        if (existingTemplate != null)
        //                        {
        //                            existingTemplate.TemplateName = templateName;
        //                            existingTemplate.ContentBody = contentBody;
        //                            existingTemplate.VenueNo = (short)req.VenueNo;

        //                            context.SaveChanges();
        //                        }
        //                    }
        //                    else if (isApproval == true)
        //                    {
        //                        var existingTemplateApproval = context.TemplateContentApproval_Test
        //                            .FirstOrDefault(x => x.TemplateApprovalNo == templateApprovalNo && x.Status == true);

        //                        if (existingTemplateApproval != null)
        //                        {
        //                            existingTemplateApproval.TemplateApprovalNo = templateApprovalNo > 0 ? templateApprovalNo : 0;
        //                            existingTemplateApproval.TemplateNo = templateNo;
        //                            existingTemplateApproval.TemplateName = templateName;
        //                            existingTemplateApproval.ContentBody = contentBody;
        //                            existingTemplateApproval.VenueNo = (short)req.VenueNo;

        //                            context.SaveChanges();
        //                        }
        //                    }
        //                }
        //            }
        //            if (File.Exists(filePath))
        //            {
        //                File.Delete(filePath);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            MyDevException.Error(ex, "TestRepository.UpdateTemplateText - Test No. : " + req.testNo.ToString(),
        //                ExceptionPriority.Medium, ApplicationType.REPOSITORY, req.VenueNo, req.VenueBranchNo, req.userNo);
        //        }

        //        result.status = 1;
        //        result.message = "Template inserted successfully";
        //    }
        //    catch (Exception ex)
        //    {
        //        MyDevException.Error(ex, "TemplatePathRepository.InsertTemplatePath",
        //            ExceptionPriority.Low, ApplicationType.REPOSITORY,
        //            req.VenueNo, req.VenueBranchNo, req.testNo);

        //        result.status = -1;
        //        result.message = ex.Message;
        //    }

        //    return result;
        //}
        public TemplatePathRes InsertTemplatePath(TemplatePathReq req)
        {
            TemplatePathRes result = new TemplatePathRes();
            int templateNo = req.templateNo;
            int templateApprovalNo = req.templateApprovalNo ?? 0;
            bool isApproval = req.isApproval;
            bool IsTestTemplateApproveMode = req.IsTestTemplateApproveMode;
            string templateName = req.templateName;
            string contentBody = "";

            try
            {
                MasterRepository _IMasterRepository = new MasterRepository(_config);
                AppSettingResponse objAppSettingResponse = _IMasterRepository.GetSingleAppSetting("TemplateMasterFilePath");

                string basePath = objAppSettingResponse != null &&
                                  !string.IsNullOrEmpty(objAppSettingResponse.ConfigValue)
                                  ? objAppSettingResponse.ConfigValue
                                  : "";

                string fileName = "";
                if (IsTestTemplateApproveMode && isApproval)
                {
                    fileName = templateNo + ".rtf";
                }
                else if (!IsTestTemplateApproveMode && isApproval)
                {
                    fileName = templateApprovalNo + ".rtf";
                }
                else if (!IsTestTemplateApproveMode && !isApproval)
                {
                    fileName = templateNo + ".rtf";
                }
                string filePath = Path.Combine(basePath, req.VenueNo.ToString(), req.testNo.ToString(), fileName);
                if (!File.Exists(filePath))
                {
                    result.status = 0;
                    result.message = "Template file not found";
                    return result;
                }
                contentBody = File.ReadAllText(filePath);
                try
                {
                    if (templateNo > 0 || templateApprovalNo > 0)
                    {
                        using (var context = new DocumentContext(_config.GetConnectionString(ConfigKeys.DocumentDBConnection)))
                        {
                            if ((IsTestTemplateApproveMode && isApproval) || (!IsTestTemplateApproveMode && !isApproval))
                            {
                                var existingTemplate = context.TemplateContent_Test
                                    .FirstOrDefault(x => x.TemplateNo == templateNo && x.Status == true);

                                if (existingTemplate != null)
                                {
                                    existingTemplate.TemplateName = templateName;
                                    existingTemplate.ContentBody = contentBody;
                                    existingTemplate.VenueNo = (short)req.VenueNo;

                                    context.SaveChanges();
                                }
                            }
                            else if (!IsTestTemplateApproveMode && isApproval)
                            {
                                var existingTemplateApproval = context.TemplateContentApproval_Test
                                    .FirstOrDefault(x => x.TemplateApprovalNo == templateApprovalNo && x.Status == true);

                                if (existingTemplateApproval != null)
                                {
                                    existingTemplateApproval.TemplateApprovalNo = templateApprovalNo > 0 ? templateApprovalNo : 0;
                                    existingTemplateApproval.TemplateNo = templateNo;
                                    existingTemplateApproval.TemplateName = templateName;
                                    existingTemplateApproval.ContentBody = contentBody;
                                    existingTemplateApproval.VenueNo = (short)req.VenueNo;

                                    context.SaveChanges();
                                }
                            }
                        }
                    }

                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                }
                catch (Exception ex)
                {
                    MyDevException.Error(ex, "TestRepository.UpdateTemplateText - Test No. : " + req.testNo.ToString(),
                        ExceptionPriority.Medium, ApplicationType.REPOSITORY, req.VenueNo, req.VenueBranchNo, req.userNo);
                }

                result.status = 1;
                result.message = "Template inserted successfully";
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TemplatePathRepository.InsertTemplatePath",
                    ExceptionPriority.Low, ApplicationType.REPOSITORY,
                    req.VenueNo, req.VenueBranchNo, req.testNo);

                result.status = -1;
                result.message = ex.Message;
            }

            return result;
        }

        public InsertTestTemplateMasterRes InsertTestTemplateMaster(InsertTestTemplateMasterReq req)
        {
            InsertTestTemplateMasterRes obj = new InsertTestTemplateMasterRes();
            int templateNo = 0;
            int templateApprovalNo = 0;
            string templateName = "";
            string contentBody = "";
            bool isApproval = false;
            bool? isApprovalTestTemplate = false;

            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _testno = new SqlParameter("testno", req.testNo);
                    var _templateNo = new SqlParameter("templateNo", req.templateNo);
                    var _templateName = new SqlParameter("templateName", req.templateName);
                    var _isDefault = new SqlParameter("isDefault", req.isDefault);
                    var _sequenceNo = new SqlParameter("sequenceNo", req.sequenceNo);
                    var _status = new SqlParameter("status", req.status);
                    var _userno = new SqlParameter("userno", req.userNo);
                    var _venueno = new SqlParameter("venueno", req.venueNo);
                    var _venuebranchno = new SqlParameter("venuebranchno", req.venueBranchNo);
                    var _OldServiceNo = new SqlParameter("OldServiceNo", req.OldServiceNo);
                    var _OldTemplateNo = new SqlParameter("OldTemplateNo", req.OldTemplateNo);
                    var _IsApproval = new SqlParameter("IsApproval", req.IsApproval);
                    var _IsReject = new SqlParameter("IsReject", req.IsReject);
                    var _IsApprovalTestTemplate = new SqlParameter("IsApprovalTestTemplate", req.IsApprovalTestTemplate);

                    var lst = context.InsertTestTemplateMaster.FromSqlRaw(
                    "Execute dbo.pro_InsertTestTemplateText @testno, @templateNo, @templateName, @isDefault, @sequenceNo," +
                    "@status, @userno, @venueno, @venuebranchno, @OldServiceNo, @OldTemplateNo, @IsApproval, @IsReject",
                    _testno, _templateNo, _templateName, _isDefault, _sequenceNo, _status, _userno, _venueno, _venuebranchno,
                    _OldServiceNo, _OldTemplateNo, _IsApproval, _IsReject).ToList();

                    obj.templateNo = lst[0].templateNo;
                    obj.templateApprovalNo = lst[0].templateApprovalNo;

                    templateNo = lst[0].templateNo;
                    templateApprovalNo = lst[0].templateApprovalNo;
                    templateName = req.templateName;
                    contentBody = req.templateText;
                    isApproval = req.IsApproval;
                    isApprovalTestTemplate = req.IsApprovalTestTemplate;
                }
                try
                {
                    if (templateNo > 0 || templateApprovalNo > 0)
                    {
                        using (var context = new DocumentContext(_config.GetConnectionString(ConfigKeys.DocumentDBConnection)))
                        {
                            if(isApproval==false)
                            {
                                if (isApprovalTestTemplate == false)
                                {
                                    var existingRecords = context.TemplateContent_Test
                                                                .Where(t => t.TemplateNo == templateNo && t.Status == true)
                                                                .ToList();

                                    foreach (var rec in existingRecords)
                                    {
                                        rec.Status = false;
                                    }

                                    TestTemplateRequest templateRequest = new TestTemplateRequest
                                    {
                                        TemplateNo = templateNo,
                                        TemplateName = templateName,
                                        ContentBody = contentBody,
                                        VenueNo = (short)req.venueNo,
                                        Status = true,
                                        CreatedOn = DateTime.UtcNow,
                                        CreatedBy = (short)req.userNo
                                    };

                                    context.TemplateContent_Test.Add(templateRequest);
                                    context.SaveChanges();
                                }
                                else if (isApprovalTestTemplate == true)
                                {
                                    var existingApprovalRecords = context.TemplateContentApproval_Test
                                                                         .Where(t => t.TemplateApprovalNo == templateApprovalNo && t.Status == true)
                                                                         .ToList();

                                    foreach (var rec in existingApprovalRecords)
                                    {
                                        rec.Status = false;
                                    }
                                    TestTemplateApprovalRequest templateApprovalRequest = new TestTemplateApprovalRequest
                                    {
                                        TemplateApprovalNo = templateApprovalNo,
                                        TemplateNo = templateNo,
                                        TemplateName = templateName,
                                        ContentBody = contentBody,
                                        VenueNo = (short)req.venueNo,
                                        Status = true,
                                        CreatedOn = DateTime.UtcNow,
                                        CreatedBy = (short)req.userNo
                                    };

                                    context.TemplateContentApproval_Test.Add(templateApprovalRequest);
                                    context.SaveChanges();
                                }
                            }
                            else if(isApproval==true)
                            {
                                var existingApprovalRecords = context.TemplateContentApproval_Test
                                    .Where(t => t.TemplateApprovalNo == templateApprovalNo && t.Status == true)
                                    .ToList();

                                if (existingApprovalRecords.Any())
                                {
                                    var templateNo1 = existingApprovalRecords.First().TemplateNo;
                                    var existingRecords = context.TemplateContent_Test
                                        .Where(t => t.TemplateNo == templateNo1 && t.Status == true)
                                        .ToList();

                                    foreach (var rec in existingRecords)
                                    {
                                        rec.Status = false;
                                    }
                                    var templateRequest = new TestTemplateRequest
                                    {
                                        TemplateNo = templateNo,
                                        TemplateName = templateName,
                                        ContentBody = contentBody,
                                        VenueNo = (short)req.venueNo,
                                        Status = true,
                                        CreatedOn = DateTime.UtcNow,
                                        CreatedBy = (short)req.userNo
                                    };

                                    context.TemplateContent_Test.Add(templateRequest);
                                    context.SaveChanges();
                                    foreach (var rec in existingApprovalRecords)
                                    {
                                        rec.Status = false;
                                    }
                                    context.SaveChanges();
                                }

                            }

                        }
                    }
                }
                catch (Exception ex)
                {
                    MyDevException.Error(ex,
                        "TemplateTestRepository.InsertTestTemplateMaster - Test No. : " + req.testNo.ToString(),
                        ExceptionPriority.Medium,
                        ApplicationType.REPOSITORY,
                        req.venueNo, req.venueBranchNo, req.userNo);
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TemplateTestRepository.InsertTestTemplateMaster - Test No. : " + req.testNo.ToString(), ExceptionPriority.Medium, ApplicationType.REPOSITORY, req.venueNo, req.venueBranchNo, req.userNo);
            }
            return obj;
        }
        public GetTestTemplateTextMasterRes GetTextTemplateTextMaster(GetTestTemplateTextMasterReq req)
        {
            GetTestTemplateTextMasterRes obj = new GetTestTemplateTextMasterRes();
            try
            {
                // 1. Query database
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DocumentDBConnection)))
                {
                    var _testno = new SqlParameter("testno", req.testNo);
                    var _templateNo = new SqlParameter("templateNo", req.templateNo);
                    var _venueno = new SqlParameter("venueno", req.venueNo);
                    var _IsApproval = new SqlParameter("IsApproval", req.IsApproval);

                    var result = context.GetTextTemplateTextMaster
                        .FromSqlRaw("EXEC dbo.pro_GetTemplateMasterDetails @TestNo, @TemplateNo, @VenueNo, @IsApproval",
                                     _testno, _templateNo, _venueno, _IsApproval)
                        .ToList();

                    if (result != null && result.Count > 0)
                    {
                        obj.templateText = result[0].templateText ?? string.Empty;
                        obj.templateNo = result[0].templateNo;
                    }
                    else
                    {
                        obj.templateText = string.Empty;
                        obj.templateNo = req.templateNo;
                    }
                }

                //// 2. If templateText is empty, try reading from file
                //if (string.IsNullOrWhiteSpace(obj.templateText) && req.templateNo > 0)
                //{
                //    MasterRepository _repo = new MasterRepository(_config);
                //    var appSetting = _repo.GetSingleAppSetting("MasterFilePath");
                //    if (appSetting != null && !string.IsNullOrEmpty(appSetting.ConfigValue))
                //    {
                //        string path = Path.Combine(appSetting.ConfigValue, req.venueNo.ToString(), "Template", req.templateNo + ".ym");
                //        if (File.Exists(path))
                //        {
                //            obj.templateText = File.ReadAllText(path);
                //        }
                //    }
                //}

                // 3. Export RTF file if needed
                try
                {
                    if (req.templateNo > 0)
                    {
                        MasterRepository _repo = new MasterRepository(_config);
                        var appSetting = _repo.GetSingleAppSetting("TemplateMasterFilePath");
                        string basePath = appSetting?.ConfigValue ?? "";

                        if (!string.IsNullOrEmpty(basePath))
                        {
                            string dirPath = Path.Combine(basePath, req.venueNo.ToString(), req.testNo.ToString());
                            if (!Directory.Exists(dirPath))
                                Directory.CreateDirectory(dirPath);

                            string filePath = Path.Combine(dirPath, req.templateNo + ".rtf");
                            string contentBody = "";

                            using (var docContext = new DocumentContext(_config.GetConnectionString(ConfigKeys.DocumentDBConnection)))
                            {
                                if (!req.IsApproval)
                                {
                                    var template = docContext.TemplateContent_Test.FirstOrDefault(x => x.TemplateNo == req.templateNo && x.Status);
                                    contentBody = template?.ContentBody ?? "";
                                }
                                else
                                {
                                    var templateApproval = docContext.TemplateContentApproval_Test
                                        .FirstOrDefault(x => x.TemplateApprovalNo == req.templateNo && x.Status);
                                    contentBody = templateApproval?.ContentBody ?? "";
                                }
                            }

                            if (!string.IsNullOrEmpty(contentBody))
                            {
                                File.WriteAllText(filePath, contentBody);
                            }
                        }
                    }
                }
                catch (Exception innerEx)
                {
                    MyDevException.Error(innerEx, "TemplateTestRepository.ExportTemplatePath (inside GetTextTemplateTextMaster)",
                        ExceptionPriority.Low, ApplicationType.REPOSITORY,
                        req.venueNo, req.venueBranchNo, req.testNo);
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TemplateTestRepository.GetTextTemplateTextMaster - Test No. : ",
                    ExceptionPriority.Low, ApplicationType.REPOSITORY,
                    req.venueNo, req.venueBranchNo, req.testNo);
            }

            return obj;
        }
        public List<GetTemplateApprovalRes> GetTemplateApprovalList(GetTemplateApprovalReq req)
        {
            List<GetTemplateApprovalRes> objResult = new List<GetTemplateApprovalRes>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", req.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", req.VenueBranchNo);
                    var _DeptNo = new SqlParameter("DeptNo", req.DeptNo);
                    var _TestNo = new SqlParameter("TestNo", req.TestNo);
                    var _PageIndex = new SqlParameter("PageIndex", req.PageIndex);
                    var _PageSize = new SqlParameter("PageSize", req.PageSize);

                    objResult = context.GetTemplateApprovalList
                        .FromSqlRaw(
                            "EXEC dbo.pro_GetTemplateApproval @VenueNo, @VenueBranchNo, @DeptNo, @TestNo, @PageIndex, @PageSize",
                            _VenueNo, _VenueBranchNo, _DeptNo, _TestNo, _PageIndex, _PageSize
                        ).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TemplateTestRepository.GetTemplateApprovalList " + req.TestNo, ExceptionPriority.Low, ApplicationType.REPOSITORY, req.VenueNo, req.VenueBranchNo, 0);
            }
            return objResult;
        }

    }
}