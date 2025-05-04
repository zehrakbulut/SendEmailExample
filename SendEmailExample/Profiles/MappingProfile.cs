using AutoMapper;
using SendEmailExample.Dtos;
using SendEmailExample.Models.Tables;

namespace SendEmailExample.Profiles
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<CreateMedalDto, Medal>();
			CreateMap<Medal, MedalDto>();

			CreateMap<User, AuthorWithMedalsDto>()
				.ForMember(dest => dest.Medals, opt => opt.MapFrom(src =>
					src.UserMedals.Select(um => new UserMedalDto
					{
						MedalId = um.MedalId,
						MedalName = um.Medal.Name,
						IconUrl = um.Medal.IconUrl,
						AssignedAt = um.AssignedAt
					}).ToList()
				));
		}
	}
}
