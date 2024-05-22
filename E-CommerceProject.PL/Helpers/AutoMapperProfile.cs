using AutoMapper;
using CourseBookingSystem.DAL.Models;
using CourseBookingSystem.PL.ViewModels;

namespace CourseBookingSystem.PL.Helpers
{
    public class AutoMapperProfile:Profile
    {

        public AutoMapperProfile()
        {
                 CreateMap<RegisterViewModel, User>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.UserPhone, opt => opt.MapFrom(src => src.UserPhone))
                .ForMember(dest => dest.UserPassword, opt => opt.MapFrom(src => src.UserPassword))
                .ForMember(dest => dest.UserEmail, opt => opt.MapFrom(src => src.UserEmail));

                CreateMap<ClientFormViewModel, Client>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender));

                CreateMap<Client, ClientFormViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ClientId))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender));

                CreateMap<ClientRequest, ClientFormViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ServiceId, opt => opt.MapFrom(src => src.ServiceId))
                .ForMember(dest => dest.SomeServiceDetails, opt => opt.MapFrom(src => src.SomeServiceDetails))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Client.FullName))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Client.PhoneNumber))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Client.Email))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Client.Gender));
        

                CreateMap<ClientFormViewModel, ClientRequest>()
                .ForMember(dest => dest.ServiceId, opt => opt.MapFrom(src => src.ServiceId))
                .ForMember(dest => dest.SomeServiceDetails, opt => opt.MapFrom(src => src.SomeServiceDetails));
        }


    }
}
