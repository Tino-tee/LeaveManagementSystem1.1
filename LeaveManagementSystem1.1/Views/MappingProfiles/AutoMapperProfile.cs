using AutoMapper;
using LeaveManagementSystem1._1.Data;
using LeaveManagementSystem1._1.Models.LeaveTypes;

namespace LeaveManagementSystem1._1.Views.MappingProfiles
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {

            CreateMap<LeaveType, LeaveTypeReadOnlyViewModel>()
                .ForMember(dest => dest.Days, opt => opt.MapFrom(src => src.NumberOfDays));

            CreateMap<LeaveTypesCreateVM, LeaveType>()
                .ForMember(dest => dest.NumberOfDays, opt => opt.MapFrom(src => src.Days));

            CreateMap<LeaveTypesEditVM, LeaveType>()
                .ForMember(dest => dest.NumberOfDays, opt => opt.MapFrom(src => src.Days))
                .ReverseMap()
                .ForMember(dest => dest.Days, opt => opt.MapFrom(src => src.NumberOfDays));

        }
    }
}
