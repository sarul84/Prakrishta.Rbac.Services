using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RbacService.Domain.Interfaces.Repositories;
using RbacService.Infrastructure.Data;
using RbacService.Infrastructure.Interceptors;
using RbacService.Infrastructure.Repositories;

namespace RbacService.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDbContext(
        this IServiceCollection services,
        string connectionString)
        {
            services.AddHttpContextAccessor();

            services.AddDbContext<RbacDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
                options.AddInterceptors(new AuditInterceptor(
                    services.BuildServiceProvider().GetRequiredService<IHttpContextAccessor>()
                ));
            });

            services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IPermissionRepository, PermissionRepository>();
            services.AddScoped<IRolePermissionRepository, RolePermissionRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IEnumerationRepository, EnumerationRepository>();
            services.AddScoped<IMaskingRuleRepository, MaskingRuleRepository>();
            services.AddScoped<IOrgAccessMappingRepository, OrgAccessMappingRepository>();
            services.AddScoped<IOrganizationRepository, OrganizationRepository>();
            services.AddScoped<IPiiAccessLogRepository, PiiAccessLogRepository>();
            services.AddScoped<IPiiFieldRepository, PiiFieldRepository>();
            services.AddScoped<IRoleMaskingRuleRepository, RoleMaskingRuleRepository>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            return services;
        }
    }
}
