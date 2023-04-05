using BaseCode.Data.Contracts;
using BaseCode.Data.Models;
using BaseCode.Data.ViewModels.Common;
using BaseCode.Domain.Contracts;
using System.Linq;

namespace BaseCode.Domain.Services
{
    public class ClassService : IClassService
    {
        private readonly IClassRepository _classRepository;

        public ClassService(IClassRepository classRepository)
        {
            _classRepository = classRepository;
        }
        public void Create(Class c)
        {
            _classRepository.Create(c);
        }

        public void Delete(Class c)
        {
            _classRepository.Delete(c);
        }

        public bool ClassExists(Class c)
        {
            return _classRepository.ClassExists(c);
        }

        public Class FindClass(int Id)
        {
            return _classRepository.FindClass(Id);
        }

        public ListViewModel FindClasses(ClassSearchViewModel searchModel)
        {
            return _classRepository.FindClasses(searchModel);
        }

        public bool HasClass(Class c)
        {
            return _classRepository.HasClass(c);
        }

        public void Update(Class c)
        {
            _classRepository.Update(c);
        }
    }
}
