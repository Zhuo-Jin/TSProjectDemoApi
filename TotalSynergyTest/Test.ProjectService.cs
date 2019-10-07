using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TotalSynergyWebApi.Models;
using TotalSynergyWebApi.Models.DataModels;
using TotalSynergyWebApi.Repository;
using Xunit;
using System.Linq;

namespace TotalSynergyTest
{
    public class ProjectService
    {

        private ProjectDbContext _projectDbCobtext;
        private GenericRepository<Project> _projectservice;
        public ProjectService()
        {
        }

        public void InitializeDB()
        {
            

            var options = new DbContextOptionsBuilder<ProjectDbContext>()
               .UseInMemoryDatabase(databaseName: "MockProjectDatabase")
               .Options;

            _projectDbCobtext = new ProjectDbContext(options);

            _projectservice = new GenericRepository<Project>(_projectDbCobtext);

            _projectDbCobtext.Database.EnsureDeleted();
        }

        [Fact]
        public async Task Seed()
        {


            await _projectDbCobtext.Projects.AddAsync(new Project() {
                Id = 1,
                ProjectName = "Test1",
                ProjectDescription = "Test2",
                CloseTime = DateTime.Now,
                CreateTime = DateTime.Now,
                ProjectContactItems =  null
                    
            });

            await _projectDbCobtext.Projects.AddAsync(new Project()
            {
                Id = 2,
                ProjectName = "Test2",
                ProjectDescription = "Test3",
                CloseTime = DateTime.Now,
                CreateTime = DateTime.Now,
                ProjectContactItems = null

            });

            _projectDbCobtext.SaveChanges();

            
        }

        [Fact]
        public async Task Test_ProjectService_ReturnAllProject()
        {
            InitializeDB();
            await Seed();
            var count = await _projectDbCobtext.Projects.CountAsync();
            var countProject = _projectservice.GetAllObj().Count();

            Assert.Equal(countProject, count);
        }

        

    }
}
