﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using PNCA_SheetLink.SheetLink.Model;
using PNCA_SheetLink.SheetLink.View;

namespace PNCA_SheetLink.SheetLink.RevitEntryPoint
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class ScheduleFromElementsExtractor : IExternalCommand

    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var uiApplication = commandData.Application;
            var application = uiApplication.Application;
            var uiDocument = uiApplication.ActiveUIDocument;
            var document = uiDocument.Document;
            //var scheduleDataFromElements = new ScheduleDataFromElements();
                         

            // Create and show your window
            var mainWindow = new SheetLinkMain(document, uiDocument);

            // Set owner to Revit window so it stays on top and modal
            System.Windows.Interop.WindowInteropHelper helper = new System.Windows.Interop.WindowInteropHelper(mainWindow);
            helper.Owner = System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle;

            mainWindow.ShowDialog();               

            return Result.Succeeded;
        }
    }
}
