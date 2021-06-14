using DocumentFormat.OpenXml.Spreadsheet;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using TMS.Models;

namespace TMS.Helpers
{
    public class ExcelHelper
    {
        public static byte[] ExportExcel(IEnumerable<Tasks> data)
        {
            //column Header name
            var columns = new List<string>{
            "Id",
            "Name",
            "Description",
            "Start Date",
            "Finish Date",
            "State",
            };

            string heading = "Tasks";
            byte[] result = null;

            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage())
            {
                // add a new worksheet to the empty workbook
                var worksheet = package.Workbook.Worksheets.Add(heading);
                using (var cells = worksheet.Cells[1, 1, 1, 7])
                {
                    cells.Style.Font.Bold = true;
                    cells.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    cells.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Green);
                }
                //First add the headers
                for (int i = 0; i < columns.Count(); i++)
                {
                    worksheet.Cells[1, i + 1].Value = columns[i];
                }

                //Add values
                var j = 2;
                var count = 1;
                foreach (var item in data)
                {
                    worksheet.Cells["A" + j].Value = count;
                    worksheet.Cells["B" + j].Value = item.Name;
                    worksheet.Cells["C" + j].Value = item.Description;
                    worksheet.Cells["D" + j].Value = item.StartDate;
                    worksheet.Cells["E" + j].Value = item.EndDate;
                    worksheet.Cells["F" + j].Value = item.State;

                    j++;
                    count++;
                }
                result = package.GetAsByteArray();
            }

            return result;
        }
    }
}
