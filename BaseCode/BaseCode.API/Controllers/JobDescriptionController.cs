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
    public class JobDescriptionController : ControllerBase
    {
        private readonly IJobDescriptionService _jobDescriptionService;
        private readonly IMapper _mapper;

        public JobDescriptionController(IJobDescriptionService jobDescriptionService, IMapper mapper)
        {
            _jobDescriptionService = jobDescriptionService;
            _mapper = mapper;
        }

        [HttpGet]
        [ActionName("list")]
        public HttpResponseMessage GetJobDescriptionList([FromQuery] JobDescriptionSearchViewModel searchModel)
        {
            var responseData = _jobDescriptionService.FindJobDescriptions(searchModel);
            return Helper.ComposeResponse(HttpStatusCode.OK, responseData);
        }

        [HttpGet]
        [ActionName("getJobDescription")]
        public HttpResponseMessage GetJobDescription(int Id)
        {
            var personalInformation = _jobDescriptionService.FindJobDescription(Id);
            return personalInformation != null ? Helper.ComposeResponse(HttpStatusCode.OK, personalInformation) : Helper.ComposeResponse(HttpStatusCode.NotFound, Constants.JobDescription.JobDescriptionDoesNotExist);
        }

        [HttpPost]
        [ActionName("add")]
        public HttpResponseMessage PostJobDescription(JobDescriptionViewModel jobDescriptionViewModel)
        {
            if (!ModelState.IsValid)
            {
                return Helper.ComposeResponse(HttpStatusCode.BadRequest, Helper.GetModelStateErrors(ModelState));
            }

            try
            {
                var jobDescription = _mapper.Map<JobDescription>(jobDescriptionViewModel);
                var validationErrors = new JobDescriptionHandler(_jobDescriptionService).CanAdd(jobDescription);
                var validationResults = validationErrors as IList<ValidationResult> ?? validationErrors.ToList();

                if (validationResults.Any())
                {
                    ModelState.AddModelErrors(validationErrors);
                }

                if (ModelState.IsValid)
                {
                    _jobDescriptionService.Create(jobDescription);
                    return Helper.ComposeResponse(HttpStatusCode.OK, Constants.JobDescription.JobDescriptionSuccessAdd);
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
        public HttpResponseMessage PutJobDescription(int Id, JobDescriptionViewModel jobDescriptionViewModel)
        {
            if (!ModelState.IsValid)
            {
                return Helper.ComposeResponse(HttpStatusCode.BadRequest, Helper.GetModelStateErrors(ModelState));
            }

            try
            {
                var jobDescription = _mapper.Map<JobDescription>(jobDescriptionViewModel);
                jobDescription.Id = Id;
                var validationErrors = new JobDescriptionHandler(_jobDescriptionService).CanUpdate(jobDescription);
                var validationResults = validationErrors as IList<ValidationResult> ?? validationErrors.ToList();

                if (validationResults.Any())
                {
                    ModelState.AddModelErrors(validationErrors);
                }

                if (ModelState.IsValid)
                {
                    _jobDescriptionService.Update(jobDescription);
                    return Helper.ComposeResponse(HttpStatusCode.OK, Constants.JobRequirement.JobRequirementSuccessUpdate);
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
        public HttpResponseMessage DeleteJobDescription(int Id)
        {
            try
            {
                var validationErrors = new JobDescriptionHandler(_jobDescriptionService).CanDelete(Id);
                var validationResults = validationErrors as IList<ValidationResult> ?? validationErrors.ToList();

                if (validationResults.Any())
                {
                    ModelState.AddModelErrors(validationErrors);
                }

                if (ModelState.IsValid)
                {
                    var jobRequirement = _jobDescriptionService.FindJobDescription(Id);
                    _jobDescriptionService.Delete(jobRequirement);

                    return Helper.ComposeResponse(HttpStatusCode.OK, Constants.JobRequirement.JobRequirementSuccessDelete);
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
