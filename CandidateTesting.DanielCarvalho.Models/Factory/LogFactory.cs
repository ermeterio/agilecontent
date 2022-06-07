using CandidateTesting.DanielCarvalho.Domain.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace CandidateTesting.DanielCarvalho.Domain.Factory
{
    public class LogFactory : ILogFactory
    {        
        List<Log> ILogFactory.LogFactory(string log)
        {
            Console.WriteLine("Converting file log for \"Agora\" format");
            List<Log> ret = new List<Log>();
            string str = "";
            using (var reader = new StringReader(log))
            {
                while((str = reader.ReadLine()) != null)
                {
                    var item = str.Split('|');
                    if (item.Length < 5)
                        throw new Exception($" not contains a correct sequence");
                    try
                    {        
                        if(item[3].Split(' ').Length < 3)
                            throw new Exception($" block three does not match the required format, example: \"GET / robots.txt HTTP / 1.1\"");

                        var method = item[3].Split(' ')[0].Replace("\"","");

                        if (method != "GET" &&
                            method != "POST" &&
                            method != "PUT" &&
                            method != "DELETE" &&
                            method != "PATCH")
                            throw new Exception($" block three does not http valid method");

                        int statusCode = Convert.ToInt32(item[1]);
                        try
                        {
                            var stcode = (HttpStatusCode)statusCode;
                        }
                        catch
                        {
                            throw new Exception($" Status code incorrect");
                        }
                        ret.Add(new Log()
                        {
                            cacheStatus = item[2] == "INVALIDATE" ? "REFRESH_HIT" : item[2],
                            httpMethod = method,
                            responseSize = Convert.ToInt32(item[0]),
                            statusCode = statusCode,
                            timeTaken = Convert.ToDecimal(item[4].Replace(".",",")),
                            uriPath = item[3].Split(' ')[1]
                        });
                    }catch(Exception ex)
                    {
                        throw new Exception($"Exception line \"{item}\" : {ex.Message}");
                    }                    
                }
            }
            return ret;
        }
    }
}
