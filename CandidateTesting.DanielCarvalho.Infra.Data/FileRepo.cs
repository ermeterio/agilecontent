using System;
using System.IO;
using System.Net;

namespace CandidateTesting.DanielCarvalho.Infra.Data
{
    public static class FileRepo
    {
        static public bool URLExists(string url)
        {
            try
            {
                WebClient myWebClient = new WebClient();
                var data = myWebClient.DownloadString(url);
            }
            catch
            {
                return false;
            }
            return true;
        }
        public static string ReadFile(string urlFile)
        {
            Console.WriteLine($"Init downloading file in {urlFile}");
            string contents;
            using (var wc = new WebClient())
                contents = wc.DownloadString(urlFile);
            Console.WriteLine($"Finish downloading file in {urlFile}");
            return contents;
        }
        public static bool DirectoryExists(string directory)
        {
            if (!Directory.Exists(directory))
                //if (!Directory.Exists($"{Directory.GetCurrentDirectory()}{directory}"))
                return false;
            return true;
        }
        public static void CreateDirectory(string directory)
        {
            try
            {
                Console.WriteLine($"Creating directory in {directory}");
                Directory.CreateDirectory(directory);
            }
            catch (Exception ex)
            {
                throw new Exception($"Create Directory error: {ex.Message}");
            }
        }
        public static void SaveFile(string urlFile, string content)
        {
            try
            {
                Console.WriteLine($"Saving file in {urlFile}");
                File.WriteAllText(urlFile, content);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in FileRepo.SaveFile: {ex.Message}");
            }
        }
    }
}
