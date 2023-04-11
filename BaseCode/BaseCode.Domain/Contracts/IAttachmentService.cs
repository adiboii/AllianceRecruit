using BaseCode.Data.ViewModels.Common;
using BaseCode.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseCode.Data.Models;

namespace BaseCode.Domain.Contracts
{
    public interface IAttachmentService
    {
        void Create(Attachment attachment);
        void Update(Attachment attachment);
        void Delete(Attachment attachment);
        bool AttachmentExists(Attachment attachment);
        Attachment FindAttachment(int Id);
        IQueryable<Attachment> RetrieveAll();
        ListViewModel FindAttachments(AttachmentSearchViewModel searchModel);
    }
}
