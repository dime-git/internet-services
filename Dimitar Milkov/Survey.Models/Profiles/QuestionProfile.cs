using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Survey.Data.Entities;
using Survey.Models.Models.Question;

namespace Survey.Models.Profiles
{
    public class QuestionProfile : Profile
    {
        public QuestionProfile()
        {
            CreateMap<Question, QuestionModelBase>().ReverseMap();
            CreateMap<Question, QuestionModelExtended>();
            CreateMap<QuestionCreateModel, Question>()
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Text))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
            CreateMap<QuestionUpdateModel, Question>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Text)).ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description)).ReverseMap();


        }
    }
}
