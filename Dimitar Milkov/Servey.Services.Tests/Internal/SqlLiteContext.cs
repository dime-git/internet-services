using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using midTerm.Data;
using midTerm.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace midTerm.Services.Tests.Internal
{
    public abstract class SqlLiteContext : IDisposable
    {
        private const string InMemoryConnectionString = "DataSource=:memory:";
        private readonly SqliteConnection _connection;
        //Actual Context of the app (from data)
        protected readonly MidTermDbContext DbContext;

        protected SqlLiteContext(bool withData = false)
        {
            _connection = new SqliteConnection(InMemoryConnectionString);
            DbContext = new MidTermDbContext(CreateOptions());
            _connection.Open();
            DbContext.Database.EnsureCreated();
            //When we create an instance, we would like to already have data in it.
            SeedData(DbContext);

            if (withData)
                SeedData(DbContext);
        }

        private DbContextOptions<MidTermDbContext> CreateOptions()
        {
            return new DbContextOptionsBuilder<MidTermDbContext>()
                .EnableDetailedErrors()
                .EnableSensitiveDataLogging()
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .UseSqlite(_connection)
                .Options;
        }

        private void SeedData(MidTermDbContext dbContext)
        {
            var surveyUser = new List<SurveyUser>()
            {
                new SurveyUser
                {
                    Id = 1,
                    FirstName = "First Name",
                    LastName = "Last name",
                    DoB = DateTime.Today.AddYears(-21),
                    Gender = Data.Enums.Gender.Male,
                    Country = "Makedonija"
                },
                new SurveyUser
                {
                    Id = 2,
                    FirstName = "First Name2",
                    LastName = "Last name2",
                    DoB = DateTime.Today.AddYears(-20),
                    Gender = Data.Enums.Gender.Male,
                    Country = "Makedonija"
                },
                new SurveyUser
                {
                    Id = 3,
                    FirstName = "First Name3",
                    LastName = "Last name3",
                    DoB = DateTime.Today.AddYears(-19),
                    Gender = Data.Enums.Gender.Female,
                    Country = "Makedonija"
                },
                new SurveyUser
                {
                    Id = 4,
                    FirstName = "First Name4",
                    LastName = "Last name4",
                    DoB = DateTime.Today.AddYears(-18),
                    Gender = Data.Enums.Gender.Female,
                    Country = "Makedonija"
                }
            };

            var answers = new List<Answers>()
            {
                new Answers
                {
                    Id = 1,
                    UserId = 1,
                    OptionId = 1
                    
                },
                new Answers
                {
                    Id = 2,
                    UserId = 2,
                    OptionId = 2
                },
                new Answers
                {
                    Id = 3,
                    UserId = 3,
                    OptionId = 3
                    
                },
                new Answers
                {
                    Id = 4,
                    UserId = 4,
                    OptionId = 4
                }
            };

            dbContext.AddRange(surveyUser);
            dbContext.AddRange(answers);
            dbContext.SaveChanges();

        }


            public void Dispose()
        {
            _connection.Close();
            _connection?.Dispose();
            DbContext?.Dispose();
        }
    }
}
