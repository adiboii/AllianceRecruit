using BaseCode.Data;
using BaseCode.Data.Models;
using BaseCode.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseCode.Domain.Handlers
{
    public class JobHandler
    {
        private readonly IJobService _jobService;

        public JobHandler(IJobService jobService)
        {
            _jobService = jobService;
        }

        public IEnumerable<ValidationResult> CanAdd(Job job)
        {
            var validationErrors = new List<ValidationResult>();

            if (job != null)
            {
                // Check if has duplicate already.
                if (_jobService.JobExists(job))
                {
                    validationErrors.Add(new ValidationResult(Constants.Job.JobExists));
                }

                
            }

            return validationErrors;
        }

        public IEnumerable<ValidationResult> CanDelete(int Id)
        {
            var validationErrors = new List<ValidationResult>();
            var job = _jobService.FindJob(Id);
            if (job == null)
            {
                validationErrors.Add(new ValidationResult(Constants.Job.JobDoesNotExist));
            }

            return validationErrors;
        }

        public IEnumerable<ValidationResult> CanUpdate(Job job)
        {
            var validationErrors = new List<ValidationResult>();
            if (job == null)
            {
                validationErrors.Add(new ValidationResult(Constants.Job.JobDoesNotExist));
            }
            else
            {
                // Check if has duplicate already.
                if (_jobService.JobExists(job))
                {
                    validationErrors.Add(new ValidationResult(Constants.Job.JobExists));
                }
            }

            return validationErrors;
        }
    }
    
}
