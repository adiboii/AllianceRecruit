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
                cfg.CreateMap<Subject, SubjectViewModel>();
                cfg.CreateMap<SubjectViewModel, Subject>();
    
                cfg.CreateMap<Instructor, InstructorViewModel>();
                cfg.CreateMap<InstructorViewModel, Instructor>();
        
                cfg.CreateMap<Class, ClassViewModel>();
                cfg.CreateMap<ClassViewModel, Class>();
                cfg.CreateMap<ClassViewModel, Instructor>();

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

                cfg.CreateMap<JobRequirement, JobRequirementViewModel>()
                    .ForMember(dest => dest.Requirement, opt => opt.MapFrom(src => src.Requirement));
                cfg.CreateMap<JobRequirementViewModel, JobRequirement>()
                    .ForMember(dest => dest.Requirement, opt => opt.MapFrom(src => src.Requirement));

                cfg.CreateMap<JobDescription, JobDescriptionViewModel>()
                    .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));

                cfg.CreateMap<JobDescriptionViewModel, JobDescription>()
                   .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
            });

            services.AddSingleton(Config.CreateMapper());
        }
    }
}