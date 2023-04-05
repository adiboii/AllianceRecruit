using BaseCode.Data;
using BaseCode.Data.Models;
using BaseCode.Domain.Contracts;
using System.Collections.Generic;


namespace BaseCode.Domain.Handlers
{
    public class ClassHandler
    {

        private readonly IClassService _classService;

        public ClassHandler(IClassService classService)
        {
            _classService = classService;
        }

        public IEnumerable<ValidationResult> CanAdd(Class c)
        {
            var validationErrors = new List<ValidationResult>();

            if (c != null)
            {
                // Check if Class already exists
                if(_classService.HasClass(c))
                {
                    validationErrors.Add(new ValidationResult(Constants.Class.ClassExists));
                }

                // Check if Class duration `From` is not greater than `To`
                if (c.From > c.To)
                {
                    validationErrors.Add(new ValidationResult(Constants.Class.ClassFromIsGreaterThanTo));
                }
            }

            return validationErrors;
        }

        public IEnumerable<ValidationResult> CanDelete(int Id)
        {
            var validationErrors = new List<ValidationResult>();
            var instructor = _classService.FindClass(Id);
            if(instructor == null)
            {
                validationErrors.Add(new ValidationResult(Constants.Class.ClassDoesNotExist));
            }

            return validationErrors;
        }

        public IEnumerable<ValidationResult> CanUpdate(Class c)
        {
            var validationErrors = new List<ValidationResult>();
            if (c == null)
            {
                validationErrors.Add(new ValidationResult(Constants.Class.ClassDoesNotExist));
            } else
            {
                // Check if Class already exists
                if (_classService.HasClass(c))
                {
                    validationErrors.Add(new ValidationResult(Constants.Class.ClassExists));
                }

                // Check if Class duration `From` is not greater than `To`
                if (c.From > c.To)
                {
                    validationErrors.Add(new ValidationResult(Constants.Class.ClassFromIsGreaterThanTo));
                }
            }

            return validationErrors;
        }
    }
}
