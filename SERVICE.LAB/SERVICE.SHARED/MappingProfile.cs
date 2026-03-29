using AutoMapper;
using Service.Model;
using Service.Model.Integration;
using Service.Model.Sample;

namespace Shared
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<IntegrationOrderVisitDetails, IntegrationOrderVisitDetailsResponse>();
            CreateMap<IntegrationOrderPatientDetails, IntegrationOrderPatientDetailsResponse>();
            CreateMap<IntegrationOrderClientDetails, IntegrationOrderClientDetailsResponse>();
            CreateMap<IntegrationOrderDoctorDetails, IntegrationOrderDoctorDetailsResponse>();
            CreateMap<IntegrationOrderTestDetails, IntegrationOrderTestDetailsResponse>();
            CreateMap<IntegrationOrderTestDetailsResponse, IntegrationOrderTestDetails>();
            CreateMap<IntegrationOrderWardDetails, IntegrationOrderWardDetailsResponse>();
            CreateMap<IntegrationOrderAllergyDetails, IntegrationOrderAllergyDetailsResponse>();
            CreateMap<IntegrationOrderDetails, IntegrationOrderDetailsResponse>();
            CreateMap<WaitingListCreateManageSampleRequest, CreateManageSampleRequest>();
            CreateMap<CreateManageSampleResponse, WaitingListCreateManageSampleResponse>();
            CreateMap<PatientNotifyLog, WaitingListMessage>();

            CreateMap<GetEditPatientDetailsFinalResponse, FrontOffficeDTO>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender == 1 ? "Male" : "Female"))
                .ForMember(dest => dest.Orders, opt => opt.MapFrom(src => src.servicelist))
                .ForMember(dest => dest.Payments, opt => opt.MapFrom(src => src.payments));

            CreateMap<EditBillServiceDetails, FrontOfficeOrderList>()
                .ForMember(dest => dest.TestType, opt => opt.MapFrom(src => src.servicetype))
                .ForMember(dest => dest.TestCode, opt => opt.MapFrom(src => src.servicecode))
                .ForMember(dest => dest.TestNo, opt => opt.MapFrom(src => src.Serviceno))
                .ForMember(dest => dest.TestName, opt => opt.MapFrom(src => src.servicename));

            CreateMap<GetEditBillPaymentDetails, FrontOfficePayment>()
                .ForMember(dest => dest.ModeOfType, opt => opt.MapFrom(src => src.PaymentType));
        }
    }
}