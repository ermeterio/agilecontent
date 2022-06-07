using CandidateTesting.DanielCarvalho.Application.Interface;
using CandidateTesting.DanielCarvalho.Infra.CrossCutting.IoC;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CandidateTesting.DanielCarvalho.Test
{
    [TestClass]
    internal class TestCase
    {
        private readonly IConvertApplication _convertApplication;
        public TestCase()
        {
            var serviceCollection = new ServiceCollection();
            DependecyInjection.ConfigureServices(serviceCollection);
            var serviceProvider = serviceCollection.BuildServiceProvider();
            _convertApplication = serviceProvider.GetService<IConvertApplication>();

            TestCaseNotImplementedFunction();
            TestCaseExccededParameters();
            TestCaseFileEmpty();
            TestCaseHeaderIncorrect();
            TestCaseIncorrectSequence();
            TestCaseIncorrectHttpMethod();
            TestCaseIncorrectStatusCode();            
        }

        [TestMethod]
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
            Assert.AreEqual("On error ocurred: This program has only the developed conversion function", _returnOfTest);
        }

        [TestMethod]
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
            Assert.AreEqual("On error ocurred: The conversion function needs 2 parameters: FileInput URL and OutputDirectory", _returnOfTest);
        }

        [TestMethod]
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
            Assert.AreEqual("On error ocurred: This program has only the developed conversion function", _returnOfTest);
        }

        [TestMethod]
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
            Assert.AreEqual("On error ocurred: Line 1 : Status code incorrect", _returnOfTest);
        }

        [TestMethod]
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
            Assert.AreEqual("On error ocurred: Line 2 : Block three does not http valid method", _returnOfTest);
        }

        [TestMethod]
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
            Assert.AreEqual("On error ocurred: Line 1 : Block three does not match the required format, example: \"GET / robots.txt HTTP / 1.1\"", _returnOfTest);
        }

        [TestMethod]
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
            Assert.AreEqual("On error ocurred: File empty", _returnOfTest);
        }
    }
}
