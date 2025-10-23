﻿using System;
using System.Data;
using System.IO;
using ClosedXML.Excel;


namespace PNCA_SheetLink.SheetLink.Model
{
    public class ExcelWriter : IDisposable
    {
        
        private FileInfo _newFile;
        private string _filePath = string.Empty;
        public ExcelWriter(string filePath)
        {
            this._filePath = filePath;
            _newFile = new FileInfo(filePath);
        }

        public void CreateExcelFile(DataTable dataTable)
        {
            if (_newFile.Exists)
            {
                _newFile.Delete();
            }

            using (var wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dataTable, "Sheet1");
                var tableWIdth = dataTable.Columns.Count;
                var worksheet = wb.Worksheet("Sheet1");
                worksheet.Columns(1, tableWIdth).AdjustToContents();
                wb.SaveAs(_filePath);
            }
        }

        public void Dispose()
        {
            // Implement IDisposable if needed
        }
    }
}
