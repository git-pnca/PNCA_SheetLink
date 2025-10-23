using System;
using System.IO;
using OfficeOpenXml;
using OfficeOpenXml.Core.ExcelPackage;

namespace PNCA_SheetLink.SheetLink.Model
{
    public class ExcelTest : IDisposable
    {
        private DirectoryInfo outputDir = new DirectoryInfo(@"C:\Temp");
        private FileInfo newFile;

        public ExcelTest()
        {
            newFile = new FileInfo(Path.Combine(outputDir.FullName, "sample1.xlsx"));
        }

        public void CreateExcelFile()
        {
            if (newFile.Exists)
            {
                newFile.Delete();
            }

            using (ExcelPackage package = new ExcelPackage(newFile))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Inventory");
                worksheet.Cell(1, 1).Value = "ID";
                worksheet.Cell(1, 2).Value = "Product";
                worksheet.Cell(1, 3).Value = "Quantity";
                worksheet.Cell(1, 4).Value = "Price";
                worksheet.Cell(1, 5).Value = "Value";

                package.Save();
            }
        }

        public void Dispose()
        {
            // Implement IDisposable if needed
        }
    }
}
