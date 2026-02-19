using Service.Common;
using Service.Model.EF;
using Service.Model.Sample;
using Service.Model;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Service.IRepository;

namespace Service.Repository.Samples
{
    public class SlidePrintingRepository : ISlidePrintingRepository
    {
        private IConfiguration _config;
        public SlidePrintingRepository(IConfiguration config) { _config = config; }

        /// <summary>
        /// Get Slide Printing Details
        /// </summary>
        /// <returns></returns>
        public List<GetSlidePrintingResponse> GetSlidePrintingDetails(SlidePrintingRequest RequestItem)
        {
            List<GetSlidePrintingResponse> lstSlidePrintingResponses = new List<GetSlidePrintingResponse>();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", RequestItem?.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", RequestItem?.VenueBranchNo);
                    var _FromDate = new SqlParameter("FROMDate", RequestItem?.FromDate);
                    var _ToDate = new SqlParameter("ToDate", RequestItem?.ToDate);
                    var _Type = new SqlParameter("Type", RequestItem?.Type);
                    var _PageIndex = new SqlParameter("PageIndex", RequestItem?.pageIndex);
                    var _UserNo = new SqlParameter("UserNo", RequestItem?.userNo);
                    var _FilterType = new SqlParameter("FilterType", RequestItem?.outSourceType);
                    var _VisitNo = new SqlParameter("VisitNo", RequestItem?.visitNo);
                    var _DepartmentType = new SqlParameter("DepartmentType", RequestItem?.departmentType.ValidateEmpty());
                    var _PatientNo = new SqlParameter("PatientNo", RequestItem?.PatientNo);

                    lstSlidePrintingResponses = context.GetSlidePrintingDTO.FromSqlRaw(
                    "Execute dbo.pro_GetSlidePrintDetails @VenueNo,@VenueBranchNo,@FROMDate,@ToDate,@Type,@PageIndex,@UserNo,@FilterType,@VisitNo,@DepartmentType,@PatientNo",
                    _VenueNo, _VenueBranchNo, _FromDate, _ToDate, _Type, _PageIndex, _UserNo, _FilterType, _VisitNo, _DepartmentType, _PatientNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GetSlidePrintingDetails", ExceptionPriority.High, ApplicationType.REPOSITORY, RequestItem?.VenueNo, RequestItem?.VenueBranchNo, RequestItem?.userNo);
            }
            return lstSlidePrintingResponses;
        }

        /// <summary>
        /// Get Slide Printing Details
        /// </summary>
        /// <returns></returns>
        public SlidePrintPatientdetailsResponse GetSlidePrintingPatientdetails(CommonFilterRequestDTO RequestItem)
        {
            List<GetSlidePrintPatientdetailsResponse> lstSlidePrintingPatientResponses = new List<GetSlidePrintPatientdetailsResponse>();
            SlidePrintPatientdetailsResponse slidePrintPatientdetailsResponse = new SlidePrintPatientdetailsResponse();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {

                    var _VenueNo = new SqlParameter("VenueNo", RequestItem?.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", RequestItem?.VenueBranchNo);
                    var _VisitNo = new SqlParameter("VisitNo", RequestItem?.visitNo);
                    var _ServiceNo = new SqlParameter("ServiceNo", RequestItem?.serviceNo);

                    lstSlidePrintingPatientResponses = context.GetSlidePrintingPatientDTO.FromSqlRaw(
                    "Execute dbo.pro_GetSlidePrintPatientdetails @VenueNo,@VenueBranchNo,@VisitNo,@ServiceNo",
                    _VenueNo, _VenueBranchNo, _VisitNo, _ServiceNo).ToList();

                    MappingSlideprintPatientdetails(lstSlidePrintingPatientResponses, slidePrintPatientdetailsResponse);
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GetSlidePrintingPatientdetails", ExceptionPriority.High, ApplicationType.REPOSITORY, RequestItem?.VenueNo, RequestItem?.VenueBranchNo, RequestItem?.userNo);
            }
            return slidePrintPatientdetailsResponse;
        }
        private static void MappingSlideprintPatientdetails(List<GetSlidePrintPatientdetailsResponse> lstSlidePrintingPatientResponses, SlidePrintPatientdetailsResponse slidePrintPatientdetailsResponse)
        {
            if (lstSlidePrintingPatientResponses.Any())
            {
                slidePrintPatientdetailsResponse.Sno = lstSlidePrintingPatientResponses.FirstOrDefault().Sno;
                slidePrintPatientdetailsResponse.RCNo = lstSlidePrintingPatientResponses.FirstOrDefault().RCNo;
                slidePrintPatientdetailsResponse.RHNo = lstSlidePrintingPatientResponses.FirstOrDefault().RHNo;
                slidePrintPatientdetailsResponse.RPNo = lstSlidePrintingPatientResponses.FirstOrDefault().RPNo;
                slidePrintPatientdetailsResponse.RCHNo = lstSlidePrintingPatientResponses.FirstOrDefault().RCHNo;
                slidePrintPatientdetailsResponse.PatientVisitNo = lstSlidePrintingPatientResponses.FirstOrDefault().PatientVisitNo;
                slidePrintPatientdetailsResponse.RegisterDate = lstSlidePrintingPatientResponses.FirstOrDefault().RegisterDate;
                slidePrintPatientdetailsResponse.LabRequestNo = lstSlidePrintingPatientResponses.FirstOrDefault().LabRequestNo;
                slidePrintPatientdetailsResponse.ReportDate = lstSlidePrintingPatientResponses.FirstOrDefault().ReportDate;
                slidePrintPatientdetailsResponse.PatientNRIC = lstSlidePrintingPatientResponses.FirstOrDefault().PatientNRIC;
                slidePrintPatientdetailsResponse.DiscardDate = lstSlidePrintingPatientResponses.FirstOrDefault().DiscardDate;
                slidePrintPatientdetailsResponse.PatientName = lstSlidePrintingPatientResponses.FirstOrDefault().PatientName;
                slidePrintPatientdetailsResponse.DoctorId = lstSlidePrintingPatientResponses.FirstOrDefault().DoctorId;
                slidePrintPatientdetailsResponse.DoctorName = lstSlidePrintingPatientResponses.FirstOrDefault().DoctorName;
                slidePrintPatientdetailsResponse.Remarks = lstSlidePrintingPatientResponses.FirstOrDefault().Remarks;
                slidePrintPatientdetailsResponse.Consultant = lstSlidePrintingPatientResponses.FirstOrDefault().Consultant;
                slidePrintPatientdetailsResponse.FooterNote = lstSlidePrintingPatientResponses.FirstOrDefault().FooterNote;
                slidePrintPatientdetailsResponse.TissueAudit = lstSlidePrintingPatientResponses.FirstOrDefault().TissueAudit;
                slidePrintPatientdetailsResponse.ReportStatus = lstSlidePrintingPatientResponses.FirstOrDefault().ReportStatus;
                slidePrintPatientdetailsResponse.AmedReason = lstSlidePrintingPatientResponses.FirstOrDefault().AmedReason;
                slidePrintPatientdetailsResponse.AmedBy = lstSlidePrintingPatientResponses.FirstOrDefault().AmedBy;
                slidePrintPatientdetailsResponse.AmedDate = lstSlidePrintingPatientResponses.FirstOrDefault().AmedDate;
                slidePrintPatientdetailsResponse.LabelPrintingType = lstSlidePrintingPatientResponses.FirstOrDefault().LabelPrintingType;
                slidePrintPatientdetailsResponse.Others = lstSlidePrintingPatientResponses.FirstOrDefault().Others;
                slidePrintPatientdetailsResponse.SecondConsultation = lstSlidePrintingPatientResponses.FirstOrDefault().SecondConsultation;
                slidePrintPatientdetailsResponse.photo = lstSlidePrintingPatientResponses.FirstOrDefault().photo;
                slidePrintPatientdetailsResponse.ApprovalDoctor = lstSlidePrintingPatientResponses.FirstOrDefault().ApprovalDoctor;
                slidePrintPatientdetailsResponse.SampleSource = lstSlidePrintingPatientResponses.FirstOrDefault().SampleSource;
                slidePrintPatientdetailsResponse.SampleSourceDesc = lstSlidePrintingPatientResponses.FirstOrDefault().SampleSourceDesc;
                slidePrintPatientdetailsResponse.IsReject = lstSlidePrintingPatientResponses.FirstOrDefault().IsReject;
                slidePrintPatientdetailsResponse.RejectionCode = lstSlidePrintingPatientResponses.FirstOrDefault().RejectionCode;

                List<Specimen> specimens = new List<Specimen>();
                List<Slide> slides = new List<Slide>();
                
                foreach (var Patientdetails in lstSlidePrintingPatientResponses?.OrderBy(x => x.SlideSpecimenType).ThenBy(x => x.SlideBlock).ThenBy(x => x.Level))
                {
                    if (Patientdetails.SlideSpecimenType > 0)
                    {
                        Slide slide = new Slide();
                        slide.SlideSpecimenType = Patientdetails.SlideSpecimenType;
                        slide.SlideSpecimenNo = Patientdetails.SlideSpecimenNo;
                        slide.SlideBlock = Patientdetails.SlideBlock;
                        slide.Level = Patientdetails.Level;
                        slide.StainType = Patientdetails.StainType;
                        slide.SlideSelected = Patientdetails.SlideSelected;
                        slide.SlideDate = Patientdetails.SlideDate;
                        slides.Add(slide);
                    }
                }

                foreach (var Patientdetails in lstSlidePrintingPatientResponses?.OrderBy(x => x.SlideSpecimenType))
                {
                    var specimenCount = specimens.Where(x => x.SpecimenType == Patientdetails.SpecimenType).ToList();
                    if (specimenCount.Count == 0)
                    {
                        Specimen specimen = new Specimen();
                        specimen.slides = slides.Where(x => x.SlideSpecimenType == Patientdetails.SpecimenType).ToList(); ;
                        specimen.SpecimenNo = Patientdetails.SpecimenNo;
                        specimen.SpecimenDateTime = Patientdetails.SpecimenDateTime;
                        specimen.Block = Patientdetails.Block;
                        specimen.TrimmedBy = Patientdetails.TrimmedBy;
                        specimen.CreatedBy = Patientdetails.CreatedBy;
                        specimen.ModifiedBy = Patientdetails.ModifiedBy;
                        specimen.Selected = Patientdetails.Selected;
                        specimen.SpecimenType = Patientdetails.SpecimenType;
                        specimen.Pap = Patientdetails.Pap;
                        specimen.HcStain = Patientdetails.HcStain;
                        specimen.HeStain = Patientdetails.HeStain;
                        specimen.Centrifuge = Patientdetails.Centrifuge;
                        specimen.Cytospin = Patientdetails.Cytospin;
                        specimen.Adequacy = Patientdetails.Adequacy;
                        specimen.Brusing = Patientdetails.Brusing;
                        specimen.CellBlocking = Patientdetails.CellBlocking;
                        specimen.TissueProcessor = Patientdetails.TissueProcessor;
                        specimen.SpecimenOthers = Patientdetails.SpecimenOthers;
                        specimen.IsShowSpecimenOthers = Patientdetails.IsShowSpecimenOthers;
                        specimens.Add(specimen);
                    }
                }
                slidePrintPatientdetailsResponse.specimens = specimens;
            }
        }
        public CommonTokenResponse SaveSlidePrintingDetails(SlidePrintPatientdetailsResponse slidePrintPatientdetails)
        {
            CommonTokenResponse response = new CommonTokenResponse();

            CommonHelper commonUtility = new CommonHelper();
            var slidePrintingXML = commonUtility.ToXML(slidePrintPatientdetails);
            var specimenXML = commonUtility.ToXML(slidePrintPatientdetails?.specimens);

            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", slidePrintPatientdetails?.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", slidePrintPatientdetails?.VenueBranchNo);
                    var _SlidePrintingXML = new SqlParameter("SlidePrintingXML", slidePrintingXML);
                    var _SpecimenXML = new SqlParameter("SpecimenXML", specimenXML);
                    var _DepartmentType = new SqlParameter("DepartmentType", slidePrintPatientdetails?.DepartmentType);
                    var _IsRCHNo = new SqlParameter("IsRCHNo", slidePrintPatientdetails?.IsRCHNo);
                    var _IsReject = new SqlParameter("IsReject", slidePrintPatientdetails?.IsReject);

                    var Objresult = context.CreateSlidePrintingDTO.FromSqlRaw(
                    "Execute dbo.Pro_InsertSlidePrinting @VenueNo,@VenueBranchNo,@SlidePrintingXML,@SpecimenXML,@DepartmentType,@IsRCHNo,@IsReject",
                    _VenueNo, _VenueBranchNo, _SlidePrintingXML, _SpecimenXML, _DepartmentType, _IsRCHNo, _IsReject).ToList();
                    response = Objresult[0];
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "SaveSlidePrintingDetails", ExceptionPriority.Low, ApplicationType.REPOSITORY, 0, 0, 0);
            }
            return response;
        }

        /// <summary>
        /// Generate Slide Number
        /// </summary>
        /// <returns></returns>
        public CommonTokenResponse GenerateSlideNumber(CommonFilterRequestDTO RequestItem)
        {
            CommonTokenResponse commonResponse = new CommonTokenResponse();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", RequestItem?.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", RequestItem?.VenueBranchNo);
                    var _Type = new SqlParameter("Type", RequestItem?.Type);

                    var response = context.GetGenerateRCHNoDTO.FromSqlRaw(
                    "Execute dbo.pro_GenerateSlidePrintNumber @VenueNo,@VenueBranchNo,@Type",
                    _VenueNo, _VenueBranchNo, _Type).ToList();

                    commonResponse = response[0];
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GenerateSlideNUmber", ExceptionPriority.High, ApplicationType.REPOSITORY, RequestItem?.VenueNo, RequestItem?.VenueBranchNo, RequestItem?.userNo);
            }
            return commonResponse;
        }

        /// <summary>
        /// Generate Slide Number
        /// </summary>
        /// <returns></returns>
        public List<ExistingRCHNoResponse> GetExistingRCHNoDetails(CommonFilterRequestDTO RequestItem)
        {
            List<ExistingRCHNoResponse> commonResponse = new List<ExistingRCHNoResponse>();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", RequestItem?.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", RequestItem?.VenueBranchNo);
                    var _Type = new SqlParameter("DepartmentType", RequestItem?.SearchKey);
                    var _VisitNo = new SqlParameter("VisitNo", RequestItem?.visitNo);

                    commonResponse = context.GetExistngRCHNoDTO.FromSqlRaw(
                    "Execute dbo.pro_GetExistingRCHNoDetails @VenueNo,@VenueBranchNo,@DepartmentType,@VisitNo",
                    _VenueNo, _VenueBranchNo, _Type, _VisitNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GetExistingRCHNoDetails", ExceptionPriority.High, ApplicationType.REPOSITORY, RequestItem?.VenueNo, RequestItem?.VenueBranchNo, RequestItem?.userNo);
            }
            return commonResponse;
        }
        public List<GetBulkSlidePrintingDetails> GetBulkSlidePrintDetails(GetBulkSlidePrintingRequest RequestItem)
        {
            List<GetBulkSlidePrintingDetails> commonResponse = new List<GetBulkSlidePrintingDetails>();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", RequestItem?.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", RequestItem?.VenueBranchNo);
                    var _FromRCHNo = new SqlParameter("FromRCHNo", RequestItem?.FromRCHNo);
                    var _ToRCHNo = new SqlParameter("ToRCHNo", RequestItem?.ToRCHNo);
                    var _UserNo = new SqlParameter("UserNo", RequestItem?.UserNo);

                    commonResponse = context.GetBulkSlidePrintingDTO.FromSqlRaw(
                    "Execute dbo.Pro_GetBulkSlidePrintDetails @VenueNo,@VenueBranchNo,@FromRCHNo,@ToRCHNo,@UserNo",
                    _VenueNo, _VenueBranchNo, _FromRCHNo, _ToRCHNo, _UserNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GetBulkSlidePrintDetails", ExceptionPriority.High, ApplicationType.REPOSITORY, RequestItem?.VenueNo, RequestItem?.VenueBranchNo, 0);
            }
            return commonResponse;
        }
    }
}
