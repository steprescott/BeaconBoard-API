using BB.Domain;
using BB.Domain.Enums;
using BB.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace BB.WebApi.Controllers
{
    /// <summary>
    /// Any requests made to the API for User entities will be handled by the UsersController
    /// </summary>
    public class UsersController : BaseWithAuthController
    {
        /// <summary>
        /// Gets all Users that are in the system.
        /// </summary>
        /// <returns>An array of User DTOs that holds the details for the Users.</returns>
        [HttpGet]
        [ResponseType(typeof(List<UserDTOModel>))]
        public HttpResponseMessage Get()
        {
            //Get back all the items
            var items = BeaconBoardService.UserBusinessLogic.GetAll();

            var Users = new List<UserDTOModel>();

            foreach (User user in items)
            {
                Users.Add(new UserDTOModel
                {
                    UserID = user.UserID,
                    Username = user.Username,
                    FirstName = user.FirstName,
                    OtherNames = user.OtherNames,
                    LastName = user.LastName,
                    EmailAddress = user.EmailAddress,
                    RoleID = user.RoleID
                });
            }

            //Return them via a HttpResponseMessage with OK
            return Request.CreateResponse(HttpStatusCode.OK, Users);
        }

        /// <summary>
        /// Gets all Lecturers that are in the system.
        /// </summary>
        /// <returns>An array of Lecturers DTOs that holds the details for the Lecturers.</returns>
        [HttpGet]
        [Route("users/lectures")]
        [ResponseType(typeof(List<LecturerDTOModel>))]
        public HttpResponseMessage GetLecturers()
        {
            //Get back all the items
            var items = BeaconBoardService.LecturerBusinessLogic.GetAll();

            var Users = new List<LecturerDTOModel>();

            foreach (Lecturer lecturer in items)
            {
                Users.Add(new LecturerDTOModel
                {
                    UserID = lecturer.UserID,
                    Username = lecturer.Username,
                    FirstName = lecturer.FirstName,
                    OtherNames = lecturer.OtherNames,
                    LastName = lecturer.LastName,
                    EmailAddress = lecturer.EmailAddress,
                    RoleID = lecturer.RoleID,
                    CourseIDs = lecturer.CourseIDs,
                    SessionIDs = lecturer.SessionIDs
                });
            }

            //Return them via a HttpResponseMessage with OK
            return Request.CreateResponse(HttpStatusCode.OK, Users);
        }

        /// <summary>
        /// Gets all Students that are in the system.
        /// </summary>
        /// <returns>An array of Student DTOs that holds the details for the Student.</returns>
        [HttpGet]
        [Route("users/students")]
        [ResponseType(typeof(List<StudentDTOModel>))]
        public HttpResponseMessage GetStudents()
        {
            //Get back all the items
            var items = BeaconBoardService.StudentBusinessLogic.GetAll();

            var Users = new List<StudentDTOModel>();

            foreach (Student student in items)
            {
                Users.Add(new StudentDTOModel
                {
                    UserID = student.UserID,
                    Username = student.Username,
                    FirstName = student.FirstName,
                    OtherNames = student.OtherNames,
                    LastName = student.LastName,
                    EmailAddress = student.EmailAddress,
                    RoleID = student.RoleID,
                    CourseIDs = student.CourseIDs
                });
            }

            //Return them via a HttpResponseMessage with OK
            return Request.CreateResponse(HttpStatusCode.OK, Users);
        }

        /// <summary>
        /// Gets the User with the given ID.
        /// </summary>
        /// <param name="id">The ID of the User that the request is asking for.</param>
        /// <returns>User DTO that holds the details for the User.</returns>
        [HttpGet]
        [ResponseType(typeof(UserDTOModel))]
        public HttpResponseMessage Get(Guid id)
        {
            //Get back the item details for the given ID
            var obj = BeaconBoardService.UserBusinessLogic.GetByID(id);

            //If there wasn't an object with the given ID
            if (obj == null)
            {
                //Return HttpResponseMessage with NotFound status code
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No User found");
            }

            var User = new UserDTOModel
            {
                UserID = obj.UserID,
                Username = obj.Username,
                FirstName = obj.FirstName,
                OtherNames = obj.OtherNames,
                LastName = obj.LastName,
                EmailAddress = obj.EmailAddress,
                RoleID = obj.RoleID
            };

            //Otherwise return the object with a status of OK
            return Request.CreateResponse(HttpStatusCode.OK, User);
        }

        /// <summary>
        /// Gets the Lecturer with the given ID.
        /// </summary>
        /// <param name="id">The ID of the Lecturer that the request is asking for.</param>
        /// <returns>Lecturer DTO that holds the details for the Lecturer.</returns>
        [HttpGet]
        [Route("users/lectures/{id}")]
        [ResponseType(typeof(LecturerDTOModel))]
        public HttpResponseMessage GetLecturer(Guid id)
        {
            //Get back the item details for the given ID
            var obj = BeaconBoardService.LecturerBusinessLogic.GetByID(id);

            //If there wasn't an object with the given ID
            if (obj == null)
            {
                //Return HttpResponseMessage with NotFound status code
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No Lecturer found");
            }

            var User = new LecturerDTOModel
            {
                UserID = obj.UserID,
                Username = obj.Username,
                FirstName = obj.FirstName,
                OtherNames = obj.OtherNames,
                LastName = obj.LastName,
                EmailAddress = obj.EmailAddress,
                RoleID = obj.RoleID,
                CourseIDs = obj.CourseIDs,
                SessionIDs = obj.SessionIDs
            };

            //Otherwise return the object with a status of OK
            return Request.CreateResponse(HttpStatusCode.OK, User);
        }

        /// <summary>
        /// Gets the Student with the given ID.
        /// </summary>
        /// <param name="id">The ID of the Student that the request is asking for.</param>
        /// <returns>Student DTO that holds the details for the Student.</returns>
        [HttpGet]
        [Route("users/students/{id}")]
        [ResponseType(typeof(StudentDTOModel))]
        public HttpResponseMessage GetStudent(Guid id)
        {
            //Get back the item details for the given ID
            var obj = BeaconBoardService.StudentBusinessLogic.GetByID(id);

            //If there wasn't an object with the given ID
            if (obj == null)
            {
                //Return HttpResponseMessage with NotFound status code
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No Student found");
            }

            var User = new StudentDTOModel
            {
                UserID = obj.UserID,
                Username = obj.Username,
                FirstName = obj.FirstName,
                OtherNames = obj.OtherNames,
                LastName = obj.LastName,
                EmailAddress = obj.EmailAddress,
                RoleID = obj.RoleID,
                CourseIDs = obj.CourseIDs
            };

            //Otherwise return the object with a status of OK
            return Request.CreateResponse(HttpStatusCode.OK, User);
        }

        /// <summary>
        /// Deletes the User from the database with the given ID.
        /// Only Lecturers can call this action.
        /// </summary>
        /// <param name="id">The ID of the User to delete.</param>
        /// <returns>HttpResponseMessage with correct status code and content for the result of the call.</returns>
        [HttpDelete]
        [Authorize(Roles = "Lecturer")]
        public HttpResponseMessage Delete(Guid id)
        {
            //Delete the item from the database with the given ID
            var result = BeaconBoardService.UserBusinessLogic.DeleteByID(id);

            //If there was an error
            if (result == CRUDResult.Error)
            {
                //Return HttpResponseMessage with InternalServerError status code
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "An error occurred when deleting the User with ID '" + id + "'");
            }
            //If there isn't an item with the ID of the given item ID
            else if (result == CRUDResult.NotFound)
            {
                //Return HttpResponseMessage with NotFound status code
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Could not find a User with ID of '" + id + "' to delete.");
            }

            //Otherwise return with a status of OK
            return Request.CreateResponse(HttpStatusCode.OK, "User deleted");
        }
    }
}