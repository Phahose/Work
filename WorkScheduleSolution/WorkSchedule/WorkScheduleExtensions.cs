
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WorkSchedule.BLL;
using WorkSchedule.DAL;

namespace WorkSchedule
{
    public static class WorkScheduleExtensions
    {
        public static void WorkScheduleBackendDependencies(this IServiceCollection services,
           Action<DbContextOptionsBuilder> options)
        {
            //  Register the DBContext class in Chinook2018 with the service collection
            services.AddDbContext<WorkScheduleContext>(options);

            services.AddTransient<EmployeeServices>((serviceProvider) =>
            {
                var context = serviceProvider.GetRequiredService<WorkScheduleContext>();
                return new EmployeeServices(context);
            });
            services.AddTransient<FindEmployees>((serviceProvider) =>
            {
                var context = serviceProvider.GetRequiredService<WorkScheduleContext>();
                return new FindEmployees(context);
            });
        }
    }
}