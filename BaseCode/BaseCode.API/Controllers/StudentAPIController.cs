using AutoMapper;
using BaseCode.Data;
using BaseCode.Data.Models;
using BaseCode.Data.ViewModels;
using BaseCode.Data.ViewModels.Common;
using BaseCode.Domain;
using BaseCode.Domain.Contracts;
using BaseCode.Domain.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using Constants = BaseCode.Data.Constants;

namespace BaseCode.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StudentAPIController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;

        public StudentAPIController(IStudentService studentService, IMapper mapper)
        {
            _studentService = studentService;
            _mapper = mapper;
        }

        /// <summary>
        ///     This function retrieves a Student record.
        /// </summary>
        /// <param name="id">ID of the Student record</param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("student")]
        public HttpResponseMessage GetStudent(int id)
        {
            var student = _studentService.Find(id);
            return student != null ? Helper.ComposeResponse(HttpStatusCode.OK, student) : Helper.ComposeResponse(HttpStatusCode.NotFound, Constants.Student.StudentDoesNotExists);
        }

        /// <summary>
        ///     This function retrieves a list of Student records.
        /// </summary>
        /// <param name="searchModel">Search filters for finding Student records</param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("list")]
        public HttpResponseMessage GetStudentList([FromQuery] StudentSearchViewModel searchModel)
        {
            var responseData = _studentService.FindStudents(searchModel);
            return Helper.ComposeResponse(HttpStatusCode.OK, responseData);
        }

        /// <summary>
        ///     This function adds a Student record.
        /// </summary>
        /// <param name="studentModel">Contains Student properties</param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("add")]
        public HttpResponseMessage PostStudent(StudentViewModel studentModel)
        {
            if (!ModelState.IsValid) return Helper.ComposeResponse(HttpStatusCode.BadRequest, Helper.GetModelStateErrors(ModelState));

            try
            {
                var student = _mapper.Map<Student>(studentModel);
                var validationErrors = new StudentHandler(_studentService).CanAdd(student);
                var validationResults = validationErrors as IList<ValidationResult> ?? validationErrors.ToList();

                if (validationResults.Any())
                {
                    ModelState.AddModelErrors(validationResults);
                }

                if (ModelState.IsValid)
                {
                    var claimsIdentity = User.Identity as ClaimsIdentity;
                    if (claimsIdentity != null)
                    {
                        student.CreatedBy = claimsIdentity.Name;
                        student.CreatedDate = DateTime.Now;
                    }

                    _studentService.Create(student);
                    return Helper.ComposeResponse(HttpStatusCode.OK, Constants.Student.StudentSuccessAdd);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return Helper.ComposeResponse(HttpStatusCode.BadRequest, Helper.GetModelStateErrors(ModelState));
        }

        /// <summary>
        ///     This function updates a Student record.
        /// </summary>
        /// <param name="studentModel">Contains Student properties</param>
        /// <returns></returns>
        [HttpPut]
        [ActionName("edit")]        
        public HttpResponseMessage PutStudent(StudentViewModel studentModel)
        {
            if (!ModelState.IsValid) return Helper.ComposeResponse(HttpStatusCode.BadRequest, Helper.GetModelStateErrors(ModelState));
            try
            {
                var student = _mapper.Map<Student>(studentModel);
                var validationErrors = new StudentHandler(_studentService).CanUpdate(student);
                var validationResults = validationErrors as IList<ValidationResult> ?? validationErrors.ToList();

                if (validationResults.Any())
                {
                    ModelState.AddModelErrors(validationResults);
                }

                if (ModelState.IsValid)
                {
                    var claimsIdentity = User.Identity as ClaimsIdentity;
                    if (claimsIdentity != null)
                    {
                        student.ModifiedBy = claimsIdentity.Name;
                        student.ModifiedDate = DateTime.Now;
                    }

                    _studentService.Update(student);
                    return Helper.ComposeResponse(HttpStatusCode.NoContent, Constants.Student.StudentSuccessEdit);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return Helper.ComposeResponse(HttpStatusCode.BadRequest, Helper.GetModelStateErrors(ModelState));
        }

        /// <summary>
        ///     This function deletes a Student record.
        /// </summary>
        /// <param name="id">ID of the Student record</param>
        /// <returns></returns>
        [HttpDelete]
        [ActionName("delete")]
        public HttpResponseMessage DeleteStudent(int id)
        {
            try
            {
                var validationErrors = new StudentHandler(_studentService).CanDelete(id);

                var validationResults = validationErrors as IList<ValidationResult> ?? validationErrors.ToList();
                if (validationResults.Any())
                {
                    ModelState.AddModelErrors(validationResults);
                }

                if (ModelState.IsValid)
                {
                    _studentService.DeleteById(id);
                    return Helper.ComposeResponse(HttpStatusCode.OK, Constants.Student.StudentSuccessDelete);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return Helper.ComposeResponse(HttpStatusCode.BadRequest, Helper.GetModelStateErrors(ModelState));
        }
    }
}