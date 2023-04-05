using BaseCode.Data.Contracts;
using BaseCode.Data.Models;
using BaseCode.Data.ViewModels.Common;
using System;
using System.Linq;

namespace BaseCode.Data.Repositories
{
    public class SubjectRepository : BaseRepository, ISubjectRepository
    {
        public SubjectRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public void Create(Subject subject)
        {
            subject.IsActive = true;
            GetDbSet<Subject>().Add(subject);
            UnitOfWork.SaveChanges();
        }
        public void Update(Subject subject)
        {
            var subjectUpdate = FindSubject(subject.Id);
            subjectUpdate.IsActive = subject.IsActive;
            subjectUpdate.Name = subject.Name;
            subjectUpdate.Description = subject.Description;
            subjectUpdate.Category = subject.Category;
            subjectUpdate.NumberOfCredits = subject.NumberOfCredits;
            UnitOfWork.SaveChanges();
        }

        public void Delete(Subject subject)
        {
            // Soft delete only
            var subjectUpdate = FindSubject(subject.Id);
            subjectUpdate.IsActive = false;
            UnitOfWork.SaveChanges();
        }

        public Subject FindSubject(int Id)
        {
            return GetDbSet<Subject>().FirstOrDefault(x => x.Id == Id);
        }

        public bool SubjectExists(Subject subject)
        {
            return GetDbSet<Subject>().Any(x => x.Name == subject.Name);
        }

        public IQueryable<Subject> RetrieveAll()
        {
            return GetDbSet<Subject>().Where(x => x.IsActive);
        }

        public ListViewModel FindSubjects(SubjectSearchViewModel searchModel)
        {
            var subjects = RetrieveAll();

            if (searchModel.Page == 0) searchModel.Page = 1;

            var totalCount = subjects.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / Constants.Subject.PageSize);

            var results = subjects.Skip(Constants.Subject.PageSize * (searchModel.Page - 1))
                .Take(Constants.Subject.PageSize)
                .AsEnumerable()
                .Select(subject => new
                {
                    id = subject.Id,
                    name = subject.Name,
                    description = subject.Description,
                    category = subject.Category,
                    numberOfCredits = subject.NumberOfCredits
                }).ToList();

            var pagination = new
            {
                pages = totalPages,
                size = totalCount
            };

            return new ListViewModel { Pagination = pagination, Data = results };
        }
   
    }
}
