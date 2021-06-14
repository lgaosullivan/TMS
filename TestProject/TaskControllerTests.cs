using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMS.Controllers;
using Moq;
using TMS.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using TMS.Services;

namespace TestProject
{
    [TestClass]
    public class TaskControllerTests
    {
        Mock<ITasksService> tasksService = new Mock<ITasksService>();
        
        Mock<TaskDbContext> taskDbContext = new Mock<TaskDbContext>();

        public void Setup()
        {

            List<Tasks> entities;
            entities = new List<Tasks>();
            entities.Add(new Tasks
            {
                Id = 1,
                Name = "TEST NAME"
            });
            var myDbMoq = new Mock<TaskDbContext>();
            myDbMoq.Setup(p => p.Tasks).Returns(TaskDbContextMock.GetQueryableMockDbSet<Tasks>(entities));
            myDbMoq.Setup(p => p.SaveChanges()).Returns(1);
            var moq = new Mock<TaskDbContext>();
        }

        public static class TaskDbContextMock
        {
            public static DbSet<T> GetQueryableMockDbSet<T>(List<T> sourceList) where T : class
            {
                var queryable = sourceList.AsQueryable();
                var dbSet = new Mock<DbSet<T>>();
                dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
                dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
                dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
                dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
                dbSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>((s) => sourceList.Add(s));
                return dbSet.Object;
            }
        }

        private IEnumerable<Tasks> GetFakeData()
        {
            var i = 1;
            var persons = new List<Tasks>();
            persons.Add(new Tasks() { Id = 1, Name = "UnitTest" });
            return persons;
        }

        [TestMethod]
        public async Task GetAllTasks()
        {
            // arrange
            var service = new Mock<ITasksService>();
            var controller = new TaskController(null, service.Object);

            var persons = GetFakeData();
            var firstPerson = persons.Where(x => x.StartDate >= DateTime.Now.AddDays(-1)
            && x.EndDate <= DateTime.Now.AddDays(1));

            service.Setup(x => x.AllTasks(DateTime.Now.AddDays(-1), DateTime.Now.AddDays(1))).Returns(firstPerson);

            //// act
            var result = controller.ExportExcel(DateTime.Now.AddDays(-1), DateTime.Now.AddDays(1));
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsInstanceOfType(result, typeof(Task<FileContentResult>));
        }

        [TestMethod]
        public async Task GetTask()
        {
            // arrange
            var service = new Mock<ITasksService>();
            var controller = new TaskController(null, service.Object);

            var persons = GetFakeData();
            var firstTask = persons.First();
            service.Setup(x => x.GetTask(1)).Returns(Task.FromResult(firstTask));

            //// act
            var result = controller.GetTask(1);

            // assert
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(Task.FromResult(result).Result);
            //Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual("UnitTest", result.Result.);
        }
    }
}
