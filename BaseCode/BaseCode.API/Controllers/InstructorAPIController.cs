using AutoMapper;
using BaseCode.Data;
using BaseCode.Data.Models;
using BaseCode.Data.ViewModels;
using BaseCode.Domain.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Security.Claims;
using BaseCode.Data.ViewModels.Common;
using BaseCode.Domain.Handlers;
using System.Collections;
using BaseCode.Domain;
using System.Linq;
using System.Collections.Generic;
using BaseCode.Domain.Services;

namespace BaseCode.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class InstructorAPIController : ControllerBase
    {
        private readonly IInstructorService _instructorService;
        private readonly IMapper _mapper;

        public InstructorAPIController(IInstructorService instructorService, IMapper mapper)
        {
            _instructorService = instructorService;
            _mapper = mapper;
        }

        [HttpGet]
        [ActionName("list")]
        public HttpResponseMessage GetInstructorList([FromQuery] InstructorSearchViewModel searchModel)
        {
            var responseData = _instructorService.FindInstructors(searchModel);
            return Helper.ComposeResponse(HttpStatusCode.OK, responseData);
        }

        [HttpGet]
        [ActionName("getInstructor")]
        public HttpResponseMessage GetInstructor(int Id)
        {
            var instructor = _instructorService.FindInstructor(Id);
            return instructor != null ? Helper.ComposeResponse(HttpStatusCode.OK, instructor) : Helper.ComposeResponse(HttpStatusCode.NotFound, Constants.Instructor.InstructorDoesNotExist);
        }

        [HttpPost]
        [ActionName("add")]
        public HttpResponseMessage PostInstructor(InstructorViewModel instructorModel)
        {

            if(!ModelState.IsValid) 
            { 
                return Helper.ComposeResponse(HttpStatusCode.BadRequest, Helper.GetModelStateErrors(ModelState));  
            }
                
            try
            {
                var instructor =  _mapper.Map<Instructor>(instructorModel);
                var validationErrors = new InstructorHandler(_instructorService).CanAdd(instructor);
                var validationResults = validationErrors as IList<ValidationResult> ?? validationErrors.ToList();

                if (validationResults.Any())
                {
                    ModelState.AddModelErrors(validationErrors);
                }

                if (ModelState.IsValid)
                {
                    _instructorService.Create(instructor);
                    return Helper.ComposeResponse(HttpStatusCode.OK, Constants.Instructor.InstructorSuccessAdd);

                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return Helper.ComposeResponse(HttpStatusCode.BadRequest, Helper.GetModelStateErrors(ModelState));
        }

        [HttpPut]
        [ActionName("edit")]
        public HttpResponseMessage PutInstructor(int Id, InstructorViewModel instructorModel)
        {
            if (!ModelState.IsValid) return Helper.ComposeResponse(HttpStatusCode.BadRequest, Helper.GetModelStateErrors(ModelState));

            try
            {
                var instructor = _mapper.Map<Instructor>(instructorModel);
                instructor.Id = Id;
                var validationErrors = new InstructorHandler(_instructorService).CanUpdate(instructor);
                var validationResults = validationErrors as IList<ValidationResult> ?? validationErrors.ToList();


                if (validationResults.Any())
                {
                    ModelState.AddModelErrors(validationErrors);
                }

                if (ModelState.IsValid)
                {
                    _instructorService.Update(instructor);
                    return Helper.ComposeResponse(HttpStatusCode.OK, Constants.Instructor.InstructorUpdateSuccess);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return Helper.ComposeResponse(HttpStatusCode.BadRequest, Helper.GetModelStateErrors(ModelState));
        }

        [HttpDelete]
        [ActionName("soft-delete")]
        public HttpResponseMessage SoftDeleteInstructor(int Id)
        {
            try
            {
                var validationErrors = new InstructorHandler(_instructorService).CanDelete(Id);
                var validationResults = validationErrors as IList<ValidationResult> ?? validationErrors.ToList();

                if (validationResults.Any())
                {
                    ModelState.AddModelErrors(validationErrors);
                }

                if (ModelState.IsValid)
                {
                    var subject = _instructorService.FindInstructor(Id);
                    _instructorService.SoftDelete(subject);

                    return Helper.ComposeResponse(HttpStatusCode.OK, Constants.Instructor.InstructorSuccessDelete);
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return Helper.ComposeResponse(HttpStatusCode.BadRequest, Helper.GetModelStateErrors(ModelState));
        }

        [HttpDelete]
        [ActionName("delete")]
        public HttpResponseMessage DeleteInstructor(int Id)
        {
            try
            {
                var validationErrors = new InstructorHandler(_instructorService).CanDelete(Id);
                var validationResults = validationErrors as IList<ValidationResult> ?? validationErrors.ToList();

                if (validationResults.Any())
                {
                    ModelState.AddModelErrors(validationErrors);
                }

                if (ModelState.IsValid)
                {
                    var subject = _instructorService.FindInstructor(Id);
                    _instructorService.Delete(subject);

                    return Helper.ComposeResponse(HttpStatusCode.OK, Constants.Instructor.InstructorSuccessDelete);
                }

            } catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return Helper.ComposeResponse(HttpStatusCode.BadRequest, Helper.GetModelStateErrors(ModelState));
        }
    }
}