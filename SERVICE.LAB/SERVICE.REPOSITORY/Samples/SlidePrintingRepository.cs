using DEV.Common;
using Service.Model.EF;
using Service.Model.Sample;
using Service.Model;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Dev.IRepository;

namespace Dev.Repository.Samples
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
        public SlidePrintPatientDetailsResponse GetSlidePrintingPatientDetails(CommonFilterRequestDTO RequestItem)
        {
            List<GetSlidePrintPatientDetailsResponse> lstSlidePrintingPatientResponses = new List<GetSlidePrintPatientDetailsResponse>();
            SlidePrintPatientDetailsResponse slidePrintPatientDetailsResponse = new SlidePrintPatientDetailsResponse();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {

                    var _VenueNo = new SqlParameter("VenueNo", RequestItem?.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", RequestItem?.VenueBranchNo);
                    var _VisitNo = new SqlParameter("VisitNo", RequestItem?.visitNo);
                    var _ServiceNo = new SqlParameter("ServiceNo", RequestItem?.serviceNo);

                    lstSlidePrintingPatientResponses = context.GetSlidePrintingPatientDTO.FromSqlRaw(
                        "Execute dbo.pro_GetSlidePrintPatientDetails @VenueNo,@VenueBranchNo,@VisitNo,@ServiceNo",
                         _VenueNo, _VenueBranchNo, _VisitNo, _ServiceNo).ToList();

                    MappingSlideprintPatientDetails(lstSlidePrintingPatientResponses, slidePrintPatientDetailsResponse);

                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GetSlidePrintingPatientDetails", ExceptionPriority.High, ApplicationType.REPOSITORY, RequestItem?.VenueNo, RequestItem?.VenueBranchNo, RequestItem?.userNo);
            }
            return slidePrintPatientDetailsResponse;
        }

        private static void MappingSlideprintPatientDetails(List<GetSlidePrintPatientDetailsResponse> lstSlidePrintingPatientResponses, SlidePrintPatientDetailsResponse slidePrintPatientDetailsResponse)
        {


            if (lstSlidePrintingPatientResponses.Any())
            {
                slidePrintPatientDetailsResponse.Sno = lstSlidePrintingPatientResponses.FirstOrDefault().Sno;
                slidePrintPatientDetailsResponse.RCNo = lstSlidePrintingPatientResponses.FirstOrDefault().RCNo;
                slidePrintPatientDetailsResponse.RHNo = lstSlidePrintingPatientResponses.FirstOrDefault().RHNo;
                slidePrintPatientDetailsResponse.RPNo = lstSlidePrintingPatientResponses.FirstOrDefault().RPNo;
                slidePrintPatientDetailsResponse.RCHNo = lstSlidePrintingPatientResponses.FirstOrDefault().RCHNo;
                slidePrintPatientDetailsResponse.PatientVisitNo = lstSlidePrintingPatientResponses.FirstOrDefault().PatientVisitNo;
                slidePrintPatientDetailsResponse.RegisterDate = lstSlidePrintingPatientResponses.FirstOrDefault().RegisterDate;
                slidePrintPatientDetailsResponse.LabRequestNo = lstSlidePrintingPatientResponses.FirstOrDefault().LabRequestNo;
                slidePrintPatientDetailsResponse.ReportDate = lstSlidePrintingPatientResponses.FirstOrDefault().ReportDate;
                slidePrintPatientDetailsResponse.PatientNRIC = lstSlidePrintingPatientResponses.FirstOrDefault().PatientNRIC;
                slidePrintPatientDetailsResponse.DiscardDate = lstSlidePrintingPatientResponses.FirstOrDefault().DiscardDate;
                slidePrintPatientDetailsResponse.PatientName = lstSlidePrintingPatientResponses.FirstOrDefault().PatientName;
                slidePrintPatientDetailsResponse.DoctorId = lstSlidePrintingPatientResponses.FirstOrDefault().DoctorId;
                slidePrintPatientDetailsResponse.DoctorName = lstSlidePrintingPatientResponses.FirstOrDefault().DoctorName;
                slidePrintPatientDetailsResponse.Remarks = lstSlidePrintingPatientResponses.FirstOrDefault().Remarks;
                slidePrintPatientDetailsResponse.Consultant = lstSlidePrintingPatientResponses.FirstOrDefault().Consultant;
                slidePrintPatientDetailsResponse.FooterNote = lstSlidePrintingPatientResponses.FirstOrDefault().FooterNote;
                slidePrintPatientDetailsResponse.TissueAudit = lstSlidePrintingPatientResponses.FirstOrDefault().TissueAudit;
                slidePrintPatientDetailsResponse.ReportStatus = lstSlidePrintingPatientResponses.FirstOrDefault().ReportStatus;
                slidePrintPatientDetailsResponse.AmedReason = lstSlidePrintingPatientResponses.FirstOrDefault().AmedReason;
                slidePrintPatientDetailsResponse.AmedBy = lstSlidePrintingPatientResponses.FirstOrDefault().AmedBy;
                slidePrintPatientDetailsResponse.AmedDate = lstSlidePrintingPatientResponses.FirstOrDefault().AmedDate;
                slidePrintPatientDetailsResponse.LabelPrintingType = lstSlidePrintingPatientResponses.FirstOrDefault().LabelPrintingType;
                slidePrintPatientDetailsResponse.Others = lstSlidePrintingPatientResponses.FirstOrDefault().Others;
                slidePrintPatientDetailsResponse.SecondConsultation = lstSlidePrintingPatientResponses.FirstOrDefault().SecondConsultation;
                slidePrintPatientDetailsResponse.photo = lstSlidePrintingPatientResponses.FirstOrDefault().photo;
                slidePrintPatientDetailsResponse.ApprovalDoctor = lstSlidePrintingPatientResponses.FirstOrDefault().ApprovalDoctor;
                slidePrintPatientDetailsResponse.SampleSource = lstSlidePrintingPatientResponses.FirstOrDefault().SampleSource;
                slidePrintPatientDetailsResponse.SampleSourceDesc = lstSlidePrintingPatientResponses.FirstOrDefault().SampleSourceDesc;
                slidePrintPatientDetailsResponse.IsReject = lstSlidePrintingPatientResponses.FirstOrDefault().IsReject;
                slidePrintPatientDetailsResponse.RejectionCode = lstSlidePrintingPatientResponses.FirstOrDefault().RejectionCode;

                List<Specimen> specimens = new List<Specimen>();
                List<Slide> slides = new List<Slide>();
                foreach (var patientDetails in lstSlidePrintingPatientResponses?.OrderBy(x => x.SlideSpecimenType).ThenBy(x => x.SlideBlock).ThenBy(x => x.Level))
                {
                    if (patientDetails.SlideSpecimenType > 0)
                    {
                        Slide slide = new Slide();
                        slide.SlideSpecimenType = patientDetails.SlideSpecimenType;
                        slide.SlideSpecimenNo = patientDetails.SlideSpecimenNo;
                        slide.SlideBlock = patientDetails.SlideBlock;
                        slide.Level = patientDetails.Level;
                        slide.StainType = patientDetails.StainType;
                        slide.SlideSelected = patientDetails.SlideSelected;
                        slide.SlideDate = patientDetails.SlideDate;
                        slides.Add(slide);
                    }
                }

                foreach (var patientDetails in lstSlidePrintingPatientResponses?.OrderBy(x => x.SlideSpecimenType))
                {
                    var specimenCount = specimens.Where(x => x.SpecimenType == patientDetails.SpecimenType).ToList();
                    if (specimenCount.Count == 0)
                    {
                        Specimen specimen = new Specimen();
                        specimen.slides = slides.Where(x => x.SlideSpecimenType == patientDetails.SpecimenType).ToList(); ;
                        specimen.SpecimenNo = patientDetails.SpecimenNo;
                        specimen.SpecimenDateTime = patientDetails.SpecimenDateTime;
                        specimen.Block = patientDetails.Block;
                        specimen.TrimmedBy = patientDetails.TrimmedBy;
                        specimen.CreatedBy = patientDetails.CreatedBy;
                        specimen.ModifiedBy = patientDetails.ModifiedBy;
                        specimen.Selected = patientDetails.Selected;
                        specimen.SpecimenType = patientDetails.SpecimenType;
                        specimen.Pap = patientDetails.Pap;
                        specimen.HcStain = patientDetails.HcStain;
                        specimen.HeStain = patientDetails.HeStain;
                        specimen.Centrifuge = patientDetails.Centrifuge;
                        specimen.Cytospin = patientDetails.Cytospin;
                        specimen.Adequacy = patientDetails.Adequacy;
                        specimen.Brusing = patientDetails.Brusing;
                        specimen.CellBlocking = patientDetails.CellBlocking;
                        specimen.TissueProcessor = patientDetails.TissueProcessor;
                        specimen.SpecimenOthers = patientDetails.SpecimenOthers;
                        specimen.IsShowSpecimenOthers = patientDetails.IsShowSpecimenOthers;
                        specimens.Add(specimen);
                    }

                }
                slidePrintPatientDetailsResponse.specimens = specimens;
            }
        }

        public CommonTokenResponse SaveSlidePrintingDetails(SlidePrintPatientDetailsResponse slidePrintPatientDetails)
        {
            CommonTokenResponse response = new CommonTokenResponse();

            CommonHelper commonUtility = new CommonHelper();
            var slidePrintingXML = commonUtility.ToXML(slidePrintPatientDetails);
            var specimenXML = commonUtility.ToXML(slidePrintPatientDetails?.specimens);

            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {

                    var _VenueNo = new SqlParameter("VenueNo", slidePrintPatientDetails?.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", slidePrintPatientDetails?.VenueBranchNo);
                    var _SlidePrintingXML = new SqlParameter("SlidePrintingXML", slidePrintingXML);
                    var _SpecimenXML = new SqlParameter("SpecimenXML", specimenXML);
                    var _DepartmentType = new SqlParameter("DepartmentType", slidePrintPatientDetails?.DepartmentType);
                    var _IsRCHNo = new SqlParameter("IsRCHNo", slidePrintPatientDetails?.IsRCHNo);
                    var _IsReject = new SqlParameter("IsReject", slidePrintPatientDetails?.IsReject);

                    var objresult = context.CreateSlidePrintingDTO.FromSqlRaw(
                        "Execute dbo.Pro_InsertSlidePrinting @VenueNo,@VenueBranchNo,@SlidePrintingXML,@SpecimenXML,@DepartmentType,@IsRCHNo,@IsReject",
                     _VenueNo, _VenueBranchNo, _SlidePrintingXML, _SpecimenXML, _DepartmentType, _IsRCHNo, _IsReject).ToList();
                    response = objresult[0];
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
