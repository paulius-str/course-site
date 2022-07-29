using Api.Contract;
using Api.Repository;
using Api.Service;
using Api.Service.Contract;
using AutoMapper;
using Microsoft.AspNetCore.Http.Features;

namespace API.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IServiceManager, ServiceManager>();
            //services.AddScoped<IHttpContextAccessor, HttpContextAccessor>();
        }

        public static void AddRepository(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryManager, RepositoryManager>();
        }

        public static void AddMapper(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(config =>
            {
                config.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        public static void ConfigureFormInput(this IServiceCollection services)
        {
            services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = long.MaxValue;
            });
        }
    }
}
