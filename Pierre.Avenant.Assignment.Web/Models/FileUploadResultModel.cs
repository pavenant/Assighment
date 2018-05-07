using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pierre.Avenant.Assignment.Core.Entities;

namespace Pierre.Avenant.Assignment.Web.Models
{
    public class FileUploadResultModel
    {
        public string FileName { get; set; }
        public bool Success { get; set; }
        public int FileUploadId { get; set; }

        public List<CarShop> CarShop { get; set; }
        public List<String> PartialErrorMessages { get; set; }
    }

    public class TransactionAccountFieldFailure
    {
        public String FieldName { get; set; }
        public string FailureDescription { get; set; }
    }

    public class CarShop
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
