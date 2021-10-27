using AutoMapper;
using FluentAssertions;
using midTerm.Models.Models.Answers;
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
    public class AnswersServiceShould : SqlLiteContext
    {
        private readonly IMapper _mapper;
        private readonly AnswerService _service;

        public AnswersServiceShould() : base(false)
        {
            if (_mapper == null)
            {
                var mapper = new MapperConfiguration(cfg =>
                {
                    cfg.AddMaps(typeof(AnswersProfile));
                }).CreateMapper();
                _mapper = mapper;
            }

            _service = new AnswerService(DbContext, _mapper);
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
            result.Should().BeAssignableTo<AnswersExtended>();
            result.Id.Should().Be(expected);
        }

        [Fact]
        public async Task Get()
        {
            // Arrange
            var expected = 4;

            // Act
            var result = await _service.Get();

            // Assert
            result.Should().NotBeEmpty().And.HaveCount(expected);
            result.Should().BeAssignableTo<IEnumerable<AnswersBaseModel>>();
        }


        [Fact]
        public async Task InsertNewAnswer()
        {
            // Arrange
            var asnwer = new AnswerCreateModel
            {
                OptionId = 1,
                UserId = 1
            };

            // Act
            var result = await _service.Insert(asnwer);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<AnswersBaseModel>();
            result.Id.Should().NotBe(0);
        }

        [Fact]
        public async Task UpdateAnswer()
        {
            // Arrange
            var asnwer = new AnswersUpdateModel
            {
                Id = 1,
                OptionId = 1,
                UserId = 2
            };

            // Act
            var result = await _service.Update(asnwer);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<AnswersBaseModel>();
            result.Id.Should().Be(asnwer.Id);
            result.OptionId.Should().Be(asnwer.OptionId);
            result.UserId.Should().Be(asnwer.UserId);

        }

        [Fact]
        public async Task ThrowExceptionOnUpdateMatch()
        {
            // Arrange
            var asnwer = new AnswersUpdateModel
            {
                Id = 10,
                OptionId = 1,
                UserId = 2
            };

            // Act and Assert
            var ex = await Assert.ThrowsAsync<Exception>(() => _service.Update(asnwer));
            Assert.Equal("Match not found", ex.Message);

        }

        [Fact]
        public async Task DeleteAnswer()
        {
            // Arrange
            var expected = 1;

            // Act
            var result = await _service.Delete(expected);
            var match = await _service.GetById(expected);

            // Assert
            result.Should().Be(true);
            match.Should().BeNull();
        }
    }
}
