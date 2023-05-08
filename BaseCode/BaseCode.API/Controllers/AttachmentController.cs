using AutoMapper;
using BaseCode.Data;
using BaseCode.Data.Models;
using BaseCode.Data.ViewModels;
using BaseCode.Domain;
using BaseCode.Domain.Contracts;
using BaseCode.Domain.Handlers;
using BaseCode.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace BaseCode.API.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AttachmentController : ControllerBase
    {
        private readonly IAttachmentService _attachmentService;
        private readonly IMapper _mapper;

        public AttachmentController(IAttachmentService attachmentService, IMapper mapper)
        {
            _attachmentService = attachmentService;
            _mapper = mapper;
        }

        [HttpGet]
        [ActionName("list")]
        public HttpResponseMessage GetAttachmentList([FromQuery] AttachmentSearchViewModel searchModel)
        {
            var responseData = _attachmentService.FindAttachments(searchModel);
            return Helper.ComposeResponse(HttpStatusCode.OK, responseData);
        }

        [HttpGet]
        [ActionName("getAttachment")]
        public HttpResponseMessage GetAttachment(int Id)
        {
            var personalInformation = _attachmentService.FindAttachment(Id);
            return personalInformation != null ? Helper.ComposeResponse(HttpStatusCode.OK, personalInformation) : Helper.ComposeResponse(HttpStatusCode.NotFound, Constants.Attachment.AttachmentDoesNotExist);
        }

        [HttpPost]
        [ActionName("add")]
        public HttpResponseMessage PostAttachment(AttachmentViewModel attachmentViewModel)
        {
            if (!ModelState.IsValid)
            {
                return Helper.ComposeResponse(HttpStatusCode.BadRequest, Helper.GetModelStateErrors(ModelState));
            }

            try
            {
                var attachment = _mapper.Map<Attachment>(attachmentViewModel);
                var validationErrors = new AttachmentHandler(_attachmentService).CanAdd(attachment);
                var validationResults = validationErrors as IList<ValidationResult> ?? validationErrors.ToList();

                if (validationResults.Any())
                {
                    ModelState.AddModelErrors(validationErrors);
                }

                if (ModelState.IsValid)
                {
                    _attachmentService.Create(attachment);
                    int newId = attachment.Id;
                    var response = Helper.ComposeResponse(HttpStatusCode.OK, Constants.Attachment.AttachmentSuccessAdd + " with ID " + newId);
                    return response;
                }
            }
            catch (Exception ex)
            {
       
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return Helper.ComposeResponse(HttpStatusCode.BadRequest, Helper.GetModelStateErrors(ModelState));
        }




        [HttpPut]
        [ActionName("edit")]
        public HttpResponseMessage PutAttachment(int Id, AttachmentViewModel attachmentViewModel)
        {
            if (!ModelState.IsValid)
            {
                return Helper.ComposeResponse(HttpStatusCode.BadRequest, Helper.GetModelStateErrors(ModelState));
            }

            try
            {
                var attachment = _mapper.Map<Attachment>(attachmentViewModel);
                attachment.Id = Id;
                var validationErrors = new AttachmentHandler(_attachmentService).CanUpdate(attachment);
                var validationResults = validationErrors as IList<ValidationResult> ?? validationErrors.ToList();

                if (validationResults.Any())
                {
                    ModelState.AddModelErrors(validationErrors);
                }

                if (ModelState.IsValid)
                {
                    _attachmentService.Update(attachment);
                    return Helper.ComposeResponse(HttpStatusCode.OK, Constants.Attachment.AttachmentSuccessUpdate);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return Helper.ComposeResponse(HttpStatusCode.BadRequest, Helper.GetModelStateErrors(ModelState));
        }

        [HttpDelete]
        [ActionName("delete")]
        public HttpResponseMessage DeleteAttachment(int Id)
        {
            try
            {
                var validationErrors = new AttachmentHandler(_attachmentService).CanDelete(Id);
                var validationResults = validationErrors as IList<ValidationResult> ?? validationErrors.ToList();

                if (validationResults.Any())
                {
                    ModelState.AddModelErrors(validationErrors);
                }

                if (ModelState.IsValid)
                {
                    var jobRequirement = _attachmentService.FindAttachment(Id);
                    _attachmentService.Delete(jobRequirement);

                    return Helper.ComposeResponse(HttpStatusCode.OK, Constants.Attachment.AttachmentSuccessDelete);
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return Helper.ComposeResponse(HttpStatusCode.BadRequest, Helper.GetModelStateErrors(ModelState));
        }
    }
}

