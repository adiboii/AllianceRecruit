using AutoMapper;
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

namespace BaseCode.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SubjectAPIController : ControllerBase
    {
        private readonly ISubjectService _subjectService;
        private readonly IMapper _mapper;

        public SubjectAPIController(ISubjectService subjectService, IMapper mapper)
        {
            _subjectService = subjectService;
            _mapper = mapper;
        }

        [HttpGet]
        [ActionName("list")]
        public HttpResponseMessage GetSubjectList([FromQuery] SubjectSearchViewModel searchModel)
        {
            var responseData = _subjectService.FindSubjects(searchModel);
            return Helper.ComposeResponse(HttpStatusCode.OK, responseData);
        }

        [HttpGet]
        [ActionName("getSubject")]
        public HttpResponseMessage GetSubject(int Id)
        {
            var subject = _subjectService.FindSubject(Id);
            return subject != null ? Helper.ComposeResponse(HttpStatusCode.OK, subject) : Helper.ComposeResponse(HttpStatusCode.NotFound, Constants.Subject.SubjectDoesNotExist);
        }

        [HttpPost]
        [ActionName("add")]
        public HttpResponseMessage PostSubject(SubjectViewModel subjectModel)
        {

            if(!ModelState.IsValid) 
            { 
                return Helper.ComposeResponse(HttpStatusCode.BadRequest, Helper.GetModelStateErrors(ModelState));  
            }
                
            try
            {
                var subject =  _mapper.Map<Subject>(subjectModel);
                var validationErrors = new SubjectHandler(_subjectService).CanAdd(subject);
                var validationResults = validationErrors as IList<ValidationResult> ?? validationErrors.ToList();

                if(validationResults.Any())
                {
                    ModelState.AddModelErrors(validationErrors);
                }

                if(ModelState.IsValid)
                {
                    _subjectService.Create(subject);
                    return Helper.ComposeResponse(HttpStatusCode.OK, Constants.Subject.SubjectSuccessAdd);

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
        public HttpResponseMessage PutSubject(int Id, SubjectViewModel subjectModel)
        {
            if (!ModelState.IsValid) return Helper.ComposeResponse(HttpStatusCode.BadRequest, Helper.GetModelStateErrors(ModelState));

            try
            {
                var subject = _mapper.Map<Subject>(subjectModel);
                subject.Id = Id;
                var validationErrors = new SubjectHandler(_subjectService).CanUpdate(subject);
                var validationResults = validationErrors as IList<ValidationResult> ?? validationErrors.ToList();

                if (validationResults.Any())
                {
                    ModelState.AddModelErrors(validationErrors);
                }

                if (ModelState.IsValid)
                {
                    _subjectService.Update(subject);
                    return Helper.ComposeResponse(HttpStatusCode.OK, Constants.Subject.SubjectUpdateSuccess);
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
        public HttpResponseMessage SoftDeleteSubject(int Id)
        {
            try
            {
                var validationErrors = new SubjectHandler(_subjectService).CanDelete(Id);
                var validationResults = validationErrors as IList<ValidationResult> ?? validationErrors.ToList();

                if(validationResults.Any())
                {
                    ModelState.AddModelErrors(validationErrors);
                }
                
                if (ModelState.IsValid)
                {
                    var subject = _subjectService.FindSubject(Id);
                    _subjectService.Delete(subject);

                    return Helper.ComposeResponse(HttpStatusCode.OK, Constants.Subject.SubjectSuccessDelete);
                }

            } catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return Helper.ComposeResponse(HttpStatusCode.BadRequest, Helper.GetModelStateErrors(ModelState));
        }
    }
}