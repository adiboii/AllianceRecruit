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
            return GetDbSet<Application>().Any(x => x.Id == application.Id);
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
            var applications = RetrieveAll();

            if (searchModel.Page == 0) searchModel.Page = 1;

            var totalCount = applications.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / Constants.Subject.PageSize);

            var results = applications.Skip(Constants.Subject.PageSize * (searchModel.Page - 1))
                .Include(a => a.Job)
                .Include(a => a.PersonalInformation)
                .Include(a => a.Attachment)
                .Take(Constants.Subject.PageSize)
                .AsEnumerable()
                .Select(application => new
                {
                    id = application.Id,
                    status = application.Status,
                    dateTimeApplied = application.DateTimeApplied,
                    job = application.Job,
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
