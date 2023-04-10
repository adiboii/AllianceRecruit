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
    public class JobDescriptionService : IJobDescriptionService
    {
        private readonly IJobDescriptionRepository _jobDescriptionRepository;

        public JobDescriptionService(IJobDescriptionRepository jobDescriptionRepository)
        {
            _jobDescriptionRepository = jobDescriptionRepository;
        }

        public void Create(JobDescription jobDescription)
        {
            _jobDescriptionRepository.Create(jobDescription);
        }

        public void Update(JobDescription jobDescription)
        {
            _jobDescriptionRepository.Update(jobDescription);
        }

        public void Delete(JobDescription jobDescription)
        {
            _jobDescriptionRepository.Delete(jobDescription);
        }

        public JobDescription FindJobDescription(int Id)
        {
            return _jobDescriptionRepository.FindJobDescription(Id);
        }

        public ListViewModel FindJobDescriptions(JobDescriptionSearchViewModel searchModel)
        {
            return _jobDescriptionRepository.FindJobDescription(searchModel);
        }
        public IQueryable<JobDescription> RetrieveAll()
        {
            return _jobDescriptionRepository.RetrieveAll();
        }

        public bool JobDescriptionExists(JobDescription jobDescription)
        {
            return _jobDescriptionRepository.JobDescriptionExists(jobDescription);
        }
    }
}
