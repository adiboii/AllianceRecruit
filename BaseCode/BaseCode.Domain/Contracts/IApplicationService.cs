using BaseCode.Data.ViewModels.Common;
using BaseCode.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseCode.Data.Models;

namespace BaseCode.Domain.Contracts
{
    public interface IApplicationService
    {
        void Create(Application application);
        void Update(Application application);
        void Delete(Application application);
        bool ApplicationExists(Application application);
        Application FindApplication(int Id);
        IQueryable<Application> RetrieveAll();
        ListViewModel FindApplications(ApplicationSearchViewModel searchModel);
    }
}
