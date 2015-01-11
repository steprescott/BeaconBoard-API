using System;
using System.Collections.Generic;
using System.Linq;
using BB.DataLayer;
using Beacon = BB.Domain.Beacon;
using Room = BB.Domain.Room;
using Lesson = BB.Domain.Lesson;
using Course = BB.Domain.Course;
using Session = BB.Domain.Session;
using ResourceType = BB.Domain.ResourceType;
using Resource = BB.Domain.Resource;

namespace DatabaseSeed
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Database seeding started\n");
            Console.WriteLine("--- Seeding rooms");
            var room1 = InsertRoom("2eae5485-512d-43e3-9050-7c7b85445e81", "9101");
            var room2 = InsertRoom("67daf5e0-326e-4ef9-8e6e-a93a3f5bd764", "9130");

            Console.WriteLine("--- Seeding beacons");
            var beacon1 = InsertBeacon("8add217a-faa6-4fc4-9e8f-48e0c2c5702a", 1, 1, room1);
            var beacon2 = InsertBeacon("e9f09f7d-0622-4c1d-91dd-adef628b43f5", 1, 2, room1);
            var beacon3 = InsertBeacon("e8b738b4-5d9d-4e1e-8046-1350f16c48d9", 2, 1, room2);

            Console.WriteLine("--- Seeding resource types");
            var resourceTypePDF = InsertResourceType("6211158c-8c5a-4862-a1b6-bd2a0b686fa5", "PDF", "This is an open format that can be normally opened via a web browser.");
            var resourceTypeDoc = InsertResourceType("5f518272-99bc-46fc-a1ba-7fe78c8658ba", "Word document", "Standard word format for MS Word documents.");

            Console.WriteLine("--- Seeding resources");
            var resorce1 = InsertResource("accd09fc-083d-4897-b4ed-04a6ea3f4217", "Test PDF", "This is to test if we can see resources.", "http://www.energy.umich.edu/sites/default/files/pdf-sample.pdf", resourceTypePDF);
            var resorce2 = InsertResource("b9c222e0-7f47-4e22-9ed6-2e92daca7eca", "Test doc", "Another test to see if resources are returned correctly.", "http://homepages.inf.ed.ac.uk/neilb/TestWordDoc.doc", resourceTypePDF);

            Console.WriteLine("--- Seeding lessons");
            var lesson1 = InsertLesson("75feec01-6cff-4f86-93fe-2d74f4e4995a");

            Console.WriteLine("--- Seeding courses");
            var course1 = InsertCourse("cd3b9e14-c648-4501-a0f6-6ff7d878cc04", "MComp Software Engineering", new List<Lesson>{ lesson1 });

            Console.WriteLine("--- Seeding sessions");
            var session1 = InsertSession("00fbf224-159b-4921-8d87-c2f3d3832afb", DateTime.Parse("10/01/2015"), lesson1, room1);

            Console.WriteLine("\nDone");
            Console.ReadKey();
        }

        static Room InsertRoom(String id, String number)
        {
            var repo = new UnitOfWork().RoomRepository;

            var obj = new Room
            {
                RoomID = Guid.Parse(id),
                Number = number
            };

            var result = repo.CreateOrUpdate(obj);

            if (result)
            {
                return obj;
            }

            Console.WriteLine("    Error occurred");
            return null;
        }

        static Beacon InsertBeacon(String id, Int32 major, Int32 minor, Room room)
        {
            var repo = new UnitOfWork().BeaconRepository;

            var obj = new Beacon
            {
                BeaconID = Guid.Parse(id),
                Major = major,
                Minor = minor,
                RoomID = room.RoomID
            };

            var result = repo.CreateOrUpdate(obj);

            if(result)
            {
                return obj;
            }

            Console.WriteLine("    Error occurred");
            return null;
        }

        static Lesson InsertLesson(String id)
        {
            var repo = new UnitOfWork().LessonRepository;

            var obj = new Lesson
            {
                LessonID= Guid.Parse(id)
            };

            var result = repo.CreateOrUpdate(obj);

            if (result)
            {
                return obj;
            }

            Console.WriteLine("    Error occurred");
            return null;
        }

        static Course InsertCourse(String id, String name, List<Lesson>lessons)
        {
            var repo = new UnitOfWork().CourseRepository;

            var obj = new Course
            {
                CourseID = Guid.Parse(id),
                Name = name,
                LessonIDs = lessons.Select(i => i.LessonID).ToList()
            };

            var result = repo.CreateOrUpdate(obj);

            if (result)
            {
                return obj;
            }

            Console.WriteLine("    Error occurred");
            return null;
        }

        static Session InsertSession(String id, DateTime scheduledDate, Lesson lesson, Room room)
        {
            var repo = new UnitOfWork().SessionRepository;

            var obj = new Session
            {
                SessionID = Guid.Parse(id),
                ScheduledDate = scheduledDate,
                LessonID = lesson.LessonID,
                RoomID = room.RoomID
            };

            var result = repo.CreateOrUpdate(obj);

            if (result)
            {
                return obj;
            }

            Console.WriteLine("    Error occurred");
            return null;
        }

        static ResourceType InsertResourceType(String id, String name, String description)
        {
            var repo = new UnitOfWork().ResourceTypeRepository;

            var obj = new ResourceType
            {
                ResourceTypeID = Guid.Parse(id),
                Name = name,
                Description = description
            };

            var result = repo.CreateOrUpdate(obj);

            if (result)
            {
                return obj;
            }

            Console.WriteLine("    Error occurred");
            return null;
        }

        static Resource InsertResource(String id, String name, String description, String URLString, ResourceType resourceType)
        {
            var repo = new UnitOfWork().ResourceRepository;

            var obj = new Resource
            {
                ResourceID = Guid.Parse(id),
                Name = name,
                Description = description,
                URLString = URLString,
                ResourceTypeID = resourceType.ResourceTypeID
            };

            var result = repo.CreateOrUpdate(obj);

            if (result)
            {
                return obj;
            }

            Console.WriteLine("    Error occurred");
            return null;
        }
    }
}
