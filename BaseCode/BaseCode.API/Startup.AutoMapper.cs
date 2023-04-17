using AutoMapper;
using BaseCode.Data.Models;
using BaseCode.Data.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace BaseCode.API
{
    public partial class Startup
    {
        private void ConfigureMapper(IServiceCollection services)
        {
            var Config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<PersonalInformation, PersonalInformationViewModel>();
                cfg.CreateMap<PersonalInformationViewModel, PersonalInformation>();

                
                cfg.CreateMap<Attachment, AttachmentViewModel>();
                cfg.CreateMap<AttachmentViewModel, Attachment>();

                cfg.CreateMap<Job, JobViewModel>()
                    .ForMember(dest => dest.JobRequirements, opt => opt.MapFrom(src => src.JobRequirements.Select(r => r.Requirement)))
                    .ForMember(dest => dest.JobDescriptions, opt => opt.MapFrom(src => src.JobDescriptions.Select(d => d.Description)));

                cfg.CreateMap<JobViewModel, Job>()
                    .ForMember(dest => dest.JobRequirements, opt => opt.MapFrom(src => src.JobRequirements.Select(r => new JobRequirement { Requirement = r })))
                    .ForMember(dest => dest.JobDescriptions, opt => opt.MapFrom(src => src.JobDescriptions.Select(d => new JobDescription { Description = d })));

                cfg.CreateMap<JobRequirement, JobRequirementViewModel>();
                cfg.CreateMap<JobRequirementViewModel, JobRequirement>();

                cfg.CreateMap<JobDescription, JobDescriptionViewModel>();
                cfg.CreateMap<JobDescriptionViewModel, JobDescription>();

                cfg.CreateMap<Application, ApplicationViewModel>();
                cfg.CreateMap<ApplicationViewModel, Application>();

            });

            services.AddSingleton(Config.CreateMapper());
        }
    }
}