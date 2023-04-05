using BaseCode.Data.Contracts;
using BaseCode.Data.Models;
using BaseCode.Data.ViewModels.Common;
using System;
using System.Linq;

namespace BaseCode.Data.Repositories
{
    public class InstructorRepository : BaseRepository, IInstructorRepository
    {
        public InstructorRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public void Create(Instructor instructor)
        {
            instructor.IsActive = true;
            GetDbSet<Instructor>().Add(instructor);
            UnitOfWork.SaveChanges();
        }

        public void Update(Instructor instructor)
        {
            var instructorUpdate = FindInstructor(instructor.Id);
            instructorUpdate.IsActive = instructor.IsActive;
            instructorUpdate.Address = instructor.Address;
            instructorUpdate.FirstName = instructor.FirstName;
            instructorUpdate.LastName = instructor.LastName;
            instructorUpdate.PhoneNumber = instructor.PhoneNumber;
            instructorUpdate.DateHired = instructor.DateHired;
            instructorUpdate.Salary = instructor.Salary;
            UnitOfWork.SaveChanges();
        }

        public void Delete(Instructor instructor)
        {
          GetDbSet<Instructor>().Remove(instructor);
          UnitOfWork.SaveChanges();
        }

        public void SoftDelete(Instructor instructor)
        {
            var instructorUpdate = FindInstructor(instructor.Id);
            instructorUpdate.IsActive = false;
            UnitOfWork.SaveChanges();
        }

        public bool InstructorExists(Instructor instructor)
        {   
            return GetDbSet<Instructor>().Any(x => (x.FirstName == instructor.FirstName && x.LastName == instructor.LastName));
            
        }

        public Instructor FindInstructor(int Id)
        {
            return GetDbSet<Instructor>().FirstOrDefault(x => x.Id == Id);
        }

        public ListViewModel FindInstructors(InstructorSearchViewModel searchModel)
        {
            var instructors = RetrieveAll();

            if (searchModel.Page == 0) searchModel.Page = 1;

            var totalCount = instructors.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / Constants.Instructor.PageSize);

            var results = instructors.Skip(Constants.Instructor.PageSize * (searchModel.Page - 1))
                .Take(Constants.Instructor.PageSize)
                .AsEnumerable()
                .Select(instructor => new
                {
                    id = instructor.Id,
                    firstName = instructor.FirstName,
                    lastName = instructor.LastName,
                    address = instructor.Address,
                    phoneNumber = instructor.PhoneNumber,
                    dateHired = instructor.DateHired,
                    salary = instructor.Salary
                }).ToList();

            var pagination = new
            {
                pages = totalPages,
                size = totalCount
            };

            return new ListViewModel { Pagination = pagination, Data = results };
        }

        public IQueryable<Instructor> RetrieveAll()
        {
            return GetDbSet<Instructor>().Where(x => x.IsActive);
        }

  
    }
}
