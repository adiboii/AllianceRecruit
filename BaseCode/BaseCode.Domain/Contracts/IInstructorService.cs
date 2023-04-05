using BaseCode.Data.Models;
using BaseCode.Data.ViewModels.Common;
using System.Linq;

namespace BaseCode.Domain.Contracts
{
    public interface IInstructorService
    {
        void Create(Instructor instructor);
        void Update(Instructor instructor);
        void Delete(Instructor instructor);
        void SoftDelete(Instructor instructor);
        Instructor FindInstructor(int Id);
        bool InstructorExists(Instructor instructor);
        ListViewModel FindInstructors(InstructorSearchViewModel searchModel);
    }
}
