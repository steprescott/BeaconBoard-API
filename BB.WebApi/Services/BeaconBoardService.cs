﻿using BB.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BB.Container;

namespace BB.WebApi.Services
{
    public class BeaconBoardService
    {
        public IBeaconBusinessLogic BeaconBusinessLogic
        {
            get { return BeaconBoardContainer.GetInstance<IBeaconBusinessLogic>(); }
        }

        public ICourseBusinessLogic CourseBusinessLogic
        {
            get { return BeaconBoardContainer.GetInstance<ICourseBusinessLogic>(); }
        }

        public ILessonBusinessLogic LessonBusinessLogic
        {
            get { return BeaconBoardContainer.GetInstance<ILessonBusinessLogic>(); }
        }

        public IResourceBusinessLogic ResourceBusinessLogic
        {
            get { return BeaconBoardContainer.GetInstance<IResourceBusinessLogic>(); }
        }

        public IResourceTypeBusinessLogic ResourceTypeBusinessLogic
        {
            get { return BeaconBoardContainer.GetInstance<IResourceTypeBusinessLogic>(); }
        }

        public IRoomBusinessLogic RoomBusinessLogic
        {
            get { return BeaconBoardContainer.GetInstance<IRoomBusinessLogic>(); }
        }

        public ISessionBusinessLogic SessionBusinessLogic
        {
            get { return BeaconBoardContainer.GetInstance<ISessionBusinessLogic>(); }
        }
    }
}