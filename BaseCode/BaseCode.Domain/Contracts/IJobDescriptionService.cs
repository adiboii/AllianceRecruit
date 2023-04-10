using BaseCode.Data.Models;
using BaseCode.Data.ViewModels.Common;
using BaseCode.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseCode.Domain.Contracts
{
    public interface IJobDescriptionService
    {
        void Create(JobDescription jobDescription);
        void Update(JobDescription jobDescription);
        void Delete(JobDescription jobDescription);
        bool JobDescriptionExists(JobDescription jobDescription);
        JobDescription FindJobDescription(int Id);
        IQueryable<JobDescription> RetrieveAll();
        ListViewModel FindJobDescriptions(JobDescriptionSearchViewModel searchModel);
    }
}
