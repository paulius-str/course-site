using Api.Entities;
using Api.Entities.CourseEntities;
using Api.Entities.Ratings;
using Api.Service;
using Api.Shared;
using Api.Shared.DataTransferObjects;
using AutoMapper;

namespace API
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserDto, User>().ReverseMap();
            CreateMap<UserForRegisterDto, User>();
            CreateMap<UserForLoginDto, User>();
            CreateMap<CourseDto, Course>().ReverseMap();
            CreateMap<CourseForCreationDto, Course>();
            CreateMap<CourseForUpdateDto, Course>();
            CreateMap<CourseElementDto, CourseElement>().ReverseMap();
            CreateMap<CourseElementForCreationDto, CourseElement>();
            CreateMap<CourseElementForUpdateDto, CourseElement>();
            CreateMap<CourseSectionDto, CourseSection>().ReverseMap();
            CreateMap<CourseSectionForCreationDto, CourseSection>();
            CreateMap<CourseSectionForUpdateDto, CourseSection>();
            CreateMap<RatingDto, Rating>().ReverseMap();
            CreateMap<RatingForCreationDto, Rating>();
            CreateMap<CourseElementDto, CourseElement>().ReverseMap();
            CreateMap<CourseElementForCreationDto, CourseElement>();
            CreateMap<CourseElementForUpdateDto, CourseElement>();
            CreateMap<CourseElementArticleContentDto, CourseElementArticleContent>().ReverseMap();
            CreateMap<CourseElementVideoContentDto, CourseElementVideoContentDto>().ReverseMap();
        }
    }
}
