using CQRSlite.Bus;
using CQRSlite.Cache;
using CQRSlite.Commands;
using CQRSlite.Domain;
using CQRSlite.Events;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using AutoMapper;
using StackExchange.Redis;
using FluentValidation.AspNetCore;
using NcCqrsPoc.Web.Commands.Filters;

namespace NcCqrsPoc.Web.Commands.DependencyResolution
{
    public interface ICustomDependencyLoader { }

    public class CustomDependencyLoader : ICustomDependencyLoader
    {
        private CustomDependencyLoader() { }

        public static void LoadCustomDependency(IServiceCollection services, IMvcBuilder mvcBuilder)
        {
            //Commands, Events, Handlers
            // ....

            //Add Cqrs services
            services.AddSingleton<InProcessBus>(new CQRSlite.Bus.InProcessBus());
            services.AddTransient<ICommandSender>(y => y.GetService<InProcessBus>());
            services.AddTransient<IEventPublisher>(y => y.GetService<InProcessBus>());
            services.AddTransient<IHandlerRegistrar>(y => y.GetService<InProcessBus>());
            services.AddScoped<ISession, Session>();
            services.AddSingleton<IEventStore, CQRSLiteDemo.Nc.Domain.EventStore.InMemoryEventStore>(); //InmemoryEventStore from Domain proj Eventstore
            services.AddScoped<ICache, MemoryCache>();
            services.AddScoped<IRepository>(y => new CacheRepository(new Repository(y.GetService<IEventStore>()), y.GetService<IEventStore>(), y.GetService<ICache>()));

            //AutoMapper
            var profiles = from t in typeof(CustomDependencyLoader).Assembly.GetTypes()
                           where typeof(Profile).IsAssignableFrom(t)
                           select (Profile)Activator.CreateInstance(t);

            var config = new MapperConfiguration(cfg =>
            {
                foreach (var profile in profiles)
                {
                    cfg.AddProfile(profile);
                }
            });
            var mapper = config.CreateMapper();
            services.AddSingleton<IMapper>(mapper); // Todo, transient does not work!! Probably get rid of Automapper

            //Stackexchange.Redis
            services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect("localhost"));
            
            mvcBuilder.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());

            services.AddScoped<BadRequestActionFilter>();

            //serviceCollection.AddSingleton<IEmployeeRepo, EmployeeRepo>();
            //serviceCollection.AddSingleton<ISubsidiaryRepo, SubsidiaryRepo>();
        }
    }
}
