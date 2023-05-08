using BaseCode.Data;
using BaseCode.Data.Models;
using BaseCode.Domain.Contracts;
using BaseCode.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseCode.Domain.Handlers
{
    public class AttachmentHandler
    {
        private readonly IAttachmentService _attachmentService;

        public AttachmentHandler(IAttachmentService attachmentService)
        {
            _attachmentService = attachmentService;
        }

        public IEnumerable<ValidationResult> CanAdd(Attachment attachment)
        {
            var validationErrors = new List<ValidationResult>();

            if (attachment == null)
            {
                // Check if has duplicate already.
                if (_attachmentService.AttachmentExists(attachment))
                {
                    validationErrors.Add(new ValidationResult(Constants.Attachment.AttachmentExists));
                }

            }
            return validationErrors;
        }

        public IEnumerable<ValidationResult> CanDelete(int Id)
        {
            var validationErrors = new List<ValidationResult>();
            var attachment = _attachmentService.FindAttachment(Id);
            if (attachment == null)
            {
                validationErrors.Add(new ValidationResult(Constants.Attachment.AttachmentExists));
            }

            return validationErrors;
        }

        public IEnumerable<ValidationResult> CanUpdate(Attachment attachment)
        {
            var validationErrors = new List<ValidationResult>();
            if (attachment == null)
            {
                validationErrors.Add(new ValidationResult(Constants.Attachment.AttachmentDoesNotExist));
            }
            else
            {
                // Check if has duplicate already.
                if (_attachmentService.AttachmentExists(attachment))
                {
                    validationErrors.Add(new ValidationResult(Constants.Attachment.AttachmentExists));
                }
            }

            return validationErrors;
        }
    }

}
