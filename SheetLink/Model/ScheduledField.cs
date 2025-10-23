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
    public class ScheduledField

    {
        public  string FieldName { get; set; } = string.Empty;
        public string FieldValue { get; set; } = string.Empty;
        public string ParameterType { get; set; } = string.Empty;
        public Parameter ParameterElement { get; set; }
        public ElementId SelectedElementId { get; set; }
    }
}
