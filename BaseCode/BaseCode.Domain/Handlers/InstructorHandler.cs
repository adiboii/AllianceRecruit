using BaseCode.Data.Models;
using BaseCode.Domain.Contracts;
using System.Collections.Generic;
using Constants = BaseCode.Data.Constants;


namespace BaseCode.Domain.Handlers
{
    public class InstructorHandler
    {

        private readonly IInstructorService _instructorService;

        public InstructorHandler(IInstructorService instructorService)
        {
            _instructorService = instructorService;
        }

        public IEnumerable<ValidationResult> CanAdd(Instructor instructor)
        {
            var validationErrors = new List<ValidationResult>();

            if (instructor != null)
            {
                // Check if has duplicate already.
                if(_instructorService.InstructorExists(instructor))
                {
                    validationErrors.Add(new ValidationResult(Constants.Instructor.InstructorExists));
                }

                // Check if First Name exceeds 250.
                if(instructor.FirstName.Length > 250)
                {
                    validationErrors.Add(new ValidationResult(Constants.Instructor.InstructorFirstNameTooLong));
                }

                // Check if Last Name exceeds 250.
                if (instructor.LastName.Length > 250)
                {
                    validationErrors.Add(new ValidationResult(Constants.Instructor.InstructorLastNameTooLong));
                }

                // Check if Phone Number length exceeds 11 characters.
                if (instructor.PhoneNumber.Length > 11)
                {
                    validationErrors.Add(new ValidationResult(Constants.Instructor.InstructorPhoneNumberTooLong));
                }

                // Check if Address length exceeds 500 characters.
                if (instructor.Address.Length > 500)
                {
                    validationErrors.Add(new ValidationResult(Constants.Instructor.InstructorPhoneNumberTooLong));
                }
            }

            return validationErrors;
        }

        public IEnumerable<ValidationResult> CanDelete(int Id)
        {
            var validationErrors = new List<ValidationResult>();
            var instructor = _instructorService.FindInstructor(Id);
            if(instructor == null)
            {
                validationErrors.Add(new ValidationResult(Constants.Instructor.InstructorDoesNotExist));
            }

            return validationErrors;
        }

        public IEnumerable<ValidationResult> CanUpdate(Instructor instructor)
        {
            var validationErrors = new List<ValidationResult>();
            if (instructor == null)
            {
                validationErrors.Add(new ValidationResult(Constants.Instructor.InstructorDoesNotExist));
            } 
            else 
            {
                // Check if has duplicate already.
                if (_instructorService.InstructorExists(instructor))
                {
                    validationErrors.Add(new ValidationResult(Constants.Instructor.InstructorNameAlreadyTaken));
                }

                // Check if First Name exceeds 250.
                if (instructor.FirstName.Length > 250)
                {
                    validationErrors.Add(new ValidationResult(Constants.Instructor.InstructorFirstNameTooLong));
                }

                // Check if Last Name exceeds 250.
                if (instructor.LastName.Length > 250)
                {
                    validationErrors.Add(new ValidationResult(Constants.Instructor.InstructorLastNameTooLong));
                }

                // Check if Phone Number length exceeds 11 characters.
                if (instructor.PhoneNumber.Length > 11)
                {
                    validationErrors.Add(new ValidationResult(Constants.Instructor.InstructorPhoneNumberTooLong));
                }

                // Check if Address length exceeds 500 characters.
                if (instructor.Address.Length > 500)
                {
                    validationErrors.Add(new ValidationResult(Constants.Instructor.InstructorPhoneNumberTooLong));
                }
            }

            return validationErrors;
        }
    }
}
