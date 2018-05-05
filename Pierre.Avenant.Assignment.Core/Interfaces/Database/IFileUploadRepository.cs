using System;
using System.Collections.Generic;
using System.Text;
using Pierre.Avenant.Assignment.Core.Entities;

namespace Pierre.Avenant.Assignment.Core.Interfaces.Database
{
    public interface IFileUploadRepository
    {
        void Save(FileUpload fileUpload);
    }
}
