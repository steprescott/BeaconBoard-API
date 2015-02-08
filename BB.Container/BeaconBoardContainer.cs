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

            _unityContainer.RegisterType<IUnitOfWork, UnitOfWorkEntityFrameworkImplementation>(new HierarchicalLifetimeManager());

            _unityContainer.RegisterType<IAttendanceBusinessLogic, AttendanceBusinessLogic>();
            _unityContainer.RegisterType<IBeaconBusinessLogic, BeaconBusinessLogic>();
            _unityContainer.RegisterType<ICourseBusinessLogic, CourseBusinessLogic>();
            _unityContainer.RegisterType<ILessonBusinessLogic, LessonBusinessLogic>();
            _unityContainer.RegisterType<IModuleBusinessLogic, ModuleBusinessLogic>();
            _unityContainer.RegisterType<IResourceBusinessLogic, ResourceBusinessLogic>();
            _unityContainer.RegisterType<IResourceTypeBusinessLogic, ResourceTypeBusinessLogic>();
            _unityContainer.RegisterType<IRoleBusinessLogic, RoleBusinessLogic>();
            _unityContainer.RegisterType<IRoomBusinessLogic, RoomBusinessLogic>();
            _unityContainer.RegisterType<ISessionBusinessLogic, SessionBusinessLogic>();

            _unityContainer.RegisterType<ITokenBusinessLogic, TokenBusinessLogic>();

            _unityContainer.RegisterType<IUserBusinessLogic, UserBusinessLogic>();
            _unityContainer.RegisterType<ILecturerBusinessLogic, LecturerBusinessLogic>();
            _unityContainer.RegisterType<IStudentBusinessLogic, StudentBusinessLogic>();
        }
    }
}
