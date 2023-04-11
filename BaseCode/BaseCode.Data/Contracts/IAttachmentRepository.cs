using BaseCode.Data.Models;
using BaseCode.Data.ViewModels;
using BaseCode.Data.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseCode.Data.Contracts
{
    public interface IAttachmentRepository
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
