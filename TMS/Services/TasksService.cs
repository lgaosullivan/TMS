using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMS.Models;

namespace TMS.Services
{
    public class TasksService : ITasksService
    {
        private readonly ILogger _logger;
        private readonly TaskDbContext _taskDbContext;
        public TasksService(TaskDbContext taskDbContext)
        {
            _taskDbContext = taskDbContext;
        }

        public IEnumerable<Tasks> AllTasks(DateTime? dateStart, DateTime? dateEnd)
        {
            List<Tasks> tasks;

            if (dateStart != null && dateEnd != null)
            {
                tasks = _taskDbContext.Tasks.Select(u => new Tasks
                {
                    Id = u.Id,
                    Name = u.Name,
                    Description = u.Description,
                    StartDate = u.StartDate,
                    EndDate = u.EndDate,
                    State = u.State
                })
              .Where(i => i.StartDate.HasValue && i.StartDate >= dateStart && i.EndDate.HasValue && i.EndDate <= dateEnd)
              .ToList();

                if (tasks == null) return null;
            }
            else
            {
                tasks = _taskDbContext.Tasks
               .Include(i => i.SubTasks)
               .ToList();

                if (tasks == null) return null;
            }
            return tasks;
        }

        public async Task<Tasks> GetTask(int id)
        {
            return _taskDbContext.Tasks
            .Include(i => i.SubTasks)
            .FirstOrDefault(x => x.Id == id);
        }

        public async Task UpdateTask(int id, Tasks task)
        {
            var getTasks = _taskDbContext.Tasks
            .AsNoTracking()
            .Include(i => i.SubTasks)
            .FirstOrDefault(x => x.Id == id);

            try
            {
                if (getTasks.SubTasks.Where(i => i.State == "InProgress").Count() >= 1)
                    task.State = "InProgress";

                if (getTasks.SubTasks.Where(i => i.State == "Completed").Count() == getTasks.SubTasks.Count())
                    task.State = "Completed";

                if (getTasks.SubTasks.Where(i => i.State == "Completed").Count() != getTasks.SubTasks.Count() &&
                    getTasks.SubTasks.Where(i => i.State == "InProgress").Count() == 0)
                    task.State = "Planned";


                _taskDbContext.Tasks.Update(task);
                _taskDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }

            _taskDbContext.Entry(task).State = EntityState.Modified;

            _taskDbContext.Tasks.Update(task);

            _taskDbContext.SaveChanges();
        }

        public async Task PostTask(Tasks task)
        {
            _taskDbContext.Add(task);
            _taskDbContext.SaveChanges();
        }


        public async Task DeleteTask(int id)
        {
            var getTasks = _taskDbContext.Tasks
                .AsNoTracking()
            .Include(i => i.SubTasks)
            .FirstOrDefault(x => x.Id == id);

            _taskDbContext.Entry(getTasks).State = EntityState.Detached;

            _taskDbContext.Tasks.Remove(getTasks);

            _taskDbContext.SaveChanges();
        }
    }
}
