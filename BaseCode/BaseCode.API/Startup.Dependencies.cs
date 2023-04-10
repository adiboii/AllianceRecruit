using BaseCode.API.Authentication;
using BaseCode.Data;
using BaseCode.Data.Contracts;
using BaseCode.Data.Repositories;
using BaseCode.Domain.Contracts;
using BaseCode.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BaseCode.API
{
    public partial class Startup
    {
        private void ConfigureDependencies(IServiceCollection services)
        {            
            // Common
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ClaimsProvider, ClaimsProvider>();

            // Services
            services.AddScoped<ISubjectService, SubjectService>();
            services.AddScoped<IInstructorService, InstructorService>();    
            services.AddScoped<IClassService, ClassService>();
            services.AddScoped<IPersonalInformationService, PersonalInformationService>();
            services.AddScoped<IJobRequirementService, JobRequirementService>();
            // Repositories
            services.AddScoped<ISubjectRepository, SubjectRepository>();
            services.AddScoped<IInstructorRepository, InstructorRepository>();  
            services.AddScoped<IClassRepository, ClassRepository>();
            services.AddScoped<IPersonalInformationRepository, PersonalInformationRepository>();
            services.AddScoped<IJobRequirementRepository, JobRequirementRepository>();
        }
    }
}
