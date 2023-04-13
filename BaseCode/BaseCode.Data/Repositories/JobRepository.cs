using BaseCode.Data.Contracts;
using BaseCode.Data.Models;
using BaseCode.Data.ViewModels;
using BaseCode.Data.ViewModels.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseCode.Data.Repositories
{
    public class JobRepository : BaseRepository, IJobRepository
    {
        public JobRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public void Create(Job job)
        {
            GetDbSet<Job>().Add(job);   
            UnitOfWork.SaveChanges();
        }
        public void Update(Job job)
        {
            var jobUpdate = FindJob(job.Id);
            jobUpdate.JobTitle = job.JobTitle;
            jobUpdate.Location = job.Location;
            jobUpdate.JobDescriptions = job.JobDescriptions;
            jobUpdate.JobRequirements = job.JobRequirements;
            UnitOfWork.SaveChanges();
        }

        public void Delete(Job job)
        {
            GetDbSet<Job>().Remove(job);    
            UnitOfWork.SaveChanges();
        }

        public Job FindJob(int Id)
        {
            return GetDbSet<Job>().FirstOrDefault(x => x.Id == Id);
        }

        public bool JobExists(Job job)
        {
            return GetDbSet<Job>().Any(x => x.JobTitle == job.JobTitle);
        }

        public IQueryable<Job> RetrieveAll()
        {
            return GetDbSet<Job>();
        }

        public ListViewModel FindJobs(JobSearchViewModel searchModel)
        {
            var jobs = RetrieveAll().Include(j => j.JobDescriptions)
                   .Include(j => j.JobRequirements)
                   .ToList();

            if (searchModel.Page == 0) searchModel.Page = 1;

            var totalCount = jobs.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / Constants.Subject.PageSize);

            var results = jobs.Skip(Constants.Subject.PageSize * (searchModel.Page - 1))
                .Take(Constants.Subject.PageSize)
                .AsEnumerable()
                .Select(job => new
                {
                    id = job.Id,
                    jobTitle = job.JobTitle,
                    location = job.Location,
                    jobDescriptions = job.JobDescriptions.Select(d => d.Description).ToList(),
                    jobRequirements = job.JobRequirements.Select(r => r.Requirement).ToList()
                }).ToList();

            var pagination = new
            {
                pages = totalPages,
                size = totalCount
            };

            return new ListViewModel { Pagination = pagination, Data = results };
        }
    }
}
