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
using System.Linq;
using BaseCode.Domain;

namespace BaseCode.API.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly IJobService _jobService;
        private readonly IMapper _mapper;

        public JobController(IJobService jobService, IMapper mapper)
        {
            _jobService = jobService;
            _mapper = mapper;
        }

        [HttpGet]
        [ActionName("list")]
        public HttpResponseMessage GetJobList([FromQuery] JobSearchViewModel searchModel)
        {
            var responseData = _jobService.FindJobs(searchModel);
            return Helper.ComposeResponse(HttpStatusCode.OK, responseData);
        }

        [HttpGet]
        [ActionName("getJob")]
        public HttpResponseMessage GetJob(int Id)
        {
            var job = _jobService.FindJob(Id);
            return job != null ? Helper.ComposeResponse(HttpStatusCode.OK, job) : Helper.ComposeResponse(HttpStatusCode.NotFound, Constants.Job.JobDoesNotExist);
        }

        [HttpPost] 
        [ActionName("add")]
        public HttpResponseMessage PostJob(JobViewModel jobViewModel)
        {
            if (!ModelState.IsValid)
            {
                return Helper.ComposeResponse(HttpStatusCode.BadRequest, Helper.GetModelStateErrors(ModelState));
            }

            try
            {
                var job = _mapper.Map<Job>(jobViewModel);
                var validationErrors = new JobHandler(_jobService).CanAdd(job);
                var validationResults = validationErrors as IList<ValidationResult> ?? validationErrors.ToList();

                if (validationResults.Any())
                {
                    ModelState.AddModelErrors(validationErrors);
                }

                if (ModelState.IsValid)
                {
                    _jobService.Create(job);
                    return Helper.ComposeResponse(HttpStatusCode.OK, Constants.Job.JobSuccessAdd);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return Helper.ComposeResponse(HttpStatusCode.BadRequest, ModelState);
            }

            return Helper.ComposeResponse(HttpStatusCode.BadRequest, Helper.GetModelStateErrors(ModelState));
        }

        [HttpPut]
        [ActionName("edit")]
        public HttpResponseMessage PutJob(int Id, JobViewModel jobViewModel)
        {
            if (!ModelState.IsValid)
            {
                return Helper.ComposeResponse(HttpStatusCode.BadRequest, Helper.GetModelStateErrors(ModelState));
            }

            try
            {
                var job = _mapper.Map<Job>(jobViewModel);
                job.Id = Id;
                var validationErrors = new JobHandler(_jobService).CanUpdate(job);
                var validationResults = validationErrors as IList<ValidationResult> ?? validationErrors.ToList();

                if (validationResults.Any())
                {
                    ModelState.AddModelErrors(validationErrors);
                }

                if (ModelState.IsValid)
                {
                    _jobService.Update(job);
                    return Helper.ComposeResponse(HttpStatusCode.OK, Constants.Job.JobSuccessUpdate);
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
        public HttpResponseMessage DeleteJob(int Id)
        {
            try
            {
                var validationErrors = new JobHandler(_jobService).CanDelete(Id);
                var validationResults = validationErrors as IList<ValidationResult> ?? validationErrors.ToList();

                if (validationResults.Any())
                {
                    ModelState.AddModelErrors(validationErrors);
                }

                if (ModelState.IsValid)
                {
                    var jobRequirement = _jobService.FindJob(Id);
                    _jobService.Delete(jobRequirement);

                    return Helper.ComposeResponse(HttpStatusCode.OK, Constants.Job.JobSuccessDelete);
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

