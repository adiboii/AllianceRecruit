using BaseCode.Data.Contracts;
using BaseCode.Data.Models;
using BaseCode.Data.ViewModels.Common;
using System;
using System.Linq;

namespace BaseCode.Data.Repositories
{
    public class StudentRepository : BaseRepository, IStudentRepository
    {
        public StudentRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Student Find(int id)
        {
            return GetDbSet<Student>().Find(id);
        }

        public IQueryable<Student> RetrieveAll()
        {
            return GetDbSet<Student>();
        }

        public ListViewModel FindStudents(StudentSearchViewModel searchModel)
        {
            var sortKey = GetSortKey(searchModel.SortBy);
            var sortDir = ((!string.IsNullOrEmpty(searchModel.SortOrder) && searchModel.SortOrder.Equals("dsc"))) ?
                Constants.SortDirection.Descending : Constants.SortDirection.Ascending;

            var students = RetrieveAll()
                .Where(x => (string.IsNullOrEmpty(searchModel.StudId) || x.StudentId.ToString().Contains(searchModel.StudId)) &&
                            (string.IsNullOrEmpty(searchModel.StudName) || x.Name.Contains(searchModel.StudName)) &&
                            (string.IsNullOrEmpty(searchModel.StudEmail) || x.Email.Contains(searchModel.StudEmail)) &&
                            (string.IsNullOrEmpty(searchModel.StudClass) || x.Class.Contains(searchModel.StudClass)) &&
                            (string.IsNullOrEmpty(searchModel.StudEnrollYear) || x.EnrollYear.Contains(searchModel.StudEnrollYear)) &&
                            (string.IsNullOrEmpty(searchModel.StudCity) || x.City.Contains(searchModel.StudCity)) &&
                            (string.IsNullOrEmpty(searchModel.StudCountry) || x.Country.Contains(searchModel.StudCountry)))
                .OrderByPropertyName(sortKey, sortDir);

            if (searchModel.Page == 0)
                searchModel.Page = 1;
            var totalCount = students.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / searchModel.PageSize);

            var results = students.Skip(searchModel.PageSize * (searchModel.Page - 1))
                .Take(searchModel.PageSize)
                .AsEnumerable()
                .Select(student => new {
                    id = student.StudentId,
                    name = student.Name,
                    year = student.EnrollYear,
                    city = student.City,
                    country = student.Country,
                    section = student.Class,
                    email = student.Email
                })
                .ToList();

            var pagination = new
            {
                pages = totalPages,
                size = totalCount
            };

            return new ListViewModel { Pagination = pagination, Data = results };
        }

        public void Create(Student student)
        {
            GetDbSet<Student>().Add(student);
            UnitOfWork.SaveChanges();
        }

        public void Update(Student student)
        {
            var studentUpdate = Find(student.StudentId);
            studentUpdate.Name = student.Name;
            studentUpdate.City = student.City;
            studentUpdate.Class = student.Class;
            studentUpdate.Country = student.Country;
            studentUpdate.Email = student.Email;
            studentUpdate.EnrollYear = student.EnrollYear;
            studentUpdate.CreatedBy = student.CreatedBy;
            studentUpdate.CreatedDate = student.CreatedDate;
            studentUpdate.ModifiedBy = student.ModifiedBy;
            studentUpdate.ModifiedDate = student.ModifiedDate;
            //this.SetEntityState(student, System.Data.Entity.EntityState.Modified);
            UnitOfWork.SaveChanges();
        }

        public void Delete(Student student)
        {
            GetDbSet<Student>().Remove(student);
            UnitOfWork.SaveChanges();
        }

        public void DeleteById(int id)
        {
            var student = Find(id);
            GetDbSet<Student>().Remove(student);
            UnitOfWork.SaveChanges();
        }

        public bool IsStudentExists(string name)
        {
            return GetDbSet<Student>().Any(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public string GetSortKey(string sortBy)
        {
            string sortKey;

            switch (sortBy)
            {
                case (Constants.Student.StudentHeaderId):
                    sortKey = "StudentID";
                    break;

                case (Constants.Student.StudentHeaderName):
                    sortKey = "Name";
                    break;

                case (Constants.Student.StudentHeaderEmail):
                    sortKey = "Email";
                    break;

                case (Constants.Student.StudentHeaderClass):
                    sortKey = "Class";
                    break;

                case (Constants.Student.StudentHeaderEnrollYear):
                    sortKey = "EnrollYear";
                    break;

                case (Constants.Student.StudentHeaderCity):
                    sortKey = "City";
                    break;

                case (Constants.Student.StudentHeaderCountry):
                    sortKey = "Country";
                    break;

                default:
                    sortKey = "StudentID";
                    break;
            }

            return sortKey;
        }
    }
}
