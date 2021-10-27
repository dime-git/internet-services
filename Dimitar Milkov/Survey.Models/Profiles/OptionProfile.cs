using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using AutoMapper;
using Survey.Data.Entities;
using Survey.Models.Models.Option;

namespace Survey.Models.Profiles
{
    class OptionProfile : Profile
    {
        public OptionProfile()
        {
            CreateMap<Option, OptionModel>().ReverseMap();
            CreateMap<OptionCreateModel, Option>();
            CreateMap<OptionUpdateModel, Option>();
        }
       
    }
}
