using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMS.Models;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;
using TMS.Services;
using TMS.Helpers;

namespace TMS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly TaskDbContext _taskDbContext;

        private readonly ITasksService _tasksService;

        public TaskController(TaskDbContext taskDbContext, ITasksService tasksService)
        {
            _taskDbContext = taskDbContext;
            _tasksService = tasksService;
        }

        [HttpGet]
        public async Task<FileContentResult> ExportExcel(DateTime? dateStart, DateTime? dateEnd)
        {
            var tasks = _tasksService.AllTasks(dateStart, dateEnd);

            var filecontent = ExcelHelper.ExportExcel(tasks);

            return File(filecontent, "application/ms-excel", "tasks.xlsx"); ;
        }

        // GET:c
        [HttpGet("{id}")]
        public async Task<ActionResult<Tasks>> GetTask(int id)
        {
            var task =  _tasksService.GetTask(id);

            if (task == null)
            {
                return NotFound();
            }

            return CreatedAtAction("GetTask", new { id = task.Id }, task);
        }
        
        // PUT: api/Task/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTask(int id, Tasks task)
        {
            if (id != task.Id)
            {
                return BadRequest();
            }
            
            try
            {
                _tasksService.UpdateTask(id, task);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Task
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tasks>> PostTask(Tasks task)
        {
            try
            {
                _tasksService.PostTask(task);

                return CreatedAtAction("GetTask", new { id = task.Id }, task);
            }
            catch
            {
                return BadRequest();
            }
        }

        //// DELETE: api/Task/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            _tasksService.DeleteTask(id);

            return NoContent();
        }

        private bool TaskExists(int id)
        {
            return _taskDbContext.Tasks.Any(e => e.Id == id);
        }
    }
}
