using System;
using System.Collections.Generic;
using System.Text;

namespace CandidateTesting.DanielCarvalho.Application.Interface
{
    public interface IFileApplication
    {
        void ValidatioFile(string urlFile);
        void SaveFile(string urlFile, string content);
    }
}
