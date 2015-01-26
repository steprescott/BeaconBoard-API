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
    /// Any requests made to the API for Student entities will be handled by the StudentsController
    /// </summary>
    public class StudentsController : BaseWithAuthController
    {
        /// <summary>
        /// Creates a new Student with the given details.
        /// </summary>
        /// <param name="Student">The details of the new Student.</param>
        /// <returns>HttpResponseMessage with correct status code and content for the result of the call.</returns>
        [HttpPost]
        public HttpResponseMessage Post([FromBody] Student Student)
        {
            //Create a new item with the given details
            var result = BeaconBoardService.StudentBusinessLogic.Create(Student);

            //If there was an error
            if (result == CRUDResult.Error)
            {
                //Return HttpResponseMessage with InternalServerError status code
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "An error occurred when creating a new Student.");
            }

            //Otherwise return with a status of OK
            return Request.CreateResponse(HttpStatusCode.OK, "Student created");
        }

        /// <summary>
        /// Updates the Student with the given details.
        /// </summary>
        /// <param name="Student">These are the details that the Student should be update with.</param>
        /// <returns>HttpResponseMessage with correct status code and content for the result of the call.</returns>
        [HttpPut]
        public HttpResponseMessage Put([FromBody] Student Student)
        {
            //Update the item that is in the database with the given details
            var result = BeaconBoardService.StudentBusinessLogic.Update(Student);

            //If there was an error
            if (result == CRUDResult.Error)
            {
                //Return HttpResponseMessage with InternalServerError status code
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "An error occurred when updating the Student with ID '" + Student.UserID + "'");
            }
            //If there isn't an item with the ID of the given item ID
            else if (result == CRUDResult.NotFound)
            {
                //Return HttpResponseMessage with NotFound status code
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Could not find a Student with ID of '" + Student.UserID + "' to update.");
            }

            //Otherwise return with a status of OK
            return Request.CreateResponse(HttpStatusCode.OK, "Student updated");
        }

        /// <summary>
        /// Gets all Students that are in the system.
        /// </summary>
        /// <returns>An array of Student DTOs that holds the details for the Students.</returns>
        [HttpGet]
        [ResponseType(typeof(List<StudentDTOModel>))]
        public HttpResponseMessage Get()
        {
            //Get back all the items
            var items = BeaconBoardService.StudentBusinessLogic.GetAll();

            var students = new List<StudentDTOModel>();

            foreach (Student student in items)
            {
                students.Add(new StudentDTOModel
                {
                    UserID = student.UserID,
                    Username = student.Username,
                    FirstName = student.FirstName,
                    OtherNames = student.OtherNames,
                    LastName = student.LastName,
                    EmailAddress = student.EmailAddress,
                    CourseIDs = student.CourseIDs
                });
            }

            //Return them via a HttpResponseMessage with OK
            return Request.CreateResponse(HttpStatusCode.OK, students);
        }

        /// <summary>
        /// Gets the Student with the given ID.
        /// </summary>
        /// <param name="id">The ID of the Student that the request is asking for.</param>
        /// <returns>Student DTO that holds the details for the Student.</returns>
        [HttpGet]
        [ResponseType(typeof(StudentDTOModel))]
        public HttpResponseMessage Get(Guid id)
        {
            //Get back the item details for the given ID
            var obj = BeaconBoardService.StudentBusinessLogic.GetByID(id);

            //If there wasn't an object with the given ID
            if (obj == null)
            {
                //Return HttpResponseMessage with NotFound status code
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No Student found");
            }

            var student = new StudentDTOModel
            {
                UserID = obj.UserID,
                Username = obj.Username,
                FirstName = obj.FirstName,
                OtherNames = obj.OtherNames,
                LastName = obj.LastName,
                EmailAddress = obj.EmailAddress,
                CourseIDs = obj.CourseIDs
            };

            //Otherwise return the object with a status of OK
            return Request.CreateResponse(HttpStatusCode.OK, student);
        }

        /// <summary>
        /// Deletes the Student from the database with the given ID.
        /// </summary>
        /// <param name="id">The ID of the Student to delete.</param>
        /// <returns>HttpResponseMessage with correct status code and content for the result of the call.</returns>
        [HttpDelete]
        public HttpResponseMessage Delete(Guid id)
        {
            //Delete the item from the database with the given ID
            var result = BeaconBoardService.StudentBusinessLogic.DeleteByID(id);

            //If there was an error
            if (result == CRUDResult.Error)
            {
                //Return HttpResponseMessage with InternalServerError status code
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "An error occurred when deleting the Student with ID '" + id + "'");
            }
            //If there isn't an item with the ID of the given item ID
            else if (result == CRUDResult.NotFound)
            {
                //Return HttpResponseMessage with NotFound status code
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Could not find a Student with ID of '" + id + "' to delete.");
            }

            //Otherwise return with a status of OK
            return Request.CreateResponse(HttpStatusCode.OK, "Student deleted");
        }
    }
}