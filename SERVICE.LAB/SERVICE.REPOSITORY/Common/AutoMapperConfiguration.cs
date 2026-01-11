using AutoMapper;
using Service.Model;
using Service.Model.Integration;
using Service.Model.Sample;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dev.Repository
{
    public class AutoMapperConfiguration
    {
        public static IMapper Configure()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<IntegrationOrderVisitDetails, IntegrationOrderVisitDetailsResponse>();
                cfg.CreateMap<IntegrationOrderPatientDetails, IntegrationOrderPatientDetailsResponse>();
                cfg.CreateMap<IntegrationOrderClientDetails, IntegrationOrderClientDetailsResponse>();
                cfg.CreateMap<IntegrationOrderDoctorDetails, IntegrationOrderDoctorDetailsResponse>();
                cfg.CreateMap<IntegrationOrderTestDetails, IntegrationOrderTestDetailsResponse>();
                cfg.CreateMap<IntegrationOrderTestDetailsResponse, IntegrationOrderTestDetails>();
                cfg.CreateMap<IntegrationOrderWardDetails, IntegrationOrderWardDetailsResponse>();
                cfg.CreateMap<IntegrationOrderAllergyDetails, IntegrationOrderAllergyDetailsResponse>();
                cfg.CreateMap<IntegrationOrderDetails, IntegrationOrderDetailsResponse>();
                cfg.CreateMap<WaitingListCreateManageSampleRequest, CreateManageSampleRequest>();
                cfg.CreateMap<CreateManageSampleResponse, WaitingListCreateManageSampleResponse>();
                cfg.CreateMap<PatientNotifyLog, WaitingListMessage>();

                cfg.CreateMap<GetEditPatientDetailsFinalResponse, FrontOffficeDTO>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender == 1 ? "Male" : "Female"))
                .ForMember(dest => dest.Orders, opt => opt.MapFrom(src => src.servicelist))
                .ForMember(dest => dest.Payments, opt => opt.MapFrom(src => src.payments))
                ;

                cfg.CreateMap<EditBillServiceDetails, FrontOfficeOrderList>()
                .ForMember(dest => dest.TestType, src => src.MapFrom(src => src.servicetype))
                .ForMember(dest => dest.TestCode, src => src.MapFrom(src => src.servicecode))
                .ForMember(dest => dest.TestNo, src => src.MapFrom(src => src.Serviceno))
                .ForMember(dest => dest.TestName, src => src.MapFrom(src => src.servicename));

                cfg.CreateMap<GetEditBillPaymentDetails, FrontOfficePayment>()
                .ForMember(dest => dest.ModeOfType, src => src.MapFrom(src => src.PaymentType));

                // Add other mappings as needed
            });

            IMapper mapper = mapperConfig.CreateMapper();
            return mapper;
        }
    }
}
