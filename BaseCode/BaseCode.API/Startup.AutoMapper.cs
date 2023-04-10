using AutoMapper;
using BaseCode.Data.Models;
using BaseCode.Data.ViewModels;
using Microsoft.Extensions.DependencyInjection;

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

                cfg.CreateMap<JobRequirement, JobRequirementViewModel>();
                cfg.CreateMap<JobRequirementViewModel, JobRequirement>();

                cfg.CreateMap<JobDescription, JobDescriptionViewModel>();
                cfg.CreateMap<JobDescriptionViewModel, JobDescription>();
   
            });

            services.AddSingleton(Config.CreateMapper());
        }
    }
}