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
using NcCqrsPoc.Domain.EventStore;
using NcCqrsPoc.Domain.ReadModel.Repos.Interfaces;
using NcCqrsPoc.Domain.ReadModel.Repos;
using CQRSlite.Config;
using NcCqrsPoc.Domain.CommandHandlers;
using Scrutor;
using NcCqrsPoc.Domain.EventHandlers;

namespace NcCqrsPoc.Web.Commands.DependencyResolution
{
    public interface ICustomDependencyLoader { }

    public class CustomDependencyLoader : ICustomDependencyLoader
    {
        private CustomDependencyLoader() { }

        public static void LoadCustomDependency(IServiceCollection services, IMvcBuilder mvcBuilder)
        {
            //CQRSlite.Web - sample
            services.AddMemoryCache();

            //Add Cqrs services
            //services.AddSingleton<InProcessBus>();
            services.AddSingleton<InProcessBus>(new CQRSlite.Bus.InProcessBus());
            services.AddSingleton<ICommandSender>(y => y.GetService<InProcessBus>());
            services.AddSingleton<IEventPublisher>(y => y.GetService<InProcessBus>());
            services.AddSingleton<IHandlerRegistrar>(y => y.GetService<InProcessBus>());
            services.AddScoped<ISession, Session>();
            services.AddSingleton<IEventStore, InMemoryEventStore>(); //InmemoryEventStore from Domain proj Eventstore
            services.AddScoped<ICache, MemoryCache>();
            services.AddScoped<IRepository>(y => new CacheRepository(new Repository(y.GetService<IEventStore>()), y.GetService<IEventStore>(), y.GetService<ICache>()));

            //CQRSlite.Web - sample
            //services.AddTransient<IReadModelFacade, ReadModelFacade>();

            //Commands, Events, Handlers
            //Scan for commandhandlers and eventhandlers
            services.Scan(scan => scan
                .FromAssemblies(typeof(SubsidiaryCommandHandler).GetTypeInfo().Assembly) // InventoryCommandHandlers).GetTypeInfo().Assembly)
                //.FromAssemblies(typeof(EmployeeCommandHandler).GetTypeInfo().Assembly)// InventoryCommandHandlers).GetTypeInfo().Assembly)
                    .AddClasses(classes => classes.Where(x => {
                        var allInterfaces = x.GetInterfaces();
                        return
                            allInterfaces.Any(y => y.GetTypeInfo().IsGenericType && y.GetTypeInfo().GetGenericTypeDefinition() == typeof(ICommandHandler<>)) ||
                            allInterfaces.Any(y => y.GetTypeInfo().IsGenericType && y.GetTypeInfo().GetGenericTypeDefinition() == typeof(IEventHandler<>));
                    }))
                    .AsSelf()
                    .WithTransientLifetime()
            );

            //AutoMapper - scans Profile Classes defined in AutoMapperConfig folder
            services.AddAutoMapper(typeof(Startup));

            //Stackexchange.Redis
            services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect("localhost"));
            
            //Fluent.Validation
            mvcBuilder.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());

            services.AddSingleton<IEmployeeRepo, EmployeeRepo>();
            services.AddSingleton<ISubsidiaryRepo, SubsidiaryRepo>();

            //Register bus
            var serviceProvider = services.BuildServiceProvider();
            var dependencyResolver = new CQRSliteDependencyResolver(serviceProvider);
            var registrar = new BusRegistrar(dependencyResolver);
            registrar.Register(typeof(SubsidiaryCommandHandler));
        }
    }
}
