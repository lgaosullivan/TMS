using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using TMS.Models;

namespace TMS.Services
{
    public interface ITasksService
    {
        IEnumerable<Tasks> AllTasks(DateTime? dateStart, DateTime? dateEnd);
        Task<Tasks> GetTask(int id);
        Task UpdateTask(int id, Tasks task);

        Task PostTask(Tasks task);

        Task DeleteTask(int id);
    }

    //public class TasksService : ITasksService
    //{
    //    private readonly TaskDbContext _taskDbContext;
    //    public TasksService(TaskDbContext taskDbContext)
    //    {
    //        _taskDbContext = taskDbContext;
    //    }

    //    public IEnumerable<Tasks> AllTasks(DateTime? dateStart, DateTime? dateEnd)
    //    {
    //        List<Tasks> tasks;

    //        if (dateStart != null && dateEnd != null)
    //        {
    //            tasks = _taskDbContext.Tasks.Select(u => new Tasks
    //            {
    //                Id = u.Id,
    //                Name = u.Name,
    //                Description = u.Description,
    //                StartDate = u.StartDate,
    //                EndDate = u.EndDate,
    //                State = u.State
    //            })
    //          .Where(i => i.StartDate.HasValue && i.StartDate >= dateStart && i.EndDate.HasValue && i.EndDate <= dateEnd)
    //          .ToList();

    //            if (tasks == null) return null;
    //        }
    //        else
    //        {
    //            tasks = _taskDbContext.Tasks
    //           .Include(i => i.SubTasks)
    //           .ToList();

    //            if (tasks == null) return null;
    //        }
    //        return tasks;
    //    }

    //    public async Task<Tasks> GetTask(int id)
    //    {
    //        return _taskDbContext.Tasks
    //        .Include(i => i.SubTasks)
    //        .FirstOrDefault(x => x.Id == id);
    //    }

    //    public async Task UpdateTasks(List<int> ids)
    //    {
    //        var getTasks = _taskDbContext.Tasks
    //            .AsNoTracking()
    //            .Include(i => i.SubTasks)
    //            .Where(i => ids.All(x => x.));
    //    }

    //    public async Task UpdateTask(int id, Tasks task)
    //    {
    //        var getTasks = _taskDbContext.Tasks
    //        .AsNoTracking()
    //        .Include(i => i.SubTasks)
    //        .FirstOrDefault(x => x.Id == id);

            

    //        try
    //        {
    //            foreach (var subtask in getTasks.SubTasks)
    //            {
    //                if (subtask.State == "InProgress")
    //                {
    //                    task.State = "InProgress";
    //                    break;
    //                }
    //                else if (getTasks.SubTasks.Where(i => i.State == "Completed").Count() == getTasks.SubTasks.Count())
    //                {
    //                    task.State = "Completed";
    //                    continue;
    //                }
    //                else
    //                {
    //                    task.State = "Planned";
    //                }
    //            }

    //            _taskDbContext.Tasks.Update(task);
    //            _taskDbContext.SaveChanges();
    //        }
    //        catch (DbUpdateConcurrencyException)
    //        {
    //            //if (!TaskExists(id))
    //            //{
    //            //    return NotFound();
    //            //}
    //            //else
    //            //{
    //            //    throw;
    //            //}
    //        }

    //        _taskDbContext.Entry(task).State = EntityState.Modified;

    //        _taskDbContext.Tasks.Update(task);

    //        _taskDbContext.SaveChanges();
    //    }

    //    public async Task PostTask(Tasks task)
    //    {
    //        _taskDbContext.Add(task);
    //        _taskDbContext.SaveChanges();
    //    }


    //    public async Task DeleteTask(int id)
    //    {
    //        var getTasks = _taskDbContext.Tasks
    //            .AsNoTracking()
    //        .Include(i => i.SubTasks)
    //        .FirstOrDefault(x => x.Id == id);

    //        _taskDbContext.Entry(getTasks).State = EntityState.Detached;

    //        _taskDbContext.Remove(getTasks);

    //        _taskDbContext.SaveChanges();
    //    }
    //}
}
