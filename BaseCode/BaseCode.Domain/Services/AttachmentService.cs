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
    public class AttachmentService : IAttachmentService
    {
        private readonly IAttachmentRepository _attachmentRepository;

        public AttachmentService(IAttachmentRepository attachmentRepository)
        {
            _attachmentRepository = attachmentRepository;
        }
        public void Create(Attachment attachment)
        {
            _attachmentRepository.Create(attachment);
        }

        public void Update(Attachment attachment)
        {
            _attachmentRepository.Update(attachment);   
        }

        public void Delete(Attachment attachment)
        {
            _attachmentRepository.Delete(attachment);
        }
        public bool AttachmentExists(Attachment attachment)
        {
            return _attachmentRepository.AttachmentExists(attachment);
        }

        public Attachment FindAttachment(int Id)
        {
            return _attachmentRepository.FindAttachment(Id);
        }

        public IQueryable<Attachment> RetrieveAll()
        {
            return _attachmentRepository.RetrieveAll();
        }

        public ListViewModel FindAttachments(AttachmentSearchViewModel searchModel)
        {
            return _attachmentRepository.FindAttachments(searchModel);
        }

     

       
    }
}
