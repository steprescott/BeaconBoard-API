using BB.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BB.Container;

namespace BB.WebApi.Services
{
    public class BeaconBoardService
    {
        public IAttendanceBusinessLogic AttendanceBusinessLogic
        {
            get { return BeaconBoardContainer.GetInstance<IAttendanceBusinessLogic>(); }
        }

        public IBeaconBusinessLogic BeaconBusinessLogic
        {
            get { return BeaconBoardContainer.GetInstance<IBeaconBusinessLogic>(); }
        }

        public ICourseBusinessLogic CourseBusinessLogic
        {
            get { return BeaconBoardContainer.GetInstance<ICourseBusinessLogic>(); }
        }

        public ILecturerBusinessLogic LecturerBusinessLogic
        {
            get { return BeaconBoardContainer.GetInstance<ILecturerBusinessLogic>(); }
        }

        public ILessonBusinessLogic LessonBusinessLogic
        {
            get { return BeaconBoardContainer.GetInstance<ILessonBusinessLogic>(); }
        }

        public IModuleBusinessLogic ModuleBusinessLogic
        {
            get { return BeaconBoardContainer.GetInstance<IModuleBusinessLogic>(); }
        }

        public IResourceBusinessLogic ResourceBusinessLogic
        {
            get { return BeaconBoardContainer.GetInstance<IResourceBusinessLogic>(); }
        }

        public IResourceTypeBusinessLogic ResourceTypeBusinessLogic
        {
            get { return BeaconBoardContainer.GetInstance<IResourceTypeBusinessLogic>(); }
        }

        public IRoleBusinessLogic RoleBusinessLogic
        {
            get { return BeaconBoardContainer.GetInstance<IRoleBusinessLogic>(); }
        }

        public IRoomBusinessLogic RoomBusinessLogic
        {
            get { return BeaconBoardContainer.GetInstance<IRoomBusinessLogic>(); }
        }

        public ISessionBusinessLogic SessionBusinessLogic
        {
            get { return BeaconBoardContainer.GetInstance<ISessionBusinessLogic>(); }
        }

        public IStudentBusinessLogic StudentBusinessLogic
        {
            get { return BeaconBoardContainer.GetInstance<IStudentBusinessLogic>(); }
        }

        public ITokenBusinessLogic TokenBusinessLogic
        {
            get { return BeaconBoardContainer.GetInstance<ITokenBusinessLogic>(); }
        }

        public IUserBusinessLogic UserBusinessLogic
        {
            get { return BeaconBoardContainer.GetInstance<IUserBusinessLogic>(); }
        }
    }
}