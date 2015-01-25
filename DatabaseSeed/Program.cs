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

namespace DatabaseSeed
{
    class Program
    {
        static void Main(string[] args)
        {
            AutoMapperUtilities.RegisterMappings();

            Console.WriteLine("Database seeding started\n");

            //Console.WriteLine("--- Seeding lecturers");
            //var lecturer1 = 

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

            Console.WriteLine("--- Seeding lessons");
            var lesson1 = CreateOrUpdateLesson("75feec01-6cff-4f86-93fe-2d74f4e4995a", new List<Resource> { resource1, resource2 });

            Console.WriteLine("--- Seeding courses");
            var course1 = CreateOrUpdateCourse("cd3b9e14-c648-4501-a0f6-6ff7d878cc04", "MComp Software Engineering", new List<Lesson> { lesson1 });

            Console.WriteLine("--- Seeding sessions");
            var session1 = CreateOrUpdateSession("00fbf224-159b-4921-8d87-c2f3d3832afb", DateTime.Parse("25/01/2015 23:00"), DateTime.Parse("26/01/2015 01:00"), lesson1, room1);

            Console.WriteLine("\nDone");
            Console.ReadKey();
        }

        //static Room CreateOrUpdateStudent(String id, String firstName, String otherNames, String lastName, String emailAddress, String token)
        //{
        //    var businessLogic = BeaconBoardContainer.GetInstance<IUserBusinessLogic>();
        //    var obj = businessLogic.GetByID(Guid.Parse(id));

        //    if (obj == null)
        //    {
        //        obj = new Room
        //        {
        //            RoomID = Guid.Parse(id),
        //            Number = number
        //        };

        //        var result = businessLogic.Create(obj);

        //        if (result == CRUDResult.Created)
        //        {
        //            return obj;
        //        }
        //    }
        //    else
        //    {
        //        var result = businessLogic.Update(obj);

        //        if (result == CRUDResult.Updated)
        //        {
        //            return obj;
        //        }
        //    }

        //    Console.WriteLine("    Error occurred");
        //    return null;
        //}

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
                var result = businessLogic.Update(obj);

                if (result == CRUDResult.Updated)
                {
                    return obj;
                }
            }

            Console.WriteLine("    Error occurred");
            return null;
        }

        static Lesson CreateOrUpdateLesson(String id, List<Resource> resources)
        {
            var businessLogic = BeaconBoardContainer.GetInstance<ILessonBusinessLogic>();
            var obj = businessLogic.GetByID(Guid.Parse(id));

            if (obj == null)
            {
                obj = new Lesson
                {
                    LessonID = Guid.Parse(id),
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

        static Course CreateOrUpdateCourse(String id, String name, List<Lesson> lessons)
        {
            var businessLogic = BeaconBoardContainer.GetInstance<ICourseBusinessLogic>();
            var obj = businessLogic.GetByID(Guid.Parse(id));

            if (obj == null)
            {
                obj = new Course
                {
                    CourseID = Guid.Parse(id),
                    Name = name,
                    LessonIDs = lessons.Select(i => i.LessonID).ToList()
                };

                var result = businessLogic.Create(obj);

                if (result == CRUDResult.Created)
                {
                    return obj;
                }
            }
            else
            {
                var result = businessLogic.Update(obj);

                if (result == CRUDResult.Updated)
                {
                    return obj;
                }
            }

            Console.WriteLine("    Error occurred");
            return null;
        }

        static Session CreateOrUpdateSession(String id, DateTime scheduledStartDate, DateTime scheduledEndDate, Lesson lesson, Room room)
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
                var result = businessLogic.Update(obj);

                if (result == CRUDResult.Updated)
                {
                    return obj;
                }
            }

            Console.WriteLine("    Error occurred");
            return null;
        }
    }
}
