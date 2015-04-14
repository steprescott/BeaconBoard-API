using BB.Domain;
using BB.Domain.Enums;
using BB.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
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

            var users = new List<UserDTOModel>();

            foreach (User user in items)
            {
                users.Add(new UserDTOModel
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
            return Request.CreateResponse(HttpStatusCode.OK, users);
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

            var users = new List<LecturerDTOModel>();

            foreach (Lecturer lecturer in items)
            {
                users.Add(new LecturerDTOModel
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
            return Request.CreateResponse(HttpStatusCode.OK, users);
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

            var users = new List<StudentDTOModel>();

            foreach (Student student in items)
            {
                users.Add(new StudentDTOModel
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
            return Request.CreateResponse(HttpStatusCode.OK, users);
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

            var user = new UserDTOModel
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
            return Request.CreateResponse(HttpStatusCode.OK, user);
        }

        /// <summary>
        /// In order to limit the amount of data needed to be synced to the device this endpoint
        /// gives the entry point that will allow only the data related to the active User to be sent.
        /// For example, this removes the need to download all the Courses in the system but only download
        /// the Courses related to the active User.
        /// 
        /// This end point gets the UserToken from the headers of the request so there is no need to any other parameters.
        /// </summary>
        /// <returns>User DTO that holds the details for the active User.</returns>
        [HttpGet]
        [Route("users/activeUser")]
        [ResponseType(typeof(UserDTOModel))]
        public HttpResponseMessage GetActiveUser()
        {
            //Get back the headers we are checking for
            var userTokenHeader = Request.Headers.SingleOrDefault(h => h.Key == ConfigurationManager.AppSettings["UserTokenHeader"]);

            if (userTokenHeader.Value != null)
            {
                //Get the User Token value from the header
                var userToken = Guid.Parse(userTokenHeader.Value.First());

                //Get back the item details for the token ID
                var obj = BeaconBoardService.UserBusinessLogic.GetUserForToken(userToken);

                //If there wasn't an object with the given token ID
                if (obj == null)
                {
                    //Return HttpResponseMessage with NotFound status code
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No User found with token.");
                }

                var user = new UserDTOModel
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
                return Request.CreateResponse(HttpStatusCode.OK, user);
            }

            //Return HttpResponseMessage with BadRequest status code
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "No User token sent.");
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

            var user = new LecturerDTOModel
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
            return Request.CreateResponse(HttpStatusCode.OK, user);
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

            var user = new StudentDTOModel
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
            return Request.CreateResponse(HttpStatusCode.OK, user);
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