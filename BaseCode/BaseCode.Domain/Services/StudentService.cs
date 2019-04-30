using System.Linq;
using AutoMapper;
using BaseCode.Data.Contracts;
using BaseCode.Data.Models;
using BaseCode.Data.ViewModels;
using BaseCode.Data.ViewModels.Common;
using BaseCode.Domain.Contracts;

namespace BaseCode.Domain.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public StudentService(IStudentRepository studentRepository, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        public StudentViewModel Find(int id)
        {
            StudentViewModel studentViewModel = null;
            var student = _studentRepository.Find(id);

            if (student != null)
            {
                studentViewModel = _mapper.Map<StudentViewModel>(student);
            }

            return studentViewModel;
        }

        public IQueryable<Student> RetrieveAll()
        {
            return _studentRepository.RetrieveAll();
        }

        public ListViewModel FindStudents(StudentSearchViewModel searchModel)
        {
            return _studentRepository.FindStudents(searchModel);
        }

        public void Create(Student student)
        {
            _studentRepository.Create(student);
        }

        public void Update(Student student)
        {
            _studentRepository.Update(student);
        }

        public void Delete(Student student)
        {
            _studentRepository.Delete(student);
        }

        public void DeleteById(int id)
        {
            _studentRepository.DeleteById(id);
        }

        public bool IsStudentExists(string name)
        {
            return _studentRepository.IsStudentExists(name);
        }
    }
}
