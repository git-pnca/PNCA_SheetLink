﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace PNCA_SheetLink.SheetLink.Model
{
    public class ScheduledElement

    {
        public ElementId RowElementId { get; set; }
        public List<ScheduledField> ScheduledFields { get; set; } = new List<ScheduledField>();

    }
}
