using AutoMapper;
using BaseCode.Data.ViewModels;
using BaseCode.Data;
using BaseCode.Domain.Contracts;
using BaseCode.Domain.Handlers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System;
using System.Linq;
using BaseCode.Domain;
using BaseCode.Data.Models;

namespace BaseCode.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PersonalInformationController : ControllerBase
    {
        private readonly IPersonalInformationService _personalInformationService;
        private readonly IMapper _mapper;

        public PersonalInformationController(IPersonalInformationService personalInformationService, IMapper mapper)
        {
            _personalInformationService = personalInformationService;
            _mapper = mapper;
        }

        [HttpGet]
        [ActionName("list")]
        public HttpResponseMessage GetPersonalInformationList([FromQuery] PersonalInformationSearchViewModel searchModel)
        {
            var responseData = _personalInformationService.FindPersonalInformations(searchModel);
            return Helper.ComposeResponse(HttpStatusCode.OK, responseData);
        }

        [HttpGet]
        [ActionName("getPersonalInformation")]
        public HttpResponseMessage GetPersonalInformation(int Id)
        {
            var personalInformation = _personalInformationService.FindPersonalInformation(Id);
            return personalInformation != null ? Helper.ComposeResponse(HttpStatusCode.OK, personalInformation) : Helper.ComposeResponse(HttpStatusCode.NotFound, Constants.PersonalInformation.PersonalInformationDoesNotExist);
        }

        [HttpPost]
        [ActionName("add")]
        public HttpResponseMessage PostPersonalInformation(PersonalInformationViewModel personalInformationModel)
        {
            if (!ModelState.IsValid)
            {
                return Helper.ComposeResponse(HttpStatusCode.BadRequest, Helper.GetModelStateErrors(ModelState));
            }

            try
            {
                var personalInformation = _mapper.Map<PersonalInformation>(personalInformationModel);
                var validationErrors = new PersonalInformationHandler(_personalInformationService).CanAdd(personalInformation);
                var validationResults = validationErrors as IList<ValidationResult> ?? validationErrors.ToList();

                if (validationResults.Any())
                {
                    ModelState.AddModelErrors(validationErrors);
                }

                if (ModelState.IsValid)
                {
                    _personalInformationService.Create(personalInformation);
                    return Helper.ComposeResponse(HttpStatusCode.OK, Constants.PersonalInformation.PersonalInformationSuccessAdd);
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
        public HttpResponseMessage PutPersonalInformation(int Id, PersonalInformationViewModel personalInformationViewModel)
        {
            if (!ModelState.IsValid)
            {
                return Helper.ComposeResponse(HttpStatusCode.BadRequest, Helper.GetModelStateErrors(ModelState));
            }

            try
            {
                var pi = _mapper.Map<PersonalInformation>(personalInformationViewModel);
                pi.Id = Id;
                var validationErrors = new PersonalInformationHandler(_personalInformationService).CanUpdate(pi);
                var validationResults = validationErrors as IList<ValidationResult> ?? validationErrors.ToList();

                if (validationResults.Any())
                {
                    ModelState.AddModelErrors(validationErrors);
                }

                if (ModelState.IsValid)
                {
                    _personalInformationService.Update(pi);
                    return Helper.ComposeResponse(HttpStatusCode.OK, Constants.PersonalInformation.PersonalInformationUpdateSuccess);
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
        public HttpResponseMessage SoftDeletePersonalInformation(int Id)
        {
            try
            {
                var validationErrors = new PersonalInformationHandler(_personalInformationService).CanDelete(Id);
                var validationResults = validationErrors as IList<ValidationResult> ?? validationErrors.ToList();

                if (validationResults.Any())
                {
                    ModelState.AddModelErrors(validationErrors);
                }

                if (ModelState.IsValid)
                {
                    var personalInformation = _personalInformationService.FindPersonalInformation(Id);
                    _personalInformationService.Delete(personalInformation);

                    return Helper.ComposeResponse(HttpStatusCode.OK, Constants.PersonalInformation.PersonalInformationSuccessDelete);
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