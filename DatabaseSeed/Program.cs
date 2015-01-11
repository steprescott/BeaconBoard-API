﻿using System;
using System.Collections.Generic;
using System.Linq;
using BB.DataLayer;
using Beacon = BB.Domain.Beacon;
using Room = BB.Domain.Room;
using Lesson = BB.Domain.Lesson;
using Course = BB.Domain.Course;
using Session = BB.Domain.Session;

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
    }
}
