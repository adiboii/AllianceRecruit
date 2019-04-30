using BaseCode.Data.Models;
using BaseCode.Domain.Contracts;
using System.Collections.Generic;
using Constants = BaseCode.Data.Constants;


namespace BaseCode.Domain.Handlers
{
    public class StudentHandler
    {
        private readonly IStudentService _studentService;

        public StudentHandler(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public IEnumerable<ValidationResult> CanAdd(Student student)
        {
            var validationErrors = new List<ValidationResult>();

            if (student != null)
            {
                if (_studentService.IsStudentExists(student.Name))
                {
                    validationErrors.Add(new ValidationResult(Constants.Student.StudentNameExists));
                }
            }
            else
            {
                validationErrors.Add(new ValidationResult(Constants.Student.StudentEntryInvalid));
            }

            return validationErrors;
        }

        public IEnumerable<ValidationResult> CanUpdate(Student student)
        {
            var validationErrors = new List<ValidationResult>();

            if (student != null)
            {
                var dbStudent = _studentService.Find(student.StudentId);

                if (dbStudent != null)
                {
                    if (!dbStudent.Name.Equals(student.Name) && _studentService.IsStudentExists(student.Name))
                    {
                        validationErrors.Add(new ValidationResult(Constants.Student.StudentNameExists));
                    }
                }
                else
                {
                    validationErrors.Add(new ValidationResult(Constants.Student.StudentNotExist));
                }
            }
            else
            {
                validationErrors.Add(new ValidationResult(Constants.Student.StudentEntryInvalid));
            }

            return validationErrors;
        }

        public IEnumerable<ValidationResult> CanDelete(int id)
        {
            var validationErrors = new List<ValidationResult>();

            var student = _studentService.Find(id);
            if (student == null)
            {
                validationErrors.Add(new ValidationResult(Constants.Student.StudentNotExist));
            }

            return validationErrors;
        }
    }
}
