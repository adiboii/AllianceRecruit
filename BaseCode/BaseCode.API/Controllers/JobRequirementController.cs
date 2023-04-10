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
    public class JobRequirementController : ControllerBase
    {
        private readonly IJobRequirementService _jobRequirementService;
        private readonly IMapper _mapper;

        public JobRequirementController(IJobRequirementService jobRequirementService, IMapper mapper)
        {
            _jobRequirementService = jobRequirementService;
            _mapper = mapper;
        }

        [HttpGet]
        [ActionName("list")]
        public HttpResponseMessage GetJobRequirementList([FromQuery] JobRequirementSearchViewModel searchModel)
        {
            var responseData = _jobRequirementService.FindJobRequirements(searchModel);
            return Helper.ComposeResponse(HttpStatusCode.OK, responseData);
        }

        [HttpGet]
        [ActionName("getJobRequirement")]
        public HttpResponseMessage GetJobRequirement(int Id)
        {
            var personalInformation = _jobRequirementService.FindJobRequirement(Id);
            return personalInformation != null ? Helper.ComposeResponse(HttpStatusCode.OK, personalInformation) : Helper.ComposeResponse(HttpStatusCode.NotFound, Constants.PersonalInformation.PersonalInformationDoesNotExist);
        }

        [HttpPost]
        [ActionName("add")]
        public HttpResponseMessage PostJobRequirement(JobRequirementViewModel jobRequirementViewModel)
        {
            if (!ModelState.IsValid)
            {
                return Helper.ComposeResponse(HttpStatusCode.BadRequest, Helper.GetModelStateErrors(ModelState));
            }

            try
            {
                var jobRequirement = _mapper.Map<JobRequirement>(jobRequirementViewModel);
                var validationErrors = new JobRequirementHandler(_jobRequirementService).CanAdd(jobRequirement);
                var validationResults = validationErrors as IList<ValidationResult> ?? validationErrors.ToList();

                if (validationResults.Any())
                {
                    ModelState.AddModelErrors(validationErrors);
                }

                if (ModelState.IsValid)
                {
                    _jobRequirementService.Create(jobRequirement);
                    return Helper.ComposeResponse(HttpStatusCode.OK, Constants.JobRequirement.JobRequirementSuccessAdd);
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
        public HttpResponseMessage PutJobRequirement(int Id, JobRequirementViewModel jobRequirementViewModel)
        {
            if (!ModelState.IsValid)
            {
                return Helper.ComposeResponse(HttpStatusCode.BadRequest, Helper.GetModelStateErrors(ModelState));
            }

            try
            {
                var jobRequirement = _mapper.Map<JobRequirement>(jobRequirementViewModel);
                jobRequirement.Id = Id;
                var validationErrors = new JobRequirementHandler(_jobRequirementService).CanUpdate(jobRequirement);
                var validationResults = validationErrors as IList<ValidationResult> ?? validationErrors.ToList();

                if (validationResults.Any())
                {
                    ModelState.AddModelErrors(validationErrors);
                }

                if (ModelState.IsValid)
                {
                    _jobRequirementService.Update(jobRequirement);
                    return Helper.ComposeResponse(HttpStatusCode.OK, Constants.JobRequirement.JobRequirementUpdateSuccess);
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
        public HttpResponseMessage DeleteJobRequirement(int Id)
        {
            try
            {
                var validationErrors = new JobRequirementHandler(_jobRequirementService).CanDelete(Id);
                var validationResults = validationErrors as IList<ValidationResult> ?? validationErrors.ToList();

                if (validationResults.Any())
                {
                    ModelState.AddModelErrors(validationErrors);
                }

                if (ModelState.IsValid)
                {
                    var jobRequirement = _jobRequirementService.FindJobRequirement(Id);
                    _jobRequirementService.Delete(jobRequirement);

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
