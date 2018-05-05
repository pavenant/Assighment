using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper;
using Pierre.Avenant.Assignment.Core.Entities;
using Pierre.Avenant.Assignment.Core.Interfaces.Database;

namespace Pierre.Avenant.Assignment.Infrastructure.Database
{
    public class FileUploadRepository : IFileUploadRepository
    {
        public void Save(FileUpload fileUpload)
        {
            using (IDbConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=Assignment;Integrated Security=True"))
            {
                    var sql = @"

	                insert FileUpload (FileName,NumberOfRecords,NumberOfSuccessfullRecords)
	                values (@FileName,@NumberOfRecords,@NumberOfSuccessfullRecords)	
                        
                    select CAST(Scope_Identity() as Int) as Id

                        ";
                    fileUpload.FileUploadId = connection.Query<int>(sql, fileUpload).FirstOrDefault();
            }
        }
    }
}
