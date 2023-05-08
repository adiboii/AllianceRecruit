using BaseCode.Data.Contracts;
using BaseCode.Data.Models;
using BaseCode.Data.ViewModels;
using BaseCode.Data.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseCode.Data.Repositories
{
    public class JobDescriptionRepository : BaseRepository, IJobDescriptionRepository
    {
        public JobDescriptionRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public void Create(JobDescription jobDescription)
        {
            GetDbSet<JobDescription>().Add(jobDescription);
            UnitOfWork.SaveChanges();
        }
        public void Update(JobDescription jobDescription)
        { 
            var jobRequirementUpdate = FindJobDescription(jobDescription.Id);
            jobRequirementUpdate.JobId = jobDescription.JobId;
            jobRequirementUpdate.Description = jobDescription.Description;
            UnitOfWork.SaveChanges();
        }

        public void Delete(JobDescription jobDescription)
        {
            GetDbSet<JobDescription>().Remove(jobDescription);
            UnitOfWork.SaveChanges();
        }

        public JobDescription FindJobDescription(int Id)
        {
            return GetDbSet<JobDescription>().FirstOrDefault(x => x.Id == Id);
        }

        public bool JobDescriptionExists(JobDescription jobRequirement)
        {
            return GetDbSet<JobDescription>().
                Any(x => x.JobId == jobRequirement.JobId
                    && x.Description == jobRequirement.Description);    
        }

        public IQueryable<JobDescription> RetrieveAll()
        {
            return GetDbSet<JobDescription>();
        }

        public ListViewModel FindJobDescription(JobDescriptionSearchViewModel searchModel)
        {
            var jobDescriptions = RetrieveAll();

            if (searchModel.Page == 0) searchModel.Page = 1;

            var totalCount = jobDescriptions.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / Constants.Subject.PageSize);

            var results = jobDescriptions.Skip(Constants.Subject.PageSize * (searchModel.Page - 1))
             .Take(Constants.Subject.PageSize)
             .AsEnumerable()
             .Select(jobDescription => new
             {
                jobId = jobDescription.Id,
                description = jobDescription.Description,
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
