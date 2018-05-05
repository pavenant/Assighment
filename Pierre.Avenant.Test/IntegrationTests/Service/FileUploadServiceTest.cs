using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pierre.Avenant.Assignment.Core.Interfaces.Services;
using Pierre.Avenant.Assignment.Core.Services.FileInputService;
using Pierre.Avenant.Assignment.Infrastructure.Database;
using Pierre.Avenant.Assignment.Infrastructure.Excel;

namespace Pierre.Avenant.Assignment.Test.IntegrationTests.Service
{
    [TestClass]
    public class FileUploadServiceTest
    {
        [TestMethod]
        public void UploadValidFile_Success()
        {
            IFileUploadService service = new FileUploadService(new ExcelFileLoader(),new CurrencyCodeRepository(),new FileUploadRepository(), new AccountTransactionRepository());
            var result =
                service.InportAccountTransactionFile(
                    ".\\IntegrationTests\\Infrastructure\\Excel\\TestFiles\\ValidTestFile.xlsx");

            Assert.IsNotNull(result);
            Assert.AreEqual(3,result.FileUpload.NumberOfRecords);
            Assert.AreEqual(3,result.FileUpload.NumberOfSuccessfullRecords);
        }

        [TestMethod]
        public void UploadPartialValidFile_Success()
        {
            IFileUploadService service = new FileUploadService(new ExcelFileLoader(), new CurrencyCodeRepository(),new FileUploadRepository(), new AccountTransactionRepository());
            var result =
                service.InportAccountTransactionFile(
                    ".\\IntegrationTests\\Infrastructure\\Excel\\TestFiles\\PartialFailureTestFile.xlsx");

            Assert.IsNotNull(result);
            Assert.AreEqual(5, result.FileUpload.NumberOfRecords);
            Assert.AreEqual(3, result.FileUpload.NumberOfSuccessfullRecords);
            Assert.IsNotNull(result.AccountTransactionUploadFailures);
        }
    }
}
