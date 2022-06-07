using System;
using System.IO;
using CandidateTesting.DanielCarvalho.Application.Interface;
using CandidateTesting.DanielCarvalho.Domain;
using CandidateTesting.DanielCarvalho.Infra.CrossCutting.IoC;
using CandidateTesting.DanielCarvalho.Infra.Data;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace CandidateTesting.DanielCarvalho.Test
{
    internal class Program
    {
        static void Main(string[] args)
        {            
            using (StreamReader r = new StreamReader("appConfiguration.json"))
            {
                AppInformationData item = JsonConvert.DeserializeObject<AppInformationData>(r.ReadToEnd());
                StaticAppInformationData.Version = item.Version;
                StaticAppInformationData.NameProvider = item.NameProvider;
            }
            new TestCase();
        }
    }
}
