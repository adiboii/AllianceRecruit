using BaseCode.Data.Models;
using BaseCode.Data.ViewModels.Common;
using System.Data;
using System.Linq;

namespace BaseCode.Data.Contracts
{
    public interface ISubjectRepository
    {
        void Create(Subject subject);
        void Update(Subject subject);
        void Delete(Subject subject);
        bool SubjectExists(Subject subject);
        Subject FindSubject(int Id);
        IQueryable<Subject> RetrieveAll();
        ListViewModel FindSubjects(SubjectSearchViewModel searchModel);
    }
}
