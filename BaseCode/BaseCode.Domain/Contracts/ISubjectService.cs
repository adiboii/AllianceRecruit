using BaseCode.Data.Models;
using BaseCode.Data.ViewModels.Common;

namespace BaseCode.Domain.Contracts
{
    public interface ISubjectService
    {
        void Create(Subject subject);
        void Update(Subject subject);
        void Delete(Subject subject);
        Subject FindSubject(int Id);
        bool SubjectExists(Subject subject);
        ListViewModel FindSubjects(SubjectSearchViewModel subjectSearchViewModel);
    }
}
