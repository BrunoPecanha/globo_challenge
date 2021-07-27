using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pecanha.Domain;
using Pecanha.Repository;
using Pecanha.Repository.Context;

namespace Pecanha.Service.Extensions {
    public static class ServiceExtensions {
        public static IServiceCollection RegisterServices(this IServiceCollection services, string connectionString) {

            services.AddDbContext<SceneContext>(o => o.UseSqlite(connectionString));
            services.AddTransient<ISceneContext, SceneContext>();

            //Entidadades
            services.AddTransient<ISceneService, SceneService>();
            services.AddTransient<ISceneRepository, SceneRepository>();        
            services.AddTransient<IRecordHistoryRepository, RecordHistoryRepository>();

            return services;
        }
    }
}
