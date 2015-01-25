using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BB.UnitOfWorkEntityFramework;
using BB.BusinessLogicEntityFramework.Logic;
using BB.Container;
using BB.Interfaces;

namespace BB.BusinessLogicEntityFramework.Utilities
{
    public static class AutoMapperUtilities
    {
        public static void RegisterMappings()
        {
            Mapper.CreateMap<Beacon, Domain.Beacon>();
            Mapper.CreateMap<Domain.Beacon, Beacon>();

            Mapper.CreateMap<Course, Domain.Course>().ForMember(dest => dest.LessonIDs, opt => opt.MapFrom(c => c.Lessons.Select(i => i.LessonID).ToList()))
                                                                .ForMember(dest => dest.StudentIDs, opt => opt.MapFrom(c => c.Students.Select(i => i.UserID).ToList()))
                                                                .ForMember(dest => dest.LecturerIDs, opt => opt.MapFrom(c => c.Lecturers.Select(i => i.UserID).ToList()));
            Mapper.CreateMap<Domain.Course, Course>();

            Mapper.CreateMap<Lesson, Domain.Lesson>().ForMember(dest => dest.SessionIDs, opt => opt.MapFrom(so => so.Sessions.Select(i => i.SessionID).ToList()))
                .ForMember(dest => dest.ResourceIDs, opt => opt.MapFrom(so => so.Resources.Select(i => i.ResourceID).ToList()))
                .ForMember(dest => dest.CourseIDs, opt => opt.MapFrom(so => so.Courses.Select(i => i.CourseID).ToList()));
            Mapper.CreateMap<Domain.Lesson, Lesson>().AfterMap((src, dest) => {
                var businessLogic = BeaconBoardContainer.GetInstance<IResourceBusinessLogic>();
                foreach(Guid id in src.ResourceIDs)
                {
                    var obj = businessLogic.GetByID(id);
                }
            });

            Mapper.CreateMap<Resource, Domain.Resource>().ForMember(dest => dest.LessonIDs, opt => opt.MapFrom(c => c.Lessons.Select(i => i.LessonID).ToList()));
            Mapper.CreateMap<Domain.Resource, Resource>();

            Mapper.CreateMap<ResourceType, Domain.ResourceType>().ForMember(dest => dest.ResourceIDs, opt => opt.MapFrom(c => c.Resources.Select(i => i.ResourceID).ToList()));
            Mapper.CreateMap<Domain.ResourceType, ResourceType>();

            Mapper.CreateMap<Room, Domain.Room>().ForMember(dest => dest.BeaconIDs, opt => opt.MapFrom(c => c.Beacons.Select(i => i.BeaconID).ToList()));
            Mapper.CreateMap<Domain.Room, Room>();

            Mapper.CreateMap<Session, Domain.Session>().ForMember(dest => dest.LecturerIDs, opt => opt.MapFrom(c => c.Lecturers.Select(i => i.UserID).ToList()));
            Mapper.CreateMap<Domain.Session, Session>();
        }
    }
}
