using BaseCode.Data.Models;
using BaseCode.Data.ViewModels.Common;
using System.Linq;

namespace BaseCode.Data.Contracts
{
    public interface IStudentRepository
    {
        Student Find(int id);
        IQueryable<Student> RetrieveAll();
        ListViewModel FindStudents(StudentSearchViewModel searchModel);
        void Create(Student student);
        void Update(Student student);
        void Delete(Student student);
        void DeleteById(int id);
        bool IsStudentExists(string name);    
        string GetSortKey(string sortBy);
    }
}
