using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using LitHub.Common;
using AutoMapper;
using LitHub.Deploy;
using LitHub.DB.Model;
using System.Linq;

namespace LitHub
{
    public static class CustomExtensionMethods
    {
        public static IConfigurationBuilder AddDbConfiguration(this IConfigurationBuilder builder)
        {
            var configuration = builder.Build();
            var connectionString = configuration.GetConnectionString("MainConnection");
            builder.AddConfigDbProvider(options => options.UseNpgsql(connectionString), connectionString);
            return builder;
        }

        public static IConfigurationBuilder AddConfigDbProvider(
            this IConfigurationBuilder configuration, Action<DbContextOptionsBuilder> setup, string connectionString)
        {
            configuration.Add(new ConfigDbSource(setup, connectionString));
            return configuration;
        }

        public static IServiceCollection ConfigureAutoMapper(this IServiceCollection services)
        {
            var mappingConfig = new AutoMapper.MapperConfiguration(mc => mc.AddProfile(new MappingProfile()));

            var mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            return services;
        }

        public static ILoggingBuilder AddErrorNotifyLogger(
        this ILoggingBuilder builder)
        {
            builder.AddConfiguration();

            builder.Services.TryAddEnumerable(
                ServiceDescriptor.Singleton<ILoggerProvider, ErrorNotifyLoggerProvider>());

            LoggerProviderOptions.RegisterProviderOptions
                <ErrorNotifyLoggerConfiguration, ErrorNotifyLoggerProvider>(builder.Services);

            return builder;
        }

        public static ILoggingBuilder AddErrorNotifyLogger(
            this ILoggingBuilder builder,
            Action<ErrorNotifyLoggerConfiguration> configure)
        {
            builder.Services.Configure(configure);
            builder.AddErrorNotifyLogger();

            return builder;
        }
    }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
           
        }
    }

    public class ConfigDbSource : IConfigurationSource
    {
        private readonly Action<DbContextOptionsBuilder> _optionsAction;
        private string _connectionString;

        public ConfigDbSource(Action<DbContextOptionsBuilder> optionsAction, string connectionString)
        {
            _optionsAction = optionsAction;
            _connectionString = connectionString;
        }

        public Microsoft.Extensions.Configuration.IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            IDeployService deployService = new DeployService(_connectionString);
            return new ConfigDbProvider(_optionsAction, deployService);
        }
    }

    public class ConfigDbProvider : ConfigurationProvider
    {
        private readonly Action<DbContextOptionsBuilder> _options;
        private readonly IDeployService _deployService;

        public ConfigDbProvider(Action<DbContextOptionsBuilder> options,
            IDeployService deployService)
        {
            _options = options;
            _deployService = deployService;
        }

        public override void Load()
        {
            try
            {
                LoadInternal();
            }
            catch
            {
                try
                {
                    _deployService.Deploy().Wait();
                    LoadInternal();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        private void LoadInternal()
        {
            var builder = new DbContextOptionsBuilder<MainDbContext>();
            _options(builder);

            using (var context = new MainDbContext(builder.Options))
            {
                var items = context.Settings
                    .AsNoTracking()
                    .ToList();

                foreach (var item in items)
                {
                    Data.Add(item.ParamName, item.ParamValue);
                }
            }
        }
    }
}
