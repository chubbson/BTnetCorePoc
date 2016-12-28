using Microsoft.Extensions.DependencyInjection;
using NcCqrsPoc.Domain.ReadModel.Repos;
using NcCqrsPoc.Domain.ReadModel.Repos.Interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NcCqrsPoc.Web.Queries.DependencyResolution
{
    public interface ICustomDependencyLoader { }

    public class CustomDependencyLoader : ICustomDependencyLoader
    {
        private CustomDependencyLoader() { }

        public static void LoadCustomDependency(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect("localhost"));
            serviceCollection.AddSingleton<IEmployeeRepo, EmployeeRepo>();
            serviceCollection.AddSingleton<ISubsidiaryRepo, SubsidiaryRepo>();
        }
    }
}
