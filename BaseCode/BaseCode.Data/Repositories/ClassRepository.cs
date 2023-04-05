using BaseCode.Data.Contracts;
using BaseCode.Data.Models;
using BaseCode.Data.ViewModels.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace BaseCode.Data.Repositories
{
    public class ClassRepository : BaseRepository, IClassRepository
    {
        public ClassRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public void Create(Class c)
        { 
            GetDbSet<Class>().Add(c);
            UnitOfWork.SaveChanges();
        }

        public void Update(Class c)
        {
            var cUpdated = FindClass(c.Id);
            cUpdated.SubjectId = c.SubjectId;
            cUpdated.From = c.From;
            cUpdated.To = c.To;
            cUpdated.InstructorId = c.InstructorId;
            cUpdated.ClassCode = c.ClassCode;
            cUpdated.ClassName = c.ClassName;
            cUpdated.RoomNumber = c.RoomNumber;
            UnitOfWork.SaveChanges();
        }

        public void Delete(Class c)
        {
            GetDbSet<Class>().Remove(c);
            UnitOfWork.SaveChanges();
        }

        public bool ClassExists(Class c)
        {
            return GetDbSet<Class>().Any(x => x.ClassName == x.ClassName);
        }

        public Class FindClass(int Id)
        {
            return GetDbSet<Class>().Include(c => c.Subject).Include(c => c.Instructor).FirstOrDefault(c => c.Id == Id);
        }

        public bool HasClass(Class c)
        {
            return GetDbSet<Class>().Any(x =>
                x.Id != c.Id &&
                x.SubjectId == c.SubjectId &&
                x.InstructorId == c.InstructorId && 
                c.From.Date == x.From.Date &&
                c.To.Date == x.To.Date
            );
        }

        public ListViewModel FindClasses(ClassSearchViewModel searchModel)
        {
            var classes = RetrieveAll();

            if (searchModel.Page == 0) searchModel.Page = 1;

            var totalCount = classes.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / Constants.Class.PageSize);

            var results = classes.Skip(Constants.Class.PageSize * (searchModel.Page - 1))
                .Include(c => c.Instructor)
                .Include(c => c.Subject)
                .Take(Constants.Class.PageSize)
                .AsEnumerable()
                .Select(c => new
                {
                    id = c.Id,
                    classCode = c.ClassCode,
                    className = c.ClassName,
                    durationFrom = c.From,
                    durationTo = c.To,
                    subject = c.Subject,
                    instructor = c.Instructor,
                    roomNumber = c.RoomNumber
                }).ToList();

            var pagination = new
            {
                pages = totalPages,
                size = totalCount
            };

            return new ListViewModel { Pagination = pagination, Data = results };
        }

        public IQueryable<Class> RetrieveAll()
        {
            return GetDbSet<Class>();
        }
    }
}
