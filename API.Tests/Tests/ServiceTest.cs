using Api.Contract;
using Api.Entities.Discussion;
using Api.Repository;
using Api.Service;
using Api.Service.Contract;
using Api.Shared.DataTransferObjects;
using AutoMapper;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Tests.Tests
{
    [TestFixture]
    internal class ServiceTest
    {
        private IServiceManager _serviceManager;

        [OneTimeSetUp]
        public void Setup()
        {
            MapperConfiguration mapperConfig = new MapperConfiguration(
            cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            IMapper mapper = new Mapper(mapperConfig);

            var repositoryManager = new RepositoryManager(new MySqlConnector.MySqlConnection("server='localhost';user=root;password='';database='course_site'; Allow User Variables=true"));
            _serviceManager = new ServiceManager(repositoryManager, mapper, null);
        }

        [Test]
        [TestCase("test1", "test1", 64, 41)]
        [TestCase("test2", "test2", 64, 41)]
        [TestCase("test3", "test3", 64, 41)]
        [TestCase("test4", "test4", 64, 41)]
        public void CreateQuestionTest(string questionName, string questionText, int elementId, int userId)
        {
            var question = new QuestionForCreationDto { Title = questionName, Text = questionText, ElementId = elementId, UserId = userId };

            var result = _serviceManager.DiscussionService.CreateQuestionAsync(question, elementId, userId).Result;

            Assert.AreEqual(result.Text, questionText);
        }

        [Test]
        [TestCase("mock1")]
        [TestCase("mock2")]
        [TestCase("mock3")]
        [TestCase("mock3")]
        public void CreateAnswerTest(string answerText)
        {
            MapperConfiguration mapperConfig = new MapperConfiguration(
              cfg =>
              {
                  cfg.AddProfile(new MappingProfile());
              });
            IMapper mapper = new Mapper(mapperConfig);

            var repositoryManagerMock = new Mock<IRepositoryManager>();
            repositoryManagerMock.Setup(x => x.DiscussionRepository.CreateAnswer(It.IsAny<Answer>()).Result).Returns(new Answer { Text = answerText });

            var serviceManager = new ServiceManager(repositoryManagerMock.Object, mapper, null);

            var answerToCreate = new AnswerForCreationDto();
            var result = serviceManager.DiscussionService.CreateAnswerAsync(answerToCreate, 0, 0).Result;

            Assert.AreEqual(result.Text, answerText);          
        }
    }
}
