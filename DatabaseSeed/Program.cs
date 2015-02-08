using System;
using System.Collections.Generic;
using System.Linq;
using Beacon = BB.Domain.Beacon;
using Room = BB.Domain.Room;
using Lesson = BB.Domain.Lesson;
using Course = BB.Domain.Course;
using Session = BB.Domain.Session;
using ResourceType = BB.Domain.ResourceType;
using Resource = BB.Domain.Resource;
using BB.Container;
using BB.Interfaces;
using BB.Domain.Enums;
using BB.BusinessLogicEntityFramework.Utilities;
using BB.Domain;
using TH.EncryptionUtilities;

namespace DatabaseSeed
{
    class Program
    {
        static void Main(string[] args)
        {
            AutoMapperUtilities.RegisterMappings();

            Console.WriteLine("Database seeding started\n");

            Console.WriteLine("--- Seeding roles");
            var lectureRole = CreateOrUpdateRole("87e05b66-8800-4f61-a6f2-132b2006ea0a", "Lecturer");
            var studentRole = CreateOrUpdateRole("730237a9-8620-4d29-b005-ba7b6a02b769", "Student");

            Console.WriteLine("--- Seeding students");
            var student1 = CreateOrUpdateStudent("2715f8a0-4d09-410e-bc02-37982d0e4632", "student1", "password", "Jonny", null, "Booker", "ste@beaconboard.co.uk", "4e279f6a-b28a-4815-8555-51c667952abe", studentRole);
            var student2 = CreateOrUpdateStudent("5ac1333c-9eeb-48dc-a6dd-a6ceaa1ad821", "student2", "password", "Tom", "Markus", "Windowson", "tom@email.com", "752e098f-27b4-4d38-92d9-42afdc73f08f", studentRole);
            var student3 = CreateOrUpdateStudent("3cc3c708-2b37-44c7-8bc3-a29d79d97102", "student3", "password", "Joe", "Christopher Andrew", "Fletcher", "joe@email.com", "c88432ec-0f70-450b-999b-cfd7a968c205", studentRole);
            var student4 = CreateOrUpdateStudent("731a27a9-840b-49b6-bcfb-6303536c6b79", "steprescott", "password", "Ste", "Christopher", "Prescott", "jonny@email.com", "c0be1a6d-3d4f-4b23-b3ca-54162aeb2022", studentRole);
            
            Console.WriteLine("--- Seeding lecturer");
            var lecturer1 = CreateOrUpdateLecturer("39c05e8f-2c65-44c9-bfb7-bb92f480dbdf", "lecturer1", "password", "Bob", null, "Smith", "bob@email.com", "568472a2-8d0c-4c32-bb87-0ca8608de3a8", lectureRole);
            var lecturer2 = CreateOrUpdateLecturer("1e80abc5-d3e7-40c1-be6e-e907d0943918", "lecturer2", "password", "Jane", null, "Doe", "jane@email.com", "b96dab00-d711-4926-a87f-4a1a7ea7838a", lectureRole);

            Console.WriteLine("--- Seeding rooms");
            var room1 = CreateOrUpdateRoom("2eae5485-512d-43e3-9050-7c7b85445e81", "9101");
            var room2 = CreateOrUpdateRoom("67daf5e0-326e-4ef9-8e6e-a93a3f5bd764", "9130");

            Console.WriteLine("--- Seeding beacons");
            var beacon1 = CreateOrUpdateBeacon("8add217a-faa6-4fc4-9e8f-48e0c2c5702a", 1, 1, room1);
            var beacon2 = CreateOrUpdateBeacon("e9f09f7d-0622-4c1d-91dd-adef628b43f5", 1, 2, room1);
            var beacon3 = CreateOrUpdateBeacon("e8b738b4-5d9d-4e1e-8046-1350f16c48d9", 2, 1, room2);

            Console.WriteLine("--- Seeding resource types");
            var resourceTypePDF = CreateOrUpdateResourceType("6211158c-8c5a-4862-a1b6-bd2a0b686fa5", "PDF", "This is an open format that can be normally opened via a web browser.");
            var resourceTypeDoc = CreateOrUpdateResourceType("5f518272-99bc-46fc-a1ba-7fe78c8658ba", "Word document", "Standard word format for MS Word documents.");

            Console.WriteLine("--- Seeding resources");
            var resource1 = CreateOrUpdateResource("accd09fc-083d-4897-b4ed-04a6ea3f4217", "Test PDF", "This is to test if we can see resources.", "http://www.energy.umich.edu/sites/default/files/pdf-sample.pdf", resourceTypePDF);
            var resource2 = CreateOrUpdateResource("b9c222e0-7f47-4e22-9ed6-2e92daca7eca", "Test doc", "Another test to see if resources are returned correctly.", "http://homepages.inf.ed.ac.uk/neilb/TestWordDoc.doc", resourceTypeDoc);
            var resource3 = CreateOrUpdateResource("41502cea-05af-4078-bc97-bb3ed10cdb9b", "Introduction to Programming Through Game Development.", "This book teaches programming in a gaming context.", "http://www.andrews.edu/~greenley/cs2/IntroProgXNAGameStudio_eBook.pdf", resourceTypePDF);

            Console.WriteLine("--- Seeding courses");
            var course1 = CreateOrUpdateCourse("cd3b9e14-c648-4501-a0f6-6ff7d878cc04", "MComp Software Engineering", new List<Lecturer> { lecturer1, lecturer2 }, new List<Student> { student1, student2, student4 });
            var course2 = CreateOrUpdateCourse("2965c284-d6a9-4d7d-8217-8a91a14e5e0b", "BSC Games Development", new List<Lecturer> { lecturer1, lecturer2 }, new List<Student> { student3 });
            
            Console.WriteLine("--- Seeding modules");
            var module1 = CreateOrUpdateModule("74e2eae7-960c-4d96-8ce7-9f2495cdba33", "Cloud Applications", "Aims are to understand how to use computing power over the web.", 1, new List<Course> { course1 });
            var module2 = CreateOrUpdateModule("11ca8cc8-4967-4860-ae13-6a3d1f8e4d71", "Advance Software Engineering", "This module is taught across several courses.", 1, new List<Course> { course1 });
            var module3 = CreateOrUpdateModule("8bbbb3b8-f676-4225-a520-c7d8e1ee4c1c", "Games & Software Engineering", "To deepen students' knowledge and understanding of the context and current trends in software engineering.", 1, new List<Course> { course2 });

            Console.WriteLine("--- Seeding lessons");
            var lesson1 = CreateOrUpdateLesson("bcead141-95c3-4c79-9ed9-574dddf449db", "ERD and Normalization", new List<Resource> { resource1 });
            var lesson2 = CreateOrUpdateLesson("29177bb2-5802-4ae5-936b-647c2711ebfb", "Interface design", new List<Resource> { resource1 });
            var lesson3 = CreateOrUpdateLesson("9545345f-8342-499c-b1d4-321bfcc68979", "Project initiation, feasibility, planning", new List<Resource> { resource1 });
            var lesson4 = CreateOrUpdateLesson("8790f586-8f93-44f8-a32f-e417d5b79a23", "Open Shift", new List<Resource> { resource1 });
            var lesson5 = CreateOrUpdateLesson("62344945-d4be-42c2-8e0b-504dda3a642a", "Programming for Games", new List<Resource> { resource1, resource3 });

            Console.WriteLine("--- Seeding sessions");
            var session1 = CreateOrUpdateSession("00fbf224-159b-4921-8d87-c2f3d3832afb", DateTime.Now.AddDays(-1), DateTime.Now.AddDays(-1).AddHours(1), lesson1, room1, module2, new List<Lecturer> { lecturer1 });
            var session2 = CreateOrUpdateSession("8d79f5cb-814e-41e8-b0eb-f6396d4f75c2", DateTime.Now.AddDays(1), DateTime.Now.AddDays(1).AddHours(1), lesson1, room1, module2, new List<Lecturer> { lecturer1, lecturer2 });
            var session3 = CreateOrUpdateSession("9e7532b4-bcd8-4ea9-9412-e7a14d268498", DateTime.Now.AddDays(2), DateTime.Now.AddDays(2).AddHours(1), lesson2, room2, module2, new List<Lecturer> { lecturer1 });
            var session4 = CreateOrUpdateSession("28d96a39-ae13-4aba-aaa4-4b7c10deadd8", DateTime.Now.AddDays(9), DateTime.Now.AddDays(9).AddHours(1), lesson3, room1, module2, new List<Lecturer> { lecturer1 });
            var session5 = CreateOrUpdateSession("9d25ec6a-da69-4e01-a21a-1922af43e5fd", DateTime.Now.AddDays(16), DateTime.Now.AddDays(16).AddHours(1), lesson4, room1, module1, new List<Lecturer> { lecturer1 });
            var session6 = CreateOrUpdateSession("38194dc4-ef60-4655-bd78-bb6805306601", DateTime.Now.AddDays(23), DateTime.Now.AddDays(23).AddHours(1), lesson5, room1, module3, new List<Lecturer> { lecturer1 });
            
            Console.WriteLine("--- Seeding attendances");
            var attendance1 = CreateAttendance("1440006f-e593-4207-ba46-5fd7d6dabce6", student1, session1);
            var attendance2 = CreateAttendance("db0d4446-8950-4072-92ba-29468f31048f", student2, session1);
            var attendance3 = CreateAttendance("acc3640c-0fe9-460c-a636-20dc9193c24f", student4, session1);

            Console.WriteLine("\nDone");
            Console.ReadKey();
        }

        static Role CreateOrUpdateRole(String id, String name)
        {
            var businessLogic = BeaconBoardContainer.GetInstance<IRoleBusinessLogic>();

            var obj = businessLogic.GetByID(Guid.Parse(id));

            if (obj == null)
            {
                obj = new Role
                {
                    RoleID = Guid.Parse(id),
                    Name = name
                };

                var result = businessLogic.Create(obj);

                if (result == CRUDResult.Created)
                {
                    return obj;
                }
            }
            else
            {
                obj.RoleID = Guid.Parse(id);
                obj.Name = name;

                var result = businessLogic.Update(obj);

                if (result == CRUDResult.Updated)
                {
                    return obj;
                }
            }

            Console.WriteLine("    Error occurred");
            return null;
        }

        static Student CreateOrUpdateStudent(String id, String username, String password, String firstName, String otherNames, String lastName, String emailAddress, String token, Role role)
        {
            var businessLogic = BeaconBoardContainer.GetInstance<IStudentBusinessLogic>();

            var obj = businessLogic.GetByID(Guid.Parse(id));

            if (obj == null)
            {
                obj = new Student
                {
                    UserID = Guid.Parse(id),
                    Username = username,
                    Password = BasicEncryptDecryptUtilities.Encrypt(password),
                    FirstName = firstName,
                    OtherNames = otherNames,
                    LastName = lastName,
                    EmailAddress = emailAddress,
                    Token = Guid.Parse(token),
                    RoleID = role.RoleID,
                };

                var result = businessLogic.Create(obj);

                if (result == CRUDResult.Created)
                {
                    return obj;
                }
            }
            else
            {
                obj.UserID = Guid.Parse(id);
                obj.Username = username;
                obj.Password = BasicEncryptDecryptUtilities.Encrypt(password);
                obj.FirstName = firstName;
                obj.OtherNames = otherNames;
                obj.LastName = lastName;
                obj.EmailAddress = emailAddress;
                obj.Token = Guid.Parse(token);
                obj.RoleID = role.RoleID;

                var result = businessLogic.Update(obj);

                if (result == CRUDResult.Updated)
                {
                    return obj;
                }
            }

            Console.WriteLine("    Error occurred");
            return null;
        }

        static Lecturer CreateOrUpdateLecturer(String id, String username, String password, String firstName, String otherNames, String lastName, String emailAddress, String token, Role role)
        {
            var businessLogic = BeaconBoardContainer.GetInstance<ILecturerBusinessLogic>();

            var obj = businessLogic.GetByID(Guid.Parse(id));

            if (obj == null)
            {
                obj = new Lecturer
                {
                    UserID = Guid.Parse(id),
                    Username = username,
                    Password = BasicEncryptDecryptUtilities.Encrypt(password),
                    FirstName = firstName,
                    OtherNames = otherNames,
                    LastName = lastName,
                    EmailAddress = emailAddress,
                    Token = Guid.Parse(token),
                    RoleID = role.RoleID
                };

                var result = businessLogic.Create(obj);

                if (result == CRUDResult.Created)
                {
                    return obj;
                }
            }
            else
            {
                obj.UserID = Guid.Parse(id);
                obj.Username = username;
                obj.Password = BasicEncryptDecryptUtilities.Encrypt(password);
                obj.FirstName = firstName;
                obj.OtherNames = otherNames;
                obj.LastName = lastName;
                obj.EmailAddress = emailAddress;
                obj.Token = Guid.Parse(token);
                obj.RoleID = role.RoleID;

                var result = businessLogic.Update(obj);

                if (result == CRUDResult.Updated)
                {
                    return obj;
                }
            }

            Console.WriteLine("    Error occurred");
            return null;
        }

        static Room CreateOrUpdateRoom(String id, String number)
        {
            var businessLogic = BeaconBoardContainer.GetInstance<IRoomBusinessLogic>();
            var obj = businessLogic.GetByID(Guid.Parse(id));

            if(obj == null)
            {
                obj = new Room
                {
                    RoomID = Guid.Parse(id),
                    Number = number
                };

                var result = businessLogic.Create(obj);

                if (result == CRUDResult.Created)
                {
                    return obj;
                }
            }
            else
            {
                obj.RoomID = Guid.Parse(id);
                obj.Number = number;

                var result = businessLogic.Update(obj);

                if (result == CRUDResult.Updated)
                {
                    return obj;
                }
            }

            Console.WriteLine("    Error occurred");
            return null;
        }

        static Beacon CreateOrUpdateBeacon(String id, Int32 major, Int32 minor, Room room)
        {
            var businessLogic = BeaconBoardContainer.GetInstance<IBeaconBusinessLogic>();
            var obj = businessLogic.GetByID(Guid.Parse(id));

            if (obj == null)
            {
                obj = new Beacon
                {
                    BeaconID = Guid.Parse(id),
                    Major = major,
                    Minor = minor,
                    RoomID = room.RoomID
                };

                var result = businessLogic.Create(obj);

                if (result == CRUDResult.Created)
                {
                    return obj;
                }
            }
            else
            {
                obj.BeaconID = Guid.Parse(id);
                obj.Major = major;
                obj.Minor = minor;
                obj.RoomID = room.RoomID;

                var result = businessLogic.Update(obj);

                if (result == CRUDResult.Updated)
                {
                    return obj;
                }
            }

            Console.WriteLine("    Error occurred");
            return null;
        }

        static ResourceType CreateOrUpdateResourceType(String id, String name, String description)
        {
            var businessLogic = BeaconBoardContainer.GetInstance<IResourceTypeBusinessLogic>();
            var obj = businessLogic.GetByID(Guid.Parse(id));

            if (obj == null)
            {
                obj = new ResourceType
                {
                    ResourceTypeID = Guid.Parse(id),
                    Name = name,
                    Description = description
                };

                var result = businessLogic.Create(obj);

                if (result == CRUDResult.Created)
                {
                    return obj;
                }
            }
            else
            {
                obj.ResourceTypeID = Guid.Parse(id);
                obj.Name = name;
                obj.Description = description;

                var result = businessLogic.Update(obj);

                if (result == CRUDResult.Updated)
                {
                    return obj;
                }
            }

            Console.WriteLine("    Error occurred");
            return null;
        }

        static Resource CreateOrUpdateResource(String id, String name, String description, String URLString, ResourceType resourceType)
        {
            var businessLogic = BeaconBoardContainer.GetInstance<IResourceBusinessLogic>();
            var obj = businessLogic.GetByID(Guid.Parse(id));

            if (obj == null)
            {
                obj = new Resource
                {
                    ResourceID = Guid.Parse(id),
                    Name = name,
                    Description = description,
                    URLString = URLString,
                    ResourceTypeID = resourceType.ResourceTypeID
                };

                var result = businessLogic.Create(obj);

                if (result == CRUDResult.Created)
                {
                    return obj;
                }
            }
            else
            {
                obj.ResourceID = Guid.Parse(id);
                obj.Name = name;
                obj.Description = description;
                obj.URLString = URLString;
                obj.ResourceTypeID = resourceType.ResourceTypeID;

                var result = businessLogic.Update(obj);

                if (result == CRUDResult.Updated)
                {
                    return obj;
                }
            }

            Console.WriteLine("    Error occurred");
            return null;
        }

        static Course CreateOrUpdateCourse(String id, String name, List<Lecturer> lecturers, List<Student> students)
        {
            var businessLogic = BeaconBoardContainer.GetInstance<ICourseBusinessLogic>();
            var obj = businessLogic.GetByID(Guid.Parse(id));

            if (obj == null)
            {
                obj = new Course
                {
                    CourseID = Guid.Parse(id),
                    Name = name,
                    LecturerIDs = lecturers.Select(i => i.UserID).ToList(),
                    StudentIDs = students.Select(i => i.UserID).ToList(),
                };

                var result = businessLogic.Create(obj);

                if (result == CRUDResult.Created)
                {
                    return obj;
                }
            }
            else
            {
                obj.CourseID = Guid.Parse(id);
                obj.Name = name;
                obj.LecturerIDs = lecturers.Select(i => i.UserID).ToList();
                obj.StudentIDs = students.Select(i => i.UserID).ToList();

                var result = businessLogic.Update(obj);

                if (result == CRUDResult.Updated)
                {
                    return obj;
                }
            }

            Console.WriteLine("    Error occurred");
            return null;
        }

        static Module CreateOrUpdateModule(String id, String name, String description, int termNumber, List<Course> courses)
        {
            var businessLogic = BeaconBoardContainer.GetInstance<IModuleBusinessLogic>();
            var obj = businessLogic.GetByID(Guid.Parse(id));

            if (obj == null)
            {
                obj = new Module
                {
                    ModuleID = Guid.Parse(id),
                    Name = name,
                    Description = description,
                    TermNumber = termNumber,
                    CourseIDs = courses.Select(i => i.CourseID).ToList()
                };

                var result = businessLogic.Create(obj);

                if (result == CRUDResult.Created)
                {
                    return obj;
                }
            }
            else
            {
                obj.ModuleID = Guid.Parse(id);
                obj.Name = name;
                obj.Description = description;
                obj.TermNumber = termNumber;
                obj.CourseIDs = courses.Select(i => i.CourseID).ToList();

                var result = businessLogic.Update(obj);

                if (result == CRUDResult.Updated)
                {
                    return obj;
                }
            }

            Console.WriteLine("    Error occurred");
            return null;
        }

        static Lesson CreateOrUpdateLesson(String id, String name, List<Resource> resources)
        {
            var businessLogic = BeaconBoardContainer.GetInstance<ILessonBusinessLogic>();
            var obj = businessLogic.GetByID(Guid.Parse(id));

            if (obj == null)
            {
                obj = new Lesson
                {
                    LessonID = Guid.Parse(id),
                    Name = name,
                    ResourceIDs = resources.Select(i => i.ResourceID).ToList()
                };

                var result = businessLogic.Create(obj);

                if (result == CRUDResult.Created)
                {
                    return obj;
                }
            }
            else
            {
                obj.LessonID = Guid.Parse(id);
                obj.Name = name;
                obj.ResourceIDs = resources.Select(i => i.ResourceID).ToList();

                var result = businessLogic.Update(obj);

                if (result == CRUDResult.Updated)
                {
                    return obj;
                }
            }

            Console.WriteLine("    Error occurred");
            return null;
        }

        static Session CreateOrUpdateSession(String id, DateTime scheduledStartDate, DateTime scheduledEndDate, Lesson lesson, Room room, Module module, List<Lecturer> lecturers)
        {
            var businessLogic = BeaconBoardContainer.GetInstance<ISessionBusinessLogic>();
            var obj = businessLogic.GetByID(Guid.Parse(id));

            if (obj == null)
            {
                obj = new Session
                {
                    SessionID = Guid.Parse(id),
                    ScheduledStartDate = scheduledStartDate,
                    ScheduledEndDate = scheduledEndDate,
                    LessonID = lesson.LessonID,
                    RoomID = room.RoomID,
                    ModuleID = module.ModuleID,
                    LecturerIDs = lecturers.Select(i => i.UserID).ToList()
                };

                var result = businessLogic.Create(obj);

                if (result == CRUDResult.Created)
                {
                    return obj;
                }
            }
            else
            {
                obj.SessionID = Guid.Parse(id);
                obj.ScheduledStartDate = scheduledStartDate;
                obj.ScheduledEndDate = scheduledEndDate;
                obj.LessonID = lesson.LessonID;
                obj.RoomID = room.RoomID;
                obj.ModuleID = module.ModuleID;
                obj.LecturerIDs = lecturers.Select(i => i.UserID).ToList();

                var result = businessLogic.Update(obj);

                if (result == CRUDResult.Updated)
                {
                    return obj;
                }
            }

            Console.WriteLine("    Error occurred");
            return null;
        }

        static Attendance CreateAttendance(String id, Student student, Session session)
        {
            var businessLogic = BeaconBoardContainer.GetInstance<IAttendanceBusinessLogic>();
            var obj = businessLogic.GetByID(Guid.Parse(id));

            if (obj == null)
            {
                obj = new Attendance
                {
                    AttendanceID = Guid.Parse(id),
                    StudentID = student.UserID,
                    SessionID = session.SessionID
                };

                var result = businessLogic.Create(obj);

                if (result == CRUDResult.Error)
                {
                    Console.WriteLine("    Error occurred");
                    return null;
                }

                return obj;
            }

            return null;
        }
    }
}
