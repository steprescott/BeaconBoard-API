using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace BB.DataLayer.Utilities
{
    public static class AutoMapperUtilities
    {
        public static void RegisterMappings()
        {
            Mapper.CreateMap<Lesson, Domain.Lesson>().ForMember(dest => dest.SessionIDs, opt => opt.MapFrom(so => so.Sessions.Select(i => i.SessionID).ToList()))
                .ForMember(dest => dest.ResourceIDs, opt => opt.MapFrom(so => so.Resources.Select(i => i.ResourceID).ToList()))
                .ForMember(dest => dest.CourseIDs, opt => opt.MapFrom(so => so.Courses.Select(i => i.CourseID).ToList()));
        }
    }
}
