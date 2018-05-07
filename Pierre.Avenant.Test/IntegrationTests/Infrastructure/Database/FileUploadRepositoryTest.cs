using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pierre.Avenant.Assignment.Core.Entities;
using Pierre.Avenant.Assignment.Infrastructure.Database;

namespace Pierre.Avenant.Assignment.Test.IntegrationTests.Infrastructure.Database
{
    [TestClass]
    public class FileUploadRepositoryTest
    {
        [TestMethod]
        public void Save_Success()
        {
            var fileUploadRepository = new FileUploadRepository(Configuration.ConnectionString);
            var fileUpload = new FileUpload() {FileName = "MyTest.xlsx",NumberOfSuccessfullRecords = 2,NumberOfRecords = 2};
            fileUploadRepository.Save(fileUpload);
            Assert.AreNotEqual(0,fileUpload.FileUploadId);
        }
    }
}
