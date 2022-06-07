using CandidateTesting.DanielCarvalho.Application.Interface;
using CandidateTesting.DanielCarvalho.Domain;
using CandidateTesting.DanielCarvalho.Infra.CrossCutting.IoC;
using CandidateTesting.DanielCarvalho.Infra.Data;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.IO;
using Xunit;

namespace CadidateTesting.DanielCarvalho.TestProject
{
    public class UnitTest1
    {
        private readonly IConvertApplication _convertApplication;
        public UnitTest1()
        {
            using (StreamReader r = new StreamReader("appConfiguration.json"))
            {
                AppInformationData item = JsonConvert.DeserializeObject<AppInformationData>(r.ReadToEnd());
                StaticAppInformationData.Version = item.Version;
                StaticAppInformationData.NameProvider = item.NameProvider;
            }
            var serviceCollection = new ServiceCollection();
            DependecyInjection.ConfigureServices(serviceCollection);
            var serviceProvider = serviceCollection.BuildServiceProvider();
            _convertApplication = serviceProvider.GetService<IConvertApplication>();

            TestCaseSuccess();
            TestCaseNotImplementedFunction();
            TestCaseExccededParameters();
            TestCaseFileEmpty();
            TestCaseHeaderIncorrect();
            TestCaseIncorrectSequence();
            TestCaseIncorrectHttpMethod();
            TestCaseIncorrectStatusCode();
            TestCaseOutputIncorrectFormat();
        }
        [Fact]
        private void TestCaseOutputIncorrectFormat()
        {
            string _returnOfTest = "";
            try
            {
                string[] args = new string[3];
                args[0] = "convert";
                args[1] = "https://s3.amazonaws.com/uux-itaas-static/minha-cdn-logs/input-01.txt";
                args[2] = "output/outest/minhaCdn1.jpg";
                _returnOfTest = _convertApplication.ConvertLog(args);
            }
            catch (Exception ex)
            {
                _returnOfTest = $"On error ocurred: {ex.Message}";
            }
            Xunit.Assert.Equal("On error ocurred: Error in FileApplication, method SaveFile: Only .txt files are allowed in output", _returnOfTest);
        }

        [Fact]
        private void TestCaseSuccess()
        {
            string _returnOfTest = "";
            try
            {
                string[] args = new string[3];
                args[0] = "convert";
                args[1] = "https://s3.amazonaws.com/uux-itaas-static/minha-cdn-logs/input-01.txt";
                args[2] = "./output/minhaCdn1.txt";
                _returnOfTest = _convertApplication.ConvertLog(args);
            }
            catch (Exception ex)
            {
                _returnOfTest = $"On error ocurred: {ex.Message}";
            }
            Assert.Equal("File successfully generated", _returnOfTest);
        }

        [Fact]
        public void TestCaseIncorrectSequence()
        {
            string _returnOfTest = "";
            try
            {
                string[] args = new string[3];
                args[0] = "convert";
                args[1] = "https://raw.githubusercontent.com/ermeterio/agilecontent/main/File_Test_Incorrect_Sequence.txt";
                args[2] = "./convert/test.txt";
                _returnOfTest = _convertApplication.ConvertLog(args);
            }
            catch (Exception ex)
            {
                _returnOfTest = $"On error ocurred: {ex.Message}";
            }
            Assert.Equal("On error ocurred: Line 2 : Not contains a correct sequence", _returnOfTest);
        }

        [Fact]
        public void TestCaseExccededParameters()
        {
            string _returnOfTest = "";
            try
            {
                string[] args = new string[4];
                args[0] = "convert";
                args[1] = "https://s3.amazonaws.com/uux-itaas-static/minha-cdn-logs/input-01.txt";
                args[2] = "./convert/test.txt";
                args[3] = "arg0";
                _returnOfTest = _convertApplication.ConvertLog(args);
            }
            catch (Exception ex)
            {
                _returnOfTest = $"On error ocurred: {ex.Message}";
            }
            Assert.Equal("On error ocurred: The conversion function needs 2 parameters: FileInput URL and OutputDirectory", _returnOfTest);
        }

        [Fact]
        public void TestCaseNotImplementedFunction()
        {
            string _returnOfTest = "";
            try
            {
                string[] args = new string[3];
                args[0] = "extract";
                args[1] = "https://s3.amazonaws.com/uux-itaas-static/minha-cdn-logs/input-01.txt";
                args[2] = "./convert/test.txt";
                _returnOfTest = _convertApplication.ConvertLog(args);
            }
            catch (Exception ex)
            {
                _returnOfTest = $"On error ocurred: {ex.Message}";
            }
            Assert.Equal("On error ocurred: This program has only the developed conversion function", _returnOfTest);
        }

        [Fact]
        public void TestCaseIncorrectStatusCode()
        {
            string _returnOfTest = "";
            try
            {
                string[] args = new string[3];
                args[0] = "convert";
                args[1] = "https://raw.githubusercontent.com/ermeterio/agilecontent/main/File_Test_Incorrect_Status_Code.txt";
                args[2] = "./convert/test.txt";
                _returnOfTest = _convertApplication.ConvertLog(args);
            }
            catch (Exception ex)
            {
                _returnOfTest = $"On error ocurred: {ex.Message}";
            }
            Assert.Equal("On error ocurred: Line 1 : Status code incorrect", _returnOfTest);
        }

        [Fact]
        public void TestCaseIncorrectHttpMethod()
        {
            string _returnOfTest = "";
            try
            {
                string[] args = new string[3];
                args[0] = "convert";
                args[1] = "https://raw.githubusercontent.com/ermeterio/agilecontent/main/File_Test_Incorrect_Method.txt";
                args[2] = "./convert/test.txt";
                _returnOfTest = _convertApplication.ConvertLog(args);
            }
            catch (Exception ex)
            {
                _returnOfTest = $"On error ocurred: {ex.Message}";
            }
            Assert.Equal("On error ocurred: Line 2 : Block three does not http valid method", _returnOfTest);
        }

        [Fact]
        public void TestCaseHeaderIncorrect()
        {
            string _returnOfTest = "";
            try
            {
                string[] args = new string[3];
                args[0] = "convert";
                args[1] = "https://raw.githubusercontent.com/ermeterio/agilecontent/main/File_Test_Header_Incorrect.txt";
                args[2] = "./convert/test.txt";
                _returnOfTest = _convertApplication.ConvertLog(args);
            }
            catch (Exception ex)
            {
                _returnOfTest = $"On error ocurred: {ex.Message}";
            }
            Assert.Equal("On error ocurred: Line 1 : Block three does not match the required format, example: \"GET / robots.txt HTTP / 1.1\"", _returnOfTest);
        }

        [Fact]
        public void TestCaseFileEmpty()
        {
            string _returnOfTest = "";
            try
            {
                string[] args = new string[3];
                args[0] = "convert";
                args[1] = "https://raw.githubusercontent.com/ermeterio/agilecontent/main/File_Test_Empty.txt";
                args[2] = "./convert/test.txt";
                _returnOfTest = _convertApplication.ConvertLog(args);
            }
            catch (Exception ex)
            {
                _returnOfTest = $"On error ocurred: {ex.Message}";
            }
            Assert.Equal("On error ocurred: File empty", _returnOfTest);
        }
    }
}
