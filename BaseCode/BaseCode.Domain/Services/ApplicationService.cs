using BaseCode.Data.Contracts;
using BaseCode.Data.Models;
using BaseCode.Data.ViewModels;
using BaseCode.Data.ViewModels.Common;
using BaseCode.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseCode.Domain.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly IApplicationRepository _applicationRepository;

       public ApplicationService(IApplicationRepository applicationRepository)
        {
            _applicationRepository = applicationRepository;
        }

        public void Create(Application application)
        {
            _applicationRepository.Create(application);
        }

        public void Update(Application application)
        {
            _applicationRepository.Update(application);
        }

        public void Delete(Application application)
        {
            _applicationRepository.Delete(application);
        }

        public bool ApplicationExists(Application application)
        {
            return _applicationRepository.ApplicationExists(application);
        }

        public Application FindApplication(int Id)
        {
            return _applicationRepository.FindApplication(Id);
        }
        public IQueryable<Application> RetrieveAll()
        {
            return _applicationRepository.RetrieveAll();
        }

        public ListViewModel FindApplications(ApplicationSearchViewModel searchModel)
        {
            return _applicationRepository.FindApplications(searchModel);
        }

    }
}
