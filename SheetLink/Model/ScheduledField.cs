using Autodesk.Revit.DB;

namespace PNCA_SheetLink.SheetLink.Model
{
    public class ScheduledField

    {
        public  string FieldName { get; set; } = string.Empty;
        public string FieldValue { get; set; } = string.Empty;
        public string ParameterType { get; set; } = string.Empty;
        public int FieldIndex { get; set; }
        public Parameter ParameterElement { get; set; }
        public ElementId SelectedElementId { get; set; }
    }
}
