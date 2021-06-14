//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using TMS.Controllers;
//using Xunit;
//using Moq;
//using TMS.Models;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Logging;
//using Microsoft.AspNetCore.Mvc;

//namespace TMS.Tests
//{
//    public class TaskController_Tests
//    {
//        Mock<ILogger<TaskController>> loggerMock = new Mock<ILogger<TaskController>>();
//        Mock<TaskDbContext> taskDbContext = new Mock<TaskDbContext>();
//        public static class TaskDbContextMock
//        {
//            public static DbSet<T> GetQueryableMockDbSet<T>(List<T> sourceList) where T : class
//            {
//                var queryable = sourceList.AsQueryable();
//                var dbSet = new Mock<DbSet<T>>();
//                dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
//                dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
//                dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
//                dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
//                dbSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>((s) => sourceList.Add(s));
//                return dbSet.Object;
//            }
//        }

//        //[SetUp]
//        public void Setup()
//        {
            
//            List<Tasks> entities;
//            entities = new List<Tasks>();
//            entities.Add(new Tasks
//            {
//                Id = 1,
//                Name = "TEST NAME"
//            });
//            var myDbMoq = new Mock<TaskDbContext>();
//            myDbMoq.Setup(p => p.Tasks).Returns(TaskDbContextMock.GetQueryableMockDbSet<Tasks>(entities));
//            myDbMoq.Setup(p => p.SaveChanges()).Returns(1);
//            var moq = new Mock<TaskDbContext>();
//            //moq.Setup(p => p.TaskDbContext()).Returns(myDbMoq.Object);
//            //contextGenerator = moq.Object;
//        }

//        [Fact]
//        public async void GetAll_ReturnsTwoProducts()
//        {
//            //var configuration = GetConfig();

//            var sut = new TaskController(loggerMock.Object, taskDbContext.Object);

//            var result = await sut.ExportExcel(DateTime.Now.AddDays(-1), DateTime.Now.AddDays(1));

//            Assert.IsType<FileContentResult>(result);
//        }

//        [Fact]
//        public async void GetTask_ReturnsATask()
//        {
//            //var configuration = GetConfig();

//            var sut = new TaskController(loggerMock.Object, taskDbContext.Object);

//            var result = await sut.GetTask(1);

//            Assert.IsType<FileContentResult>(result);
//        }

//       // GetTask(int id)
//    }
//}
