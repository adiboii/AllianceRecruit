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
            services.AddScoped<IPersonalInformationService, PersonalInformationService>();
            services.AddScoped<IJobRequirementService, JobRequirementService>();
            services.AddScoped<IJobDescriptionService, JobDescriptionService>();
            services.AddScoped<IAttachmentService, AttachmentService>();
            services.AddScoped<IJobService, JobService>();
            services.AddScoped<IApplicationService, ApplicationService>();
            services.AddScoped<IUserService, UserService>();

            // Repositories
            services.AddScoped<IPersonalInformationRepository, PersonalInformationRepository>();
            services.AddScoped<IJobRequirementRepository, JobRequirementRepository>();
            services.AddScoped<IJobDescriptionRepository, JobDescriptionRepository>();
            services.AddScoped<IAttachmentRepository, AttachmentRepository>();
            services.AddScoped<IJobRepository, JobRepository>();
            services.AddScoped<IApplicationRepository, ApplicationRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
