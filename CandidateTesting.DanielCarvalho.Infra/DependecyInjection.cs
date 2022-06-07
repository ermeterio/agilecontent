using CandidateTesting.DanielCarvalho.Application;
using CandidateTesting.DanielCarvalho.Application.Interface;
using CandidateTesting.DanielCarvalho.Domain.Factory;
using CandidateTesting.DanielCarvalho.Domain.Interface;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CandidateTesting.DanielCarvalho.Infra.CrossCutting.IoC
{
    public static class DependecyInjection
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IConvertApplication, ConvertApplication>();
            services.AddScoped<ILogFactory, LogFactory>();
            services.AddScoped<IFileApplication, FileApplication>();
        }
    }
}
