using AutoMapper;
using BaseCode.Data.ViewModels;
using BaseCode.Data;
using BaseCode.Domain.Contracts;
using BaseCode.Domain.Handlers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System;
using BaseCode.Data.Models;
using BaseCode.Domain;
using System.Linq;

namespace BaseCode.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationService _service;
        private readonly IMapper _mapper;

        public ApplicationController(IApplicationService applicationService, IMapper mapper)
        {
            _service = applicationService;
            _mapper = mapper;
        }

        [HttpGet]
        [ActionName("list")]
        public HttpResponseMessage GetApplications([FromQuery] ApplicationSearchViewModel searchModel)
        {
            var responseData = _service.FindApplications(searchModel);
            return Helper.ComposeResponse(HttpStatusCode.OK, responseData);
        }

        [HttpGet]
        [ActionName("getApplication")]
        public HttpResponseMessage GetApplication(int Id)
        {
            var personalInformation = _service.FindApplication(Id);
            return personalInformation != null ? Helper.ComposeResponse(HttpStatusCode.OK, personalInformation) : Helper.ComposeResponse(HttpStatusCode.NotFound, Constants.Application.ApplicationDoesNotExist);
        }

        [HttpPost]
        [ActionName("add")]
        public HttpResponseMessage PostApplication(ApplicationViewModel applicationViewModel)
        {
            if (!ModelState.IsValid)
            {
                return Helper.ComposeResponse(HttpStatusCode.BadRequest, Helper.GetModelStateErrors(ModelState));
            }

            try
            {
                var application = _mapper.Map<Application>(applicationViewModel);
                var validationErrors = new ApplicationHandler(_service).CanAdd(application);
                var validationResults = validationErrors as IList<ValidationResult> ?? validationErrors.ToList();

                if (validationResults.Any())
                {
                    ModelState.AddModelErrors(validationErrors);
                }

                if (ModelState.IsValid)
                {
                    _service.Create(application);
                    return Helper.ComposeResponse(HttpStatusCode.OK, Constants.Application.ApplicationSuccessAdd);
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
        public HttpResponseMessage PutApplication(int Id, ApplicationViewModel applicationViewModel)
        {
            if (!ModelState.IsValid)
            {
                return Helper.ComposeResponse(HttpStatusCode.BadRequest, Helper.GetModelStateErrors(ModelState));
            }

            try
            {
                var application = _mapper.Map<Application>(applicationViewModel);
                application.Id = Id;
                var validationErrors = new ApplicationHandler(_service).CanUpdate(application);
                var validationResults = validationErrors as IList<ValidationResult> ?? validationErrors.ToList();

                if (validationResults.Any())
                {
                    ModelState.AddModelErrors(validationErrors);
                }

                if (ModelState.IsValid)
                {
                    _service.Update(application);
                    return Helper.ComposeResponse(HttpStatusCode.OK, Constants.Application.ApplicationSuccessAdd);
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
        public HttpResponseMessage DeleteApplication(int Id)
        {
            try
            {
                var validationErrors = new ApplicationHandler(_service).CanDelete(Id);
                var validationResults = validationErrors as IList<ValidationResult> ?? validationErrors.ToList();

                if (validationResults.Any())
                {
                    ModelState.AddModelErrors(validationErrors);
                }

                if (ModelState.IsValid)
                {
                    var jobRequirement = _service.FindApplication(Id);
                    _service.Delete(jobRequirement);

                    return Helper.ComposeResponse(HttpStatusCode.OK, Constants.Attachment.AttachmentSuccessDelete);
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
