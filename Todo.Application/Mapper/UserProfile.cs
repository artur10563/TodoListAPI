using AutoMapper;
using Todo.Application.DTOs.UserDTOs;
using Todo.Domain.Entities;

namespace Todo.Application.Mapper
{
	public class UserProfile : Profile
	{
		public UserProfile()
		{
			CreateMap<User, UserInfoDTO>()
				.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
				.ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));
		}
	}
}
