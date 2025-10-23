using System.Collections.Generic;
using System.Data;
using System.Linq;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;

namespace PNCA_SheetLink.SheetLink.Model
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class ScheduleDataFromElements

    {
        public DataTable CreateScheduleDataTable(Document document, ViewSchedule scheduleView)
        {
            var scheduledElements = new List<ScheduledElement>();
            var dataTableBuilder = new ScheduleDataBuilder();
            var fieldIds = new List<ElementId>();
            #region ViewCollector
            var allSchedule = new FilteredElementCollector(document).OfClass(typeof(ViewSchedule)).Cast<ViewSchedule>().ToList();
            //var scheduleView = allSchedule.Where(s => s.Name.Equals("Door Schedule")).FirstOrDefault();
            var visibleElem = new FilteredElementCollector(document, scheduleView.Id).ToElements();
            var scheduleFieldCount = scheduleView.Definition.GetFieldCount();
            #endregion
            for (int i = 0; i < scheduleFieldCount; i++)
            {
                var fieldId = scheduleView.Definition.GetField(i).ParameterId;
                if (fieldId != new ElementId(-1))
                    fieldIds.Add(fieldId);
                else if (scheduleView.Definition.GetField(i).IsCombinedParameterField)
                {
                    var combinedParameters = scheduleView.Definition.GetField(i).GetCombinedParameters().ToList().Select(a => a.ParamId);
                    fieldIds.AddRange(combinedParameters);
                }
            }
            foreach (var elem in visibleElem)
            {

                var scheduledElement = new ScheduledElement();
                scheduledElement.RowElementId = elem.Id;

                var instanceParameterSet = elem.Parameters.OfType<Parameter>().ToList();
                scheduledElement.ScheduledFields.AddRange(processParameters(elem, fieldIds, "Instance", instanceParameterSet));


                var elemSymbol = document.GetElement(elem.GetTypeId());
                var typeParameterSet = elemSymbol.Parameters.OfType<Parameter>().ToList();

                scheduledElement.ScheduledFields.AddRange(processParameters(elem, fieldIds, "Type", typeParameterSet));
                scheduledElements.Add(scheduledElement);

            }
            var dataTable = dataTableBuilder.PrepareTableData(scheduledElements);

            //TaskDialog.Show("Success", "Read Success");

            return dataTable;
        }
        

        public List<ScheduledField> processParameters (Element elem, List<ElementId> fieldIds, string parameterType, List<Parameter> combinedParameters)
        {
            List<ScheduledField> scheduledFields = combinedParameters                    
                    .Where(p => fieldIds.Contains(p.Id))
                    .Select(p => new ScheduledField
                    {
                        ParameterElement = p,
                        FieldName = p.Definition.Name,
                        FieldValue = p.AsString() ?? p.AsValueString() ?? string.Empty,
                        SelectedElementId = elem.Id,
                        ParameterType = parameterType
                    })
                    .ToList();
            return scheduledFields;
        }


    }
}
