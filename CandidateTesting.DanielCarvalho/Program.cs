using System;
using System.IO;
using CandidateTesting.DanielCarvalho.Application.Interface;
using CandidateTesting.DanielCarvalho.Domain;
using CandidateTesting.DanielCarvalho.Infra.CrossCutting.IoC;
using CandidateTesting.DanielCarvalho.Infra.Data;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace CandidateTesting.DanielCarvalho
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            DependecyInjection.ConfigureServices(serviceCollection);
            var serviceProvider = serviceCollection.BuildServiceProvider();
            using (StreamReader r = new StreamReader("appConfiguration.json"))
            {
                AppInformationData item = JsonConvert.DeserializeObject<AppInformationData>(r.ReadToEnd());                
                StaticAppInformationData.Version = item.Version;
                StaticAppInformationData.NameProvider = item.NameProvider;
            }
            try
            {
                var eventService = serviceProvider.GetService<IConvertApplication>();
                Console.Write(eventService.ConvertLog(args));
            }
            catch(Exception ex)
            {
                Console.WriteLine($"On error ocurred: {ex.Message}");
            }            
        }        
    }
}
