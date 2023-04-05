using BaseCode.Data.Contracts;
using BaseCode.Data.Models;
using BaseCode.Data.ViewModels.Common;
using BaseCode.Domain.Contracts;

namespace BaseCode.Domain.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly ISubjectRepository _subjectRepository;

        public SubjectService(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }
        public void Create(Subject subject)
        {
            _subjectRepository.Create(subject);
        }

        public void Delete(Subject subject)
        {
            _subjectRepository.Delete(subject);
        }

        public bool SubjectExists(Subject subject)
        {
            return _subjectRepository.SubjectExists(subject);
        }

        public Subject FindSubject(int Id)
        {
            return _subjectRepository.FindSubject(Id);
        }

        public ListViewModel FindSubjects(SubjectSearchViewModel subjectSearchViewModel)
        {
            return _subjectRepository.FindSubjects(subjectSearchViewModel);
        }

        public void Update(Subject subject)
        {
           _subjectRepository.Update(subject);
        }
    }
}
