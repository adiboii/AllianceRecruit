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
    public class JobRequirementRepository : BaseRepository, IJobRequirementRepository
    {
        public JobRequirementRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public void Create(JobRequirement jobRequirement)
        {
            GetDbSet<JobRequirement>().Add(jobRequirement);
            UnitOfWork.SaveChanges();
        }
        public void Update(JobRequirement jobRequirement)
        {
            var jobRequirementUpdate = FindJobRequirement(jobRequirement.Id);
            jobRequirementUpdate.JobId = jobRequirement.JobId;
            jobRequirementUpdate.Requirement = jobRequirement.Requirement;
            UnitOfWork.SaveChanges();
        }

        public void Delete(JobRequirement jobRequirement)
        {
            GetDbSet<JobRequirement>().Remove(jobRequirement);
            UnitOfWork.SaveChanges();
        }

        public JobRequirement FindJobRequirement(int Id)
        {
            return GetDbSet<JobRequirement>().FirstOrDefault(x => x.Id == Id);
        }

        public bool JobRequirementExists(JobRequirement jobRequirement)
        {
            return GetDbSet<JobRequirement>().Any(x => x.Requirement == jobRequirement.Requirement);    
        }

        public IQueryable<JobRequirement> RetrieveAll()
        {
            return GetDbSet<JobRequirement>();
        }

        public ListViewModel FindJobRequirements(JobRequirementSearchViewModel searchModel)
        {
            var jobRequirements = RetrieveAll();

            if (searchModel.Page == 0) searchModel.Page = 1;

            var totalCount = jobRequirements.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / Constants.Subject.PageSize);

            var results = jobRequirements.Skip(Constants.Subject.PageSize * (searchModel.Page - 1))
             .Take(Constants.Subject.PageSize)
             .AsEnumerable()
             .Select(jobRequirement => new
             {
                jobId = jobRequirement.Id,
                requirement = jobRequirement.Requirement,
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
