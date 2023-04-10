﻿using BaseCode.Data.ViewModels.Common;
using BaseCode.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseCode.Data.Models;

namespace BaseCode.Data.Contracts
{
    public interface IJobRequirementRepository
    {
        void Create(JobRequirement jobRequirement);
        void Update(JobRequirement jobRequirement);
        void Delete(JobRequirement jobRequirement);
        bool JobRequirementExists(JobRequirement jobRequirement);
        JobRequirement FindJobRequirement(int Id);
        IQueryable<JobRequirement> RetrieveAll();
        ListViewModel FindJobRequirements(JobRequirementSearchViewModel searchModel);
    }
}
