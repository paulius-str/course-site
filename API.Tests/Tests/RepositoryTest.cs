using Api.Contract;
using Api.Entities;
using Api.Entities.CourseEntities;
using Api.Entities.Ratings;
using Api.Repository;
using Api.Service.Contract;
using Api.Shared.DataTransferObjects;
using API.Controllers;
using Castle.Core.Configuration;
using Microsoft.AspNetCore.Mvc;
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
    internal class RepositoryTest
    {
        private Mock<IRepositoryManager> _repositoryMock;
        private Mock<IServiceManager> _serviceMock;
        private IRepositoryManager _repositoryManager;

        [OneTimeSetUp]
        public void Setup()
        {
            _repositoryMock = new Mock<IRepositoryManager>();
            _serviceMock = new Mock<IServiceManager>();
            _repositoryMock.Setup(x => x.CourseRepository.GetCourseById(It.IsAny<int>()).Result).Returns(new Course { Description = "test" });
            _serviceMock.Setup(x => x.CourseService.GetPublishedCourseAsync(It.IsAny<int>()).Result).Returns(new CourseDto { Description = "test" });

            _repositoryManager = new RepositoryManager(new MySqlConnector.MySqlConnection("server='localhost';user=root;password='';database='course_site'; Allow User Variables=true"));
        }

        [Test]
        [TestCase("test1", 36)]
        [TestCase("test2", 36)]
        [TestCase("test3", 36)]
        [TestCase("test4", 36)]
        public void CreateCourse(string courseName, int authorUserId)
        {

            var course = new Course { Name = courseName };

            var result = _repositoryManager.CourseRepository.CreateCourse(course, authorUserId).Result;
            
            Assert.AreEqual(courseName, result.Name);
        }

        [TestCase("test1", 64)]
        [TestCase("test2", 64)]
        [TestCase("test3", 64)]
        [TestCase("test4", 64)]
        public void CreateCourseSection(string sectionName, int courseId)
        {
            var section = new CourseSection { Name = sectionName, CourseId = courseId };

            var result = _repositoryManager.CourseRepository.CreateSection(section).Result;

            Assert.AreEqual(result.Name, sectionName);
        }


        [TestCase("test1", 72)]
        [TestCase("test2", 72)]
        [TestCase("test3", 72)]
        [TestCase("test4", 72)]
        public void CreateSectionElement(string elementName, int sectionId)
        {
            var element = new CourseElement { Name = elementName, SectionId = sectionId };

            var result = _repositoryManager.CourseRepository.CreateElement(element).Result;

            Assert.AreEqual(element.Name, elementName);
        }



        [TestCase(1, "test review", 41, 2)]
        public void CreateRating(int ratingScore, string review, int userId, int courseId)
        {
            var rating = new Rating { Score = ratingScore, Review = review, StudentId = userId};

            var result = _repositoryManager.RatingRepository.RateCourse(rating, courseId, userId).Result;

            Assert.AreEqual(result.Review, review);
        }
    }
}
