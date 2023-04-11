using BaseCode.Data.Models;
using BaseCode.Data.ViewModels.Common;
using BaseCode.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseCode.Domain.Contracts
{
    public interface IJobService
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
