using System;
using System.Collections.Generic;
using System.Text;

namespace Pierre.Avenant.Assignment.Core.Entities
{
    public class RowValidationFailure
    {
        public int RowNumber { get; set; }
        public IList<CellValidationFailure> CellValidationFailures { get; set; }
    }
}
