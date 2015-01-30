using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;
using System.Text;
using System.Threading.Tasks;
using BB.Interfaces;
using BB.UnitOfWorkEntityFramework;
using BB.BusinessLogicEntityFramework.Logic;

namespace BB.Container
{
    public class BeaconBoardContainer
    {
        private static UnityContainer _unityContainer;

        public static T GetInstance<T>()
        {
            if (_unityContainer == null)
            {
                Register();
            }

            return _unityContainer.Resolve<T>();
        }

        private static void Register()
        {
            _unityContainer = new UnityContainer();

            _unityContainer.RegisterType<IUnitOfWork, UnitOfWorkEntityFrameworkImplementation>(new ContainerControlledLifetimeManager());

            _unityContainer.RegisterType<IAttendanceBusinessLogic, AttendanceBusinessLogic>(new ContainerControlledLifetimeManager());
            _unityContainer.RegisterType<IBeaconBusinessLogic, BeaconBusinessLogic>(new ContainerControlledLifetimeManager());
            _unityContainer.RegisterType<ICourseBusinessLogic, CourseBusinessLogic>(new ContainerControlledLifetimeManager());
            _unityContainer.RegisterType<ILessonBusinessLogic, LessonBusinessLogic>(new ContainerControlledLifetimeManager());
            _unityContainer.RegisterType<IResourceBusinessLogic, ResourceBusinessLogic>(new ContainerControlledLifetimeManager());
            _unityContainer.RegisterType<IResourceTypeBusinessLogic, ResourceTypeBusinessLogic>(new ContainerControlledLifetimeManager());
            _unityContainer.RegisterType<IRoleBusinessLogic, RoleBusinessLogic>(new ContainerControlledLifetimeManager());
            _unityContainer.RegisterType<IRoomBusinessLogic, RoomBusinessLogic>(new ContainerControlledLifetimeManager());
            _unityContainer.RegisterType<ISessionBusinessLogic, SessionBusinessLogic>(new ContainerControlledLifetimeManager());

            _unityContainer.RegisterType<ITokenBusinessLogic, TokenBusinessLogic>(new ContainerControlledLifetimeManager());

            _unityContainer.RegisterType<IUserBusinessLogic, UserBusinessLogic>(new ContainerControlledLifetimeManager());
            _unityContainer.RegisterType<ILecturerBusinessLogic, LecturerBusinessLogic>(new ContainerControlledLifetimeManager());
            _unityContainer.RegisterType<IStudentBusinessLogic, StudentBusinessLogic>(new ContainerControlledLifetimeManager());
        }
    }
}
