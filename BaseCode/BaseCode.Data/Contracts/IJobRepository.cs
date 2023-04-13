using BaseCode.Data.Models;
using BaseCode.Data.ViewModels;
using BaseCode.Data.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseCode.Data.Contracts
{
    public interface IJobRepository
    {
        void Create(Job job);
        void Update(Job job);
        void Delete(Job job);   
        bool JobExists(Job job);
        Job FindJob(int Id);
        IQueryable<Job> RetrieveAll();
        ListViewModel FindJobs(JobSearchViewModel searchModel);
    }
}
