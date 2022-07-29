using Api.Entities;
using Api.Entities.CourseEntities;
using Api.Entities.Discussion;
using Api.Entities.Ratings;
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
            CreateMap<UserForRegisterDto, User>()
                .ForMember(d => d.Email, opt => opt.MapFrom(src => src.EmailAddress))
                .ForMember(d => d.Name, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(d => d.Surname, opt => opt.MapFrom(src => src.LastName));
            CreateMap<User, User>();
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
            CreateMap<QuestionForCreationDto, Question>().ReverseMap();
            CreateMap<Question, QuestionDto>().ReverseMap();
            CreateMap<AnswerForCreationDto, Answer>().ReverseMap();
            CreateMap<AnswerDto, Answer>().ReverseMap();
            CreateMap<RatingForCreationDto, Rating>();
            CreateMap<CourseElementDto, CourseElement>().ReverseMap();
            CreateMap<CourseElementForCreationDto, CourseElement>();
            CreateMap<CourseElementForUpdateDto, CourseElement>();
            CreateMap<CourseElementArticleContentDto, CourseElementArticleContent>().ReverseMap();
            CreateMap<CourseElementVideoContentDto, CourseElementVideoContent>().ReverseMap();
        }
    }
}
