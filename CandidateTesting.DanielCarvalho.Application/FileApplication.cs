using CandidateTesting.DanielCarvalho.Application.Interface;
using CandidateTesting.DanielCarvalho.Infra.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CandidateTesting.DanielCarvalho.Application
{
    public class FileApplication : IFileApplication
    {
        public void SaveFile(string urlFile, string content)
        {
            try
            {
                Console.WriteLine("Initialize proccess file");
                if (urlFile.StartsWith("."))
                    urlFile = $"{Directory.GetCurrentDirectory()}/{urlFile.Replace("./", "")}";
                var splFile = urlFile.Split('/');
                string file = splFile.LastOrDefault();
                if (!FileRepo.DirectoryExists(urlFile.Replace(file, "")))
                    FileRepo.CreateDirectory(urlFile.Replace(file, ""));
                FileRepo.SaveFile(urlFile, content);
            }catch(Exception ex)
            {
                throw new Exception($"Error in FileApplication, method SaveFile: {ex.Message}");
            }            
        }

        public void ValidatioFile(string urlFile)
        {
            if (!FileRepo.URLExists(urlFile))
                throw new Exception("File not found");
            else if (!System.IO.Path.GetExtension(urlFile).Contains(".txt"))
                throw new Exception("Only .txt files are allowed");
            string file = FileRepo.ReadFile(urlFile);
            if (file.Length == 0)
                throw new Exception("File empty");
        }
    }
}
