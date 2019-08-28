using Microsoft.Office.Interop.Excel;
using System;

namespace BierPongTurnier.Settings
{
    public class ExportToKnockoutStage
    {
        public void Test()
        {
            Application excel = new Application();
            Workbook sheet = excel.Workbooks.Open(@"C:\Users\timme\Desktop\master-knockout-plan.xlsx", ReadOnly:false);
            Worksheet ko = excel.ActiveSheet as Worksheet;

            var range = ko.UsedRange;
            Console.WriteLine(range.Value as string);
            /*
            for (int y = 1; y <= range.Rows.Count; y++)
            {
                for (int x = 1; x <= range.Columns.Count; x++)
                {
                    var cell = range[y, x] as Range;
                    var content = cell.Value as string;
                    if ("2.A".Equals(content))
                    {
                        var cellAbove = range[y-1, x] as Range;
                        cellAbove.Value = "Team 2A";
                    }
                    Console.WriteLine("x:" + x + "y"  + y + "Cell:"  + content);
                }
            }*/

            sheet.Close(true, Type.Missing, Type.Missing);
            excel.Quit();
        }
    }
}