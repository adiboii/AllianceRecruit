using BaseCode.Data.Models;
using BaseCode.Domain.Contracts;
using System.Collections.Generic;
using Constants = BaseCode.Data.Constants;


namespace BaseCode.Domain.Handlers
{
    public class SubjectHandler
    {

        private readonly ISubjectService _subjectService;

        public SubjectHandler(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        public IEnumerable<ValidationResult> CanAdd(Subject subject)
        {
            var validationErrors = new List<ValidationResult>();

            if (subject != null)
            {
                // Check if has duplicate already.
                if(_subjectService.SubjectExists(subject))
                {
                    validationErrors.Add(new ValidationResult(Constants.Subject.SubjectExists));
                }

                // Check if number of credits is greater than 5 and not less than or equal to 0.
                if (subject.NumberOfCredits > 5 && subject.NumberOfCredits <= 0)
                {
                    validationErrors.Add(new ValidationResult(Constants.Subject.SubjectTooMuchNumberOfCredits));
                }

                // Check if description length is beyond than 100 characters.
                if(subject.Description.Length > 100)
                {
                    validationErrors.Add(new ValidationResult(Constants.Subject.SubjectDescriptionTooLong));
                }

                // Check if subject name length exceeds 30 characters.
                if(subject.Name.Length > 30)
                {
                    validationErrors.Add(new ValidationResult(Constants.Subject.SubjectNameTooLong));
                }

                // Check if category is neither `major` nor `minor`.
                if(!(subject.Category == "major" || subject.Category == "minor"))
                {
                    validationErrors.Add(new ValidationResult(Constants.Subject.SubjectCategoryNotFound));
                }
            }

            return validationErrors;
        }

        public IEnumerable<ValidationResult> CanDelete(int Id)
        {
            var validationErrors = new List<ValidationResult>();
            var subject = _subjectService.FindSubject(Id);
            if(subject == null)
            {
                validationErrors.Add(new ValidationResult(Constants.Subject.SubjectDoesNotExist));
            }

            return validationErrors;
        }

        public IEnumerable<ValidationResult> CanUpdate(Subject subject)
        {
            var validationErrors = new List<ValidationResult>();

            if (subject == null)
            {
                validationErrors.Add(new ValidationResult(Constants.Subject.SubjectDoesNotExist));
            }
            else
            { 
                // Check if number of credits is greater than 5 and not less than or equal to 0.
                if (subject.NumberOfCredits > 5 && subject.NumberOfCredits <= 0)
                {
                    validationErrors.Add(new ValidationResult(Constants.Subject.SubjectTooMuchNumberOfCredits));
                }

                // Check if description length is beyond than 100 characters.
                if (subject.Description.Length > 100)
                {
                    validationErrors.Add(new ValidationResult(Constants.Subject.SubjectDescriptionTooLong));
                }

                // Check if subject name length exceeds 30 characters.
                if (subject.Name.Length > 30)
                {
                    validationErrors.Add(new ValidationResult(Constants.Subject.SubjectNameTooLong));
                }

                // Check if subject name is already taken
                if (_subjectService.SubjectExists(subject)) {
                    validationErrors.Add(new ValidationResult(Constants.Subject.SubjectNameAlreadyTaken));
                }

                // Check if category is neither `major` nor `minor`.
                if (!(subject.Category == "major" || subject.Category == "minor"))
                {
                    validationErrors.Add(new ValidationResult(Constants.Subject.SubjectCategoryNotFound));
                }
            }

            return validationErrors;
        }
    }
}
