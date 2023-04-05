using BaseCode.Data.Models;
using BaseCode.Data.ViewModels.Common;
using System.Data;
using System.Linq;

namespace BaseCode.Data.Contracts
{
    public interface IClassRepository
    {
        void Create(Class c);
        void Update(Class c);
        void Delete(Class c);
        bool ClassExists(Class c);
        bool HasClass(Class c);
        Class FindClass(int Id);
        IQueryable<Class> RetrieveAll();
        ListViewModel FindClasses(ClassSearchViewModel searchModel);
    }
}
