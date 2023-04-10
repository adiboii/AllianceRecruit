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
    public class JobRequirementService : IJobRequirementService
    {
        private readonly IJobRequirementRepository _jobRequirementRepository;

        public JobRequirementService(IJobRequirementRepository jobRequirementRepository)
        {
            _jobRequirementRepository = jobRequirementRepository;
        }

        public void Create(JobRequirement jobRequirement)
        {
            _jobRequirementRepository.Create(jobRequirement);
        }

        public void Update(JobRequirement jobRequirement)
        {
            _jobRequirementRepository.Update(jobRequirement);
        }

        public void Delete(JobRequirement jobRequirement)
        {
            _jobRequirementRepository.Delete(jobRequirement);
        }

        public JobRequirement FindJobRequirement(int Id)
        {
            return _jobRequirementRepository.FindJobRequirement(Id);
        }

        public ListViewModel FindJobRequirements(JobRequirementSearchViewModel searchModel)
        {
            return _jobRequirementRepository.FindJobRequirements(searchModel);
        }
        public IQueryable<JobRequirement> RetrieveAll()
        {
            return _jobRequirementRepository.RetrieveAll();
        }

        public bool JobRequirementExists(JobRequirement jobRequirement)
        {
            return _jobRequirementRepository.JobRequirementExists(jobRequirement);
        }
    }
}
