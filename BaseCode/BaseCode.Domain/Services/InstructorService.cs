using BaseCode.Data.Contracts;
using BaseCode.Data.Models;
using BaseCode.Data.ViewModels.Common;
using BaseCode.Domain.Contracts;
using System.Linq;

namespace BaseCode.Domain.Services
{
    public class InstructorService : IInstructorService
    {
        private readonly IInstructorRepository _instructorRepository;

        public InstructorService(IInstructorRepository instructorRepository)
        {
            _instructorRepository = instructorRepository;
        }
        public void Create(Instructor instructor)
        {
            _instructorRepository.Create(instructor);
        }

        public void Delete(Instructor instructor)
        {
            _instructorRepository.Delete(instructor);
        }

        public bool InstructorExists(Instructor instructor)
        {
            return _instructorRepository.InstructorExists(instructor);
        }

        public Instructor FindInstructor(int Id)
        {
            return _instructorRepository.FindInstructor(Id);
        }

        public ListViewModel FindInstructors(InstructorSearchViewModel searchModel)
        {
            return _instructorRepository.FindInstructors(searchModel);
        }

        public void Update(Instructor instructor)
        {
            _instructorRepository.Update(instructor);
        }

        public void SoftDelete(Instructor instructor)
        {
            _instructorRepository.SoftDelete(instructor);
        }
    }
}
