using BaseCode.Data.Models;
using BaseCode.Data.ViewModels.Common;
using System.Linq;

namespace BaseCode.Domain.Contracts
{
    public interface IClassService
    {
        void Create(Class c);
        void Update(Class c);
        void Delete(Class c);
        Class FindClass(int Id);
        bool ClassExists(Class c);
        bool HasClass(Class c);
        ListViewModel FindClasses(ClassSearchViewModel searchModel);
    }
}
