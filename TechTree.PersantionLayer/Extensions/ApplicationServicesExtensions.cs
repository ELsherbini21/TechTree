using TechTree.BLL.Interfaces;
using TechTree.BLL.Repositories;
using TechTree.PersantionLayer.Helpers;
using TechTree.PersantionLayer.Helpers.interfaces;

namespace TechTree.PersantionLayer.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ICategoryItemRepository, CategoryItemRepository>();

            services.AddScoped<ICategoryRepository, CategoryRepository>();

            services.AddScoped<IContentRepository, ContentRepository>();

            services.AddScoped<IMediaTypeRepository, MediaTypeRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IUserCategoryRepository, UserCategoryRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IDataFunctions, DataFunctions>();

            services.AddAutoMapper(map =>
                    map.AddProfile(new MappingProfiles())
                );

            return services;
        }
    }
}
