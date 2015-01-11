using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using BB.DataLayer.Repositories;

namespace BB.DataLayer
{
	public class UnitOfWork : IDisposable
	{
		/// <summary>
        /// Used to denote whether a connection has been disposed of
        /// </summary>
        private bool _disposed;

        /// <summary>
        /// Connection to the database
        /// </summary>
        private readonly BeaconDatabaseContainer _applicationEntities;

		/// <summary>
        /// Base constructor that will create the database context/ connection
        /// </summary>
        public UnitOfWork()
        {
            _applicationEntities = new BeaconDatabaseContainer();
        }
		
		/// <summary>
        /// Saves any changes back to the database for objects that might have changed
        /// </summary>
        public void SaveChanges()
        {
            _applicationEntities.SaveChanges();
        }

        /// <summary>
        /// Used for implementing the IDisposable so the connection can be used in a 
        /// 'using' context
        /// </summary>
        protected virtual void DisposeContext()
        {
            if (!_disposed)
            {
                _applicationEntities.Dispose();
            }
            _disposed = true;
        }

        /// <summary>
        /// Used for disposing of the connection outside a 'using' context
        /// </summary>
        public void Dispose()
        {
            DisposeContext();
            GC.SuppressFinalize(this);
        }
		
		/// <summary>
        /// Repositories that allows access to their entity
        /// </summary>		
		private BeaconRepository _beaconRepository;
        public BeaconRepository BeaconRepository
        {
            get
            {
                if (_beaconRepository == null)
                {
                    _beaconRepository = new BeaconRepository(_applicationEntities);
                }
                return _beaconRepository;
            }
        }
				
		private CourseRepository _courseRepository;
        public CourseRepository CourseRepository
        {
            get
            {
                if (_courseRepository == null)
                {
                    _courseRepository = new CourseRepository(_applicationEntities);
                }
                return _courseRepository;
            }
        }
				
		private LecturerRepository _lecturerRepository;
        public LecturerRepository LecturerRepository
        {
            get
            {
                if (_lecturerRepository == null)
                {
                    _lecturerRepository = new LecturerRepository(_applicationEntities);
                }
                return _lecturerRepository;
            }
        }
				
		private LessonRepository _lessonRepository;
        public LessonRepository LessonRepository
        {
            get
            {
                if (_lessonRepository == null)
                {
                    _lessonRepository = new LessonRepository(_applicationEntities);
                }
                return _lessonRepository;
            }
        }
				
		private ResourceRepository _resourceRepository;
        public ResourceRepository ResourceRepository
        {
            get
            {
                if (_resourceRepository == null)
                {
                    _resourceRepository = new ResourceRepository(_applicationEntities);
                }
                return _resourceRepository;
            }
        }
				
		private ResourceTypeRepository _resourcetypeRepository;
        public ResourceTypeRepository ResourceTypeRepository
        {
            get
            {
                if (_resourcetypeRepository == null)
                {
                    _resourcetypeRepository = new ResourceTypeRepository(_applicationEntities);
                }
                return _resourcetypeRepository;
            }
        }
				
		private RoomRepository _roomRepository;
        public RoomRepository RoomRepository
        {
            get
            {
                if (_roomRepository == null)
                {
                    _roomRepository = new RoomRepository(_applicationEntities);
                }
                return _roomRepository;
            }
        }
				
		private SessionRepository _sessionRepository;
        public SessionRepository SessionRepository
        {
            get
            {
                if (_sessionRepository == null)
                {
                    _sessionRepository = new SessionRepository(_applicationEntities);
                }
                return _sessionRepository;
            }
        }
				
		private StudentRepository _studentRepository;
        public StudentRepository StudentRepository
        {
            get
            {
                if (_studentRepository == null)
                {
                    _studentRepository = new StudentRepository(_applicationEntities);
                }
                return _studentRepository;
            }
        }
				
		private UserRepository _userRepository;
        public UserRepository UserRepository
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new UserRepository(_applicationEntities);
                }
                return _userRepository;
            }
        }
			
	}	

	/// <summary>
    /// Various partial properties that allow the ability to add to via further partial functions
	/// later on
    /// </summary>
	public partial class BeaconRepository : GenericRepository<Beacon> 
	{
		public BeaconRepository(DbContext dbContextEntities) : base(dbContextEntities) { }
	}
	
	public partial class CourseRepository : GenericRepository<Course> 
	{
		public CourseRepository(DbContext dbContextEntities) : base(dbContextEntities) { }
	}
	
	public partial class LecturerRepository : GenericRepository<Lecturer> 
	{
		public LecturerRepository(DbContext dbContextEntities) : base(dbContextEntities) { }
	}
	
	public partial class LessonRepository : GenericRepository<Lesson> 
	{
		public LessonRepository(DbContext dbContextEntities) : base(dbContextEntities) { }
	}
	
	public partial class ResourceRepository : GenericRepository<Resource> 
	{
		public ResourceRepository(DbContext dbContextEntities) : base(dbContextEntities) { }
	}
	
	public partial class ResourceTypeRepository : GenericRepository<ResourceType> 
	{
		public ResourceTypeRepository(DbContext dbContextEntities) : base(dbContextEntities) { }
	}
	
	public partial class RoomRepository : GenericRepository<Room> 
	{
		public RoomRepository(DbContext dbContextEntities) : base(dbContextEntities) { }
	}
	
	public partial class SessionRepository : GenericRepository<Session> 
	{
		public SessionRepository(DbContext dbContextEntities) : base(dbContextEntities) { }
	}
	
	public partial class StudentRepository : GenericRepository<Student> 
	{
		public StudentRepository(DbContext dbContextEntities) : base(dbContextEntities) { }
	}
	
	public partial class UserRepository : GenericRepository<User> 
	{
		public UserRepository(DbContext dbContextEntities) : base(dbContextEntities) { }
	}
	
}
