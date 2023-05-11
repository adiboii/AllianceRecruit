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
    public class AttachmentRepository : BaseRepository, IAttachmentRepository
    {
        public AttachmentRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public void Create(Attachment attachment)
        {
            GetDbSet<Attachment>().Add(attachment);
            UnitOfWork.SaveChanges();
        }
        public void Update(Attachment attachment)
        {
            var attachmentUpdate = FindAttachment(attachment.Id);
            attachmentUpdate.LinkedInProfile = attachment.LinkedInProfile;
            attachmentUpdate.PortfolioUrl = attachment.PortfolioUrl;
            attachmentUpdate.FormalPhoto = attachment.FormalPhoto;
            attachmentUpdate.Resume = attachment.Resume;
            UnitOfWork.SaveChanges();
        }

        public void Delete(Attachment attachment)
        {
            GetDbSet<Attachment>().Remove(attachment);
            UnitOfWork.SaveChanges();
        }

        public Attachment FindAttachment(int Id)
        {
            return GetDbSet<Attachment>().FirstOrDefault(x => x.Id == Id);
        }

        public bool AttachmentExists(Attachment attachment)
        {
            return GetDbSet<Attachment>().
                Any(x => x.Resume == attachment.Resume 
                    && x.FormalPhoto == attachment.FormalPhoto
                    && x.PortfolioUrl == attachment.PortfolioUrl
                    && x.LinkedInProfile == attachment.LinkedInProfile);
        }

        public IQueryable<Attachment> RetrieveAll()
        {
            return GetDbSet<Attachment>();
        }

        public ListViewModel FindAttachments(AttachmentSearchViewModel searchModel)
        {
            var attachments = RetrieveAll();

            if (searchModel.Page == 0) searchModel.Page = 1;

            var totalCount = attachments.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / searchModel.PageSize);

            var results = attachments.Skip(Constants.Subject.PageSize * (searchModel.Page - 1))
                .Take(Constants.Subject.PageSize)
                .AsEnumerable()
                .Select(attachment => new
                {
                    id = attachment.Id,
                    linkedInProfile = attachment.LinkedInProfile,
                    portfolioUrl = attachment.PortfolioUrl,
                    formalPhoto = attachment.FormalPhoto,
                    resume = attachment.Resume
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
