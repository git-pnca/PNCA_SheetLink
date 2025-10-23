using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace PNCA_SheetLink.SheetLink.Model
{
    public class ScheduleDataBuilder

    {
        public DataTable PrepareTableData(List<ScheduledElement> scheduledElements)
        {
            DataTable dataTable = new DataTable();

            // Build columns dynamically
            dataTable.Columns.Add("ElementId");
            var allFieldNames = scheduledElements
                .SelectMany(e => e.ScheduledFields.Select(f => f.FieldName))
                .Distinct();

            foreach (var fieldName in allFieldNames)
                dataTable.Columns.Add(fieldName);

            // Fill rows
            foreach (var element in scheduledElements)
            {
                var row = dataTable.NewRow();
                row["ElementId"] = element.RowElementId.ToString();

                foreach (var field in element.ScheduledFields)
                    row[field.FieldName] = field.FieldValue ?? string.Empty;

                dataTable.Rows.Add(row);
            }
            return dataTable;
        }

    }
}
