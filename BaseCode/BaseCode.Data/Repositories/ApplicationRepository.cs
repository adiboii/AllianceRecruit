﻿using BaseCode.Data.Contracts;
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
    public class ApplicationRepository : BaseRepository, IApplicationRepository
    {
        public ApplicationRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public void Create(Application application)
        {
            GetDbSet<Application>().Add(application);
            UnitOfWork.SaveChanges();
        }

        public void Update(Application application)
        {
            var applicationUpdate = FindApplication(application.Id);
            applicationUpdate.JobId = application.JobId;
            applicationUpdate.PersonalInformationId = application.PersonalInformationId;
            applicationUpdate.AttachmentId = application.AttachmentId;
            applicationUpdate.Status = application.Status;
            applicationUpdate.Remarks = application.Remarks;
            application.DateTimeApplied = application.DateTimeApplied;
            UnitOfWork.SaveChanges();
        }

        public void Delete(Application application)
        {
            GetDbSet<Application>().Remove(application);
            UnitOfWork.SaveChanges();
        }

        public bool ApplicationExists(Application application)
        {
            return GetDbSet<Application>().
                Any(x => x.JobId == application.JobId 
                       && x.PersonalInformationId == application.PersonalInformationId
                       && x.AttachmentId == application.AttachmentId
                       && x.Status == application.Status
                       && x.Remarks == application.Remarks
                       && x.DateTimeApplied == application.DateTimeApplied);
        }   

        public Application FindApplication(int Id)
        {
            return GetDbSet<Application>().FirstOrDefault(x => x.Id == Id);
        }

        public IQueryable<Application> RetrieveAll()
        {
            return GetDbSet<Application>();
        }

        public ListViewModel FindApplications(ApplicationSearchViewModel searchModel)
        {
            var sortDir = ((!string.IsNullOrEmpty(searchModel.SortOrder) && searchModel.SortOrder.Equals("dsc"))) ?
                Constants.SortDirection.Descending : Constants.SortDirection.Ascending;

            var applications = RetrieveAll()
                .Where(x => (string.IsNullOrEmpty(searchModel.JobId) || x.JobId.ToString().Contains(searchModel.JobId)) &&
                            (string.IsNullOrEmpty(searchModel.PersonalInformationId) || x.JobId.ToString().Contains(searchModel.PersonalInformationId)) &&
                            (string.IsNullOrEmpty(searchModel.AttachmentId) || x.JobId.ToString().Contains(searchModel.AttachmentId)) &&
                            (string.IsNullOrEmpty(searchModel.Status) || x.JobId.ToString().Contains(searchModel.Status)));

            if (searchModel.Page == 0) searchModel.Page = 1;

            var totalCount = applications.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / searchModel.PageSize);

            var results = applications.Skip(Constants.Application.PageSize * (searchModel.Page - 1))
                .Include(a => a.Job)
                    .ThenInclude(j => j.JobDescriptions)
                .Include(a => a.Job)
                    .ThenInclude(j => j.JobRequirements)
                .Include(a => a.PersonalInformation)
                .Include(a => a.Attachment)
                .Take(Constants.Subject.PageSize)
                .AsEnumerable()
                .Select(application => new
                {
                    id = application.Id,
                    status = application.Status,
                    dateTimeApplied = application.DateTimeApplied,
                    job = new
                    {
                        id = application.Job.Id,
                        JobTitle = application.Job.JobTitle,
                        Location = application.Job.Location,
                        JobRequirements = application.Job.JobRequirements.Select(jr => jr.Requirement),
                        JobDescriptions = application.Job.JobDescriptions.Select(jd => jd.Description)
                    },
                    personalInformation = application.PersonalInformation,
                    attachment = application.Attachment,
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
