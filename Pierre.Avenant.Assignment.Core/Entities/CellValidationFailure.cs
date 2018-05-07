using System;
using System.Collections.Generic;
using System.Text;

namespace Pierre.Avenant.Assignment.Core.Entities
{
    public class CellValidationFailure
    {
        public String ColumnName { get; set; }
        public string ValidationFailureDescription { get; set; }
    }
}
