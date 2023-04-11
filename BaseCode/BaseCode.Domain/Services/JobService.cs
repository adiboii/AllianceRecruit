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
    public class JobService : IJobService
    {

        private readonly IJobRepository _jobRepository;

        JobService(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }   

        public void Create(Job job)
        {
            _jobRepository.Create(job);
        }
        public void Update(Job job)
        {
            _jobRepository.Update(job); 
        }

        public void Delete(Job job)
        {
            _jobRepository.Delete(job);
        }

        public Job FindJob(int Id)
        {
           return _jobRepository.FindJob(Id);
        }

        public ListViewModel FindJobs(JobSearchViewModel searchModel)
        {
            return _jobRepository.FindJobs(searchModel);
        }

        public bool JobExists(Job job)
        {
            return _jobRepository.JobExists(job);
        }

        public IQueryable<Job> RetrieveAll()
        {
            return _jobRepository.RetrieveAll();
        }

    }
}
