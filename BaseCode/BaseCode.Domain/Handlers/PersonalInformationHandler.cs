using BaseCode.Data;
using BaseCode.Data.Models;
using BaseCode.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseCode.Domain.Handlers
{
    public class PersonalInformationHandler
    {
        private readonly IPersonalInformationService _personalInformationService;

        public PersonalInformationHandler(IPersonalInformationService personalInformationService)
        {
            _personalInformationService = personalInformationService;
        }

        public IEnumerable<ValidationResult> CanAdd(PersonalInformation personalInformation)
        {
            var validationErrors = new List<ValidationResult>();

            if (personalInformation != null)
            {
                // Check if has duplicate already.
                if (_personalInformationService.PersonalInformationExists(personalInformation))
                {
                    validationErrors.Add(new ValidationResult(Constants.PersonalInformation.PersonalInformationExists));
                }

                // Check if First Name exceeds 250.
                if (personalInformation.FirstName.Length > 250)
                {
                    validationErrors.Add(new ValidationResult(Constants.PersonalInformation.PersonalInformationFirstNameTooLong));
                }

                // Check if Last Name exceeds 250.
                if (personalInformation.LastName.Length > 250)
                {
                    validationErrors.Add(new ValidationResult(Constants.PersonalInformation.PersonalInformationLastNameTooLong));
                }

                // Check if Phone Number length exceeds 11 characters.
                if (personalInformation.PhoneNumber.Length > 11)
                {
                    validationErrors.Add(new ValidationResult(Constants.PersonalInformation.PersonalInformationPhoneNumberTooLong));
                }

                // Check if Address length exceeds 500 characters.
                if (personalInformation.AddressLine1.Length > 500 || personalInformation.AddressLine2.Length > 500)
                {
                    validationErrors.Add(new ValidationResult(Constants.PersonalInformation.PersonalInformationAddressTooLong));
                }
            }

            return validationErrors;
        }

        public IEnumerable<ValidationResult> CanDelete(int Id)
        {
            var validationErrors = new List<ValidationResult>();
            var personalInformation = _personalInformationService.FindPersonalInformation(Id);
            if (personalInformation == null)
            {
                validationErrors.Add(new ValidationResult(Constants.PersonalInformation.PersonalInformationDoesNotExist));
            }

            return validationErrors;
        }

        public IEnumerable<ValidationResult> CanUpdate(PersonalInformation personalInformation)
        {
            var validationErrors = new List<ValidationResult>();
            if (personalInformation == null)
            {
                validationErrors.Add(new ValidationResult(Constants.PersonalInformation.PersonalInformationDoesNotExist));
            }
            else
            {
                // Check if has duplicate already.
                if (_personalInformationService.PersonalInformationExists(personalInformation))
                {
                    validationErrors.Add(new ValidationResult(Constants.PersonalInformation.PersonalInformationExists));
                }

                // Check if First Name exceeds 250.
                if (personalInformation.FirstName.Length > 250)
                {
                    validationErrors.Add(new ValidationResult(Constants.PersonalInformation.PersonalInformationFirstNameTooLong));
                }

                // Check if Last Name exceeds 250.
                if (personalInformation.LastName.Length > 250)
                {
                    validationErrors.Add(new ValidationResult(Constants.PersonalInformation.PersonalInformationLastNameTooLong));
                }

                // Check if Phone Number length exceeds 11 characters.
                if (personalInformation.PhoneNumber.Length > 11)
                {
                    validationErrors.Add(new ValidationResult(Constants.PersonalInformation.PersonalInformationPhoneNumberTooLong));
                }

                // Check if Address length exceeds 500 characters.
                if (personalInformation.AddressLine1.Length > 500 || personalInformation.AddressLine2.Length > 500)
                {
                    validationErrors.Add(new ValidationResult(Constants.PersonalInformation.PersonalInformationAddressTooLong));
                }
            }

            return validationErrors;
        }
    }
}
