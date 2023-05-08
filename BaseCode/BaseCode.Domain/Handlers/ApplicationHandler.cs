using BaseCode.Data;
using BaseCode.Data.Models;
using BaseCode.Domain.Contracts;
using BaseCode.Domain.Services;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseCode.Domain.Handlers
{
    public class ApplicationHandler
    {
        private readonly IApplicationService _applicationService;

        public ApplicationHandler(IApplicationService applicationService) 
        { 
            _applicationService = applicationService;
        }

        public IEnumerable<ValidationResult> CanAdd(Application application)
        {
            var validationErrors = new List<ValidationResult>();

            if (application != null)
            {
                if (_applicationService.ApplicationExists(application))
                {
                    validationErrors.Add(new ValidationResult(Constants.Application.ApplicationExists));
                }
            }

            return validationErrors;
        }

        public IEnumerable<ValidationResult> CanDelete(int Id)
        {
            var validationErrors = new List<ValidationResult>();
            var jobRequirement = _applicationService.FindApplication(Id);
            if (jobRequirement == null)
            {
                validationErrors.Add(new ValidationResult(Constants.Application.ApplicationDoesNotExist));
            }

            return validationErrors;
        }

        public IEnumerable<ValidationResult> CanUpdate(Application application)
        {
            var validationErrors = new List<ValidationResult>();
            if (application == null)
            {
                validationErrors.Add(new ValidationResult(Constants.Application.ApplicationDoesNotExist));
            }
            else
            {
                // Check if has duplicate already.
                if (_applicationService.ApplicationExists(application))
                {
                    validationErrors.Add(new ValidationResult(Constants.Application.ApplicationExists));
                }
            }

            return validationErrors;
        }
    }
}
