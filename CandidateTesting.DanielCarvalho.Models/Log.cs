using CandidateTesting.DanielCarvalho.Infra.Data;
using System;
using System.Collections.Generic;

namespace CandidateTesting.DanielCarvalho.Domain
{
    public class Log
    {
        public string httpMethod { get; set; }
        public int statusCode { get; set; }
        public string uriPath { get; set; }
        public decimal timeTaken { get; set; }
        public int responseSize { get; set; }
        public string cacheStatus { get; set; }

        public string ReturnsLog(List<Log> logs)
        {
            Console.WriteLine("Creating log file...");
            string header = $"#Version: {StaticAppInformationData.Version} \r\n";
            header += $"#Date: {DateTime.UtcNow} \r\n";
            header += $"#Fields: provider http-method status-code uri-path time-taken response-size cache-status \r\n";
            string body = "";
            foreach (var log in logs)            
                body += $"\"{StaticAppInformationData.NameProvider}\" {log.httpMethod} {log.statusCode} {log.uriPath} {Decimal.Round(log.timeTaken)} {log.responseSize} {log.cacheStatus} \r\n";
            
            return header + body;
        }
    }
}
