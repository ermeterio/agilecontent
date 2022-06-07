using CandidateTesting.DanielCarvalho.Application.Interface;
using CandidateTesting.DanielCarvalho.Infra.Data;
using CandidateTesting.DanielCarvalho.Domain.Interface;
using System;
using CandidateTesting.DanielCarvalho.Domain;

namespace CandidateTesting.DanielCarvalho.Application
{
    public class ConvertApplication : IConvertApplication
    {
        private readonly ILogFactory _logFactory;
        private readonly IFileApplication _fileApplication;
        public ConvertApplication(ILogFactory logFactory,
                                  IFileApplication fileApplication)
        {
            _logFactory = logFactory;
            _fileApplication = fileApplication;
        }
        public string ConvertLog(string[] args)
        {
            if (args.Length == 0)
                throw new Exception("This program not accept zero arguments.");

            if (args[0] != "convert")
                throw new Exception("This program has only the developed conversion function");

            if(args[0] == "convert" && args.Length != 3)
                throw new Exception("The conversion function needs 2 parameters: FileInput URL and OutputDirectory");

            _fileApplication.ValidatioFile(args[1]);
            
            _fileApplication.SaveFile(args[2], new Log().ReturnsLog(_logFactory.LogFactory(FileRepo.ReadFile(args[1]))));

            return "File successfully generated";
        }
    }
}
