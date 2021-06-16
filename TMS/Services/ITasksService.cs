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

}
