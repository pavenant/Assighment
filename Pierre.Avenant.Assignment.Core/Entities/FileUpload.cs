using System;
using System.Collections.Generic;
using System.Text;

namespace Pierre.Avenant.Assignment.Core.Entities
{
    public class FileUpload
    {
        public int FileUploadId { get; set; }
        public string FileName { get; set; }
        public int NumberOfRecords { get; set; }
        public int NumberOfSuccessfullRecords { get; set; }
    }
}
