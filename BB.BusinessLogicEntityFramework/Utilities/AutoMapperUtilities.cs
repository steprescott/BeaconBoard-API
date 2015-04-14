using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BB.UnitOfWorkEntityFramework;
using BB.BusinessLogicEntityFramework.Logic;
using BB.Interfaces;

namespace BB.BusinessLogicEntityFramework.Utilities
{
    public static class AutoMapperUtilities
    {
        public static void RegisterMappings()
        {
            Mapper.CreateMap<Attendance, Domain.Attendance>();
            Mapper.CreateMap<Domain.Attendance, Attendance>();

            Mapper.CreateMap<Beacon, Domain.Beacon>();
            Mapper.CreateMap<Domain.Beacon, Beacon>();

            Mapper.CreateMap<Course, Domain.Course>().ForMember(dest => dest.ModuleIDs, opt => opt.MapFrom(c => c.Modules.Select(i => i.ModuleID).ToList()))
                                                                .ForMember(dest => dest.StudentIDs, opt => opt.MapFrom(c => c.Students.Select(i => i.UserID).ToList()))
                                                                .ForMember(dest => dest.LecturerIDs, opt => opt.MapFrom(c => c.Lecturers.Select(i => i.UserID).ToList()))
                                                                .ForMember(dest => dest.ModuleIDs, opt => opt.MapFrom(c => c.Modules.Select(i => i.ModuleID).ToList()));
            Mapper.CreateMap<Domain.Course, Course>();

            Mapper.CreateMap<Lecturer, Domain.Lecturer>().ForMember(dest => dest.CourseIDs, opt => opt.MapFrom(c => c.Courses.Select(i => i.CourseID).ToList()))
                .ForMember(dest => dest.SessionIDs, opt => opt.MapFrom(c => c.Sessions.Select(i => i.SessionID).ToList()));
            Mapper.CreateMap<Domain.Lecturer, Lecturer>();

            Mapper.CreateMap<Lesson, Domain.Lesson>().ForMember(dest => dest.SessionIDs, opt => opt.MapFrom(so => so.Sessions.Select(i => i.SessionID).ToList()))
                .ForMember(dest => dest.ResourceIDs, opt => opt.MapFrom(so => so.Resources.Select(i => i.ResourceID).ToList()));
            Mapper.CreateMap<Domain.Lesson, Lesson>();

            Mapper.CreateMap<Module, Domain.Module>().ForMember(dest => dest.CourseIDs, opt => opt.MapFrom(c => c.Courses.Select(i => i.CourseID).ToList()))
                .ForMember(dest => dest.SessionIDs, opt => opt.MapFrom(c => c.Sessions.Select(i => i.SessionID).ToList()));
            Mapper.CreateMap<Domain.Module, Module>();

            Mapper.CreateMap<Resource, Domain.Resource>().ForMember(dest => dest.LessonIDs, opt => opt.MapFrom(c => c.Lessons.Select(i => i.LessonID).ToList()));
            Mapper.CreateMap<Domain.Resource, Resource>();

            Mapper.CreateMap<ResourceType, Domain.ResourceType>().ForMember(dest => dest.ResourceIDs, opt => opt.MapFrom(c => c.Resources.Select(i => i.ResourceID).ToList()));
            Mapper.CreateMap<Domain.ResourceType, ResourceType>();

            Mapper.CreateMap<Role, Domain.Role>().ForMember(dest => dest.UserIDs, opt => opt.MapFrom(c => c.Users.Select(i => i.UserID).ToList()));
            Mapper.CreateMap<Domain.Role, Role>();

            Mapper.CreateMap<Room, Domain.Room>().ForMember(dest => dest.BeaconIDs, opt => opt.MapFrom(c => c.Beacons.Select(i => i.BeaconID).ToList()))
                .ForMember(dest => dest.SessionIDs, opt => opt.MapFrom(c => c.Sessions.Select(i => i.SessionID).ToList()));
            Mapper.CreateMap<Domain.Room, Room>();

            Mapper.CreateMap<Session, Domain.Session>().ForMember(dest => dest.LecturerIDs, opt => opt.MapFrom(c => c.Lecturers.Select(i => i.UserID).ToList()))
                .ForMember(dest => dest.AttendanceIDs, opt => opt.MapFrom(c => c.Attendances.Select(i => i.AttendanceID).ToList()));
            Mapper.CreateMap<Domain.Session, Session>();

            Mapper.CreateMap<Student, Domain.Student>().ForMember(dest => dest.CourseIDs, opt => opt.MapFrom(c => c.Courses.Select(i => i.CourseID).ToList()));
            Mapper.CreateMap<Domain.Student, Student>();

            Mapper.CreateMap<User, Domain.User>();
            Mapper.CreateMap<Domain.User, User>();
        }
    }
}
