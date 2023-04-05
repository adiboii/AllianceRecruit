using BaseCode.Data.Models;
using BaseCode.Data.ViewModels.Common;
using System.Data;
using System.Linq;

namespace BaseCode.Data.Contracts
{
    public interface IInstructorRepository
    {
        void Create(Instructor instructor);
        void Update(Instructor instructor);
        void SoftDelete(Instructor instructor);
        void Delete(Instructor instructor);
        bool InstructorExists(Instructor instructor);
        Instructor FindInstructor(int Id);
        IQueryable<Instructor> RetrieveAll();
        ListViewModel FindInstructors(InstructorSearchViewModel searchModel);
    }
}
