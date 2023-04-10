using BaseCode.Data;
using BaseCode.Data.Models;
using BaseCode.Domain.Contracts;
using BaseCode.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseCode.Domain.Handlers
{
    public class JobRequirementHandler
    {
        private readonly IJobRequirementService _jobRequirementService;

        public JobRequirementHandler(IJobRequirementService jobRequirementService)
        {
            _jobRequirementService = jobRequirementService;
        }

        public IEnumerable<ValidationResult> CanAdd(JobRequirement jobRequirement)
        {
            var validationErrors = new List<ValidationResult>();

            if (jobRequirement != null)
            {
                // Check if has duplicate already.
                if (_jobRequirementService.JobRequirementExists(jobRequirement))
                {
                    validationErrors.Add(new ValidationResult(Constants.JobRequirement.JobRequirementExists));
                }

            }

            return validationErrors;
        }

        public IEnumerable<ValidationResult> CanDelete(int Id)
        {
            var validationErrors = new List<ValidationResult>();
            var jobRequirement = _jobRequirementService.FindJobRequirement(Id);
            if (jobRequirement == null)
            {
                validationErrors.Add(new ValidationResult(Constants.JobRequirement.JobRequirementDoesNotExist));
            }

            return validationErrors;
        }

        public IEnumerable<ValidationResult> CanUpdate(JobRequirement jobRequirement)
        {
            var validationErrors = new List<ValidationResult>();
            if (jobRequirement == null)
            {
                validationErrors.Add(new ValidationResult(Constants.JobRequirement.JobRequirementDoesNotExist));
            }
            else
            {
                // Check if has duplicate already.
                if (_jobRequirementService.JobRequirementExists(jobRequirement))
                {
                    validationErrors.Add(new ValidationResult(Constants.JobRequirement.JobRequirementExists));
                }
            }

            return validationErrors;
        }
    }
}
