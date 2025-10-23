#region fromScheduleData

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace PNCA_SheetLink.SheetLink.Model
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class ScheduleDataAsText : IExternalCommand

    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var uiApplication = commandData.Application;
            var application = uiApplication.Application;
            var uiDocument = uiApplication.ActiveUIDocument;
            var document = uiDocument.Document;

            var allSchedule = new FilteredElementCollector(document).OfClass(typeof(ViewSchedule)).Cast<ViewSchedule>().ToList();
            var schedule = allSchedule.Where(s => s.Name.Equals("Door Schedule")).FirstOrDefault();


            if (schedule == null)
            {
                TaskDialog.Show("Info", "No schedules found.");
                return Result.Succeeded;
            }

            // Access table data
            TableData tableData = schedule.GetTableData();
            TableSectionData bodyData = tableData.GetSectionData(SectionType.Body);
            var coln = bodyData.NumberOfColumns;
            var rown = bodyData.NumberOfRows;

            List<string[]> rowCollection = new List<string[]>();
            for (int row = 0; row < rown; row++)
            {
                string[] cellTexts = new string[coln];
                for (int col = 0; col < coln; col++)
                {

                    var scheduleTexts = schedule.GetCellText(SectionType.Body, row, col).ToString();
                    if (!string.IsNullOrEmpty(scheduleTexts) && !string.IsNullOrWhiteSpace(scheduleTexts))
                        cellTexts[col] = scheduleTexts;
                }
                //if (!cellTexts.All(string.IsNullOrEmpty))
                rowCollection.Add(cellTexts);
            }



            TaskDialog.Show("Success", "Change Success");

            return Result.Succeeded;
        }
    }
}
#endregion

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Autodesk.Revit.Attributes;
//using Autodesk.Revit.DB;
//using Autodesk.Revit.UI;

//namespace RevitDevTest
//{
//    [Transaction(TransactionMode.Manual)]
//    [Regeneration(RegenerationOption.Manual)]
//    public class GetScheduleElements : IExternalCommand

//    {
//        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
//        {
//            var uiApplication = commandData.Application;
//            var application = uiApplication.Application;
//            var uiDocument = uiApplication.ActiveUIDocument;
//            var document = uiDocument.Document;
//            var allSchedule = new FilteredElementCollector(document).OfClass(typeof(ViewSchedule)).Cast<ViewSchedule>().ToList();
//            var schedule = allSchedule.Where(s => s.Name.Equals("Door Schedule")).FirstOrDefault().Id;
//            var visibleElem = new FilteredElementCollector(document, schedule).ToElements();

//            using (var t = new Transaction(document))
//            {
//                t.Start("Write Test");
//                //Codeblock
//                t.Commit();
//            }
//            TaskDialog.Show("Success", "Change Success");

//            return Result.Succeeded;
//        }
//    }
//}
