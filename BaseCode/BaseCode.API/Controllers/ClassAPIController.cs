using AutoMapper;
using BaseCode.Data;
using BaseCode.Data.Models;
using BaseCode.Data.ViewModels;
using BaseCode.Domain.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Security.Claims;
using BaseCode.Data.ViewModels.Common;
using BaseCode.Domain.Handlers;
using System.Collections;
using BaseCode.Domain;
using System.Linq;
using System.Collections.Generic;
using BaseCode.Domain.Services;

namespace BaseCode.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ClassAPIController : ControllerBase
    {
        private readonly IClassService _classService;
        private readonly IMapper _mapper;

        public ClassAPIController(IClassService ClassService, IMapper mapper)
        {
            _classService = ClassService;
            _mapper = mapper;
        }

        [HttpGet]
        [ActionName("list")]
        public HttpResponseMessage GetClassList([FromQuery] ClassSearchViewModel searchModel)
        {
            var responseData = _classService.FindClasses(searchModel);
            return Helper.ComposeResponse(HttpStatusCode.OK, responseData);
        }

        [HttpGet]
        [ActionName("getClass")]
        public HttpResponseMessage GetClass(int Id)
        {
            var Class = _classService.FindClass(Id);
            return Class != null ? Helper.ComposeResponse(HttpStatusCode.OK, Class) : Helper.ComposeResponse(HttpStatusCode.NotFound, Constants.Class.ClassDoesNotExist);
        }

        [HttpPost]
        [ActionName("add")]
        public HttpResponseMessage PostClass(ClassViewModel ClassModel)
        {

            if(!ModelState.IsValid) 
            { 
                return Helper.ComposeResponse(HttpStatusCode.BadRequest, Helper.GetModelStateErrors(ModelState));  
            }
                
            try
            {
                var Class =  _mapper.Map<Class>(ClassModel);
                var validationErrors = new ClassHandler(_classService).CanAdd(Class);
                var validationResults = validationErrors as IList<ValidationResult> ?? validationErrors.ToList();

                if (validationResults.Any())
                {
                    ModelState.AddModelErrors(validationErrors);
                }

                if (ModelState.IsValid)
                {
                    _classService.Create(Class);
                    return Helper.ComposeResponse(HttpStatusCode.OK, Constants.Class.ClassSuccessAdd);

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
        public HttpResponseMessage PutClass(int Id, ClassViewModel classViewModel)
        {
            if (!ModelState.IsValid) return Helper.ComposeResponse(HttpStatusCode.BadRequest, Helper.GetModelStateErrors(ModelState));

            try
            {
                var c = _mapper.Map<Class>(classViewModel);
                c.Id = Id;
                var validationErrors = new ClassHandler(_classService).CanUpdate(c);
                var validationResults = validationErrors as IList<ValidationResult> ?? validationErrors.ToList();


                if (validationResults.Any())
                {
                    ModelState.AddModelErrors(validationErrors);
                }

                if (ModelState.IsValid)
                {
                    _classService.Update(c);
                    return Helper.ComposeResponse(HttpStatusCode.OK, Constants.Class.ClassSuccessEdit);
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
        public HttpResponseMessage SoftDeleteClass(int Id)
        {
            try
            {
                var validationErrors = new ClassHandler(_classService).CanDelete(Id);
                var validationResults = validationErrors as IList<ValidationResult> ?? validationErrors.ToList();

                if (validationResults.Any())
                {
                    ModelState.AddModelErrors(validationErrors);
                }

                if (ModelState.IsValid)
                {
                    var Class = _classService.FindClass(Id);
                    _classService.Delete(Class);

                    return Helper.ComposeResponse(HttpStatusCode.OK, Constants.Class.ClassSuccessDelete);
                }

            } catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return Helper.ComposeResponse(HttpStatusCode.BadRequest, Helper.GetModelStateErrors(ModelState));
        }

       
    }
}