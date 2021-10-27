using AutoMapper;
using FluentAssertions;
using midTerm.Models.Models.SurveyUser;
using midTerm.Models.Profiles;
using midTerm.Services.Services;
using midTerm.Services.Tests.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace midTerm.Services.Tests.Service
{
    public class SurveyUserServiceServiceShould : SqlLiteContext
    {
        private readonly IMapper _mapper;
        private readonly SurveyUserService _service;

        public SurveyUserServiceServiceShould() : base(false)
        {
            if (_mapper == null)
            {
                var mapper = new MapperConfiguration(cfg =>
                {
                    cfg.AddMaps(typeof(SurveyUserProfile));
                }).CreateMapper();
                _mapper = mapper;
            }

            _service = new SurveyUserService(DbContext, _mapper);
        }

        [Fact]
        public async Task GetById()
        {
            // Arrange
            var expected = 1;
            // Act 
            var result = await _service.GetById(expected);
            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<SurveyUserExtended>();
            result.Id.Should().Be(expected);
        }

        [Fact]
        public async Task Get()
        {
            //Arrange 
            var expected = 6;


            //Act
            var result = await _service.Get();


            //Assert
            result.Should().NotBeNull().And.NotBeEmpty().And.HaveCount(expected);
            result.Should().BeAssignableTo < IEnumerable<SurveyUserBaseModel>>();
        }


        [Fact]
        public async Task Insert()
        {
            //Arrange
            var survetUser = new SurveyUserCreate
            {
                FirstName = "First Name",
                LastName = "Last name",
                DoB = DateTime.Today.AddYears(-21),
                Gender = Data.Enums.Gender.Male,
                Country = "Makedonija"
            };


            //Act
            var result = await _service.Insert(survetUser);


            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<SurveyUserBaseModel>();
            result.Id.Should().NotBe(0);
        }


        [Fact]
        public async Task Update()
        {

            //Arrange
            var surveyuser = new SurveyUserUpdate
            {
                Id = 1,
                FirstName = "First Name",
                LastName = "Last name",
                DoB = DateTime.Today.AddYears(-21),
                Gender = Data.Enums.Gender.Male,
                Country = "Makedonija"
            };


            //Act
            var result = await _service.Update(surveyuser);


            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<SurveyUserBaseModel>();
            result.Id.Should().Be(surveyuser.Id);
            result.FirstName.Should().Be(surveyuser.FirstName);
            result.LastName.Should().Be(surveyuser.LastName);
            result.Country.Should().Be(surveyuser.Country);
            result.Gender.Should().Be(surveyuser.Gender);
        }

        [Fact]
        public async Task Delete()
        {
            //Arrange
            int expected = 3;

            //Act
            var result = await _service.Delete(expected);

            //Find the object with the expected Id and store it
            var surveyUser = await _service.GetById(expected);

            //Assert
            result.Should().BeTrue();
            surveyUser.Should().BeNull();
        }

        [Fact]
        public async Task ThrowExceptionOnUpdateMatch()
        {
            //Arrange
            var surveyUser = new SurveyUserUpdate
            {
                Id = 20,
                FirstName = "First Name",
                LastName = "Last name",
                DoB = DateTime.Today.AddYears(-21),
                Gender = Data.Enums.Gender.Male,
                Country = "Makedonija"
            };


            //Act & Assert
            var ex = await Assert.ThrowsAsync<Exception>(() => _service.Update(surveyUser));

            Assert.Equal("Match not found", ex.Message);

        }
    }
}
