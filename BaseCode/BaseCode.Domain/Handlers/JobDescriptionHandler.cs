using BaseCode.Data;
using BaseCode.Data.Models;
using BaseCode.Domain.Contracts;
using BaseCode.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseCode.Domain.Handlers
{
    public class JobDescriptionHandler
    {
        private readonly IJobDescriptionService _jobDescriptionService;

        public JobDescriptionHandler(IJobDescriptionService jobDescriptionService)
        {
            _jobDescriptionService = jobDescriptionService;
        }

        public IEnumerable<ValidationResult> CanAdd(JobDescription jobDescription)
        {
            var validationErrors = new List<ValidationResult>();

            if (jobDescription != null)
            {
                // Check if has duplicate already.
                if (_jobDescriptionService.JobDescriptionExists(jobDescription))
                {
                    validationErrors.Add(new ValidationResult(Constants.JobDescription.JobDescriptionExists));
                }

            }

            return validationErrors;
        }

        public IEnumerable<ValidationResult> CanDelete(int Id)
        {
            var validationErrors = new List<ValidationResult>();
            var jobDescription = _jobDescriptionService.FindJobDescription(Id);
            if (jobDescription == null)
            {
                validationErrors.Add(new ValidationResult(Constants.JobDescription.JobDescriptionDoesNotExist));
            }

            return validationErrors;
        }

        public IEnumerable<ValidationResult> CanUpdate(JobDescription jobDescription)
        {
            var validationErrors = new List<ValidationResult>();
            if (jobDescription == null)
            {
                validationErrors.Add(new ValidationResult(Constants.JobDescription.JobDescriptionDoesNotExist));
            }
            else
            {
                // Check if has duplicate already.
                if (_jobDescriptionService.JobDescriptionExists(jobDescription))
                {
                    validationErrors.Add(new ValidationResult(Constants.JobDescription.JobDescriptionExists));
                }
            }

            return validationErrors;
        }
    }
}
