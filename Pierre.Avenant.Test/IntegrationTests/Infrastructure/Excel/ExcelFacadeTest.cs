using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pierre.Avenant.Assignment.Infrastructure.Excel;

namespace Pierre.Avenant.Assignment.Test.IntegrationTests.Infrastructure.Excel
{
    [TestClass]
    public class ExcelFacadeTest
    {
        [TestMethod]
        public void Import_ValidFileSuccess()
        {
            var e = new ExcelFileLoader();
            var records = e.GetTransactionRecords(".\\IntegrationTests\\Infrastructure\\Excel\\TestFiles\\ValidTestFile.xlsx");
            foreach (var record in records)
            {
                Console.WriteLine($"{record.Account}|{record.Description}|{record.Amount}|{record.CurrencyCode}");
            }
             Assert.AreEqual(4,records.Count);
        }

        //todo are we going todo validation here?
        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void Import_InvalidFileFormat_Exception()
        {
            var e = new ExcelFileLoader();
            var records = e.GetTransactionRecords(".\\IntegrationTests\\Infrastructure\\Excel\\TestFiles\\InvalidTestFile.xlsx");
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void Import_InvalidFileHeaderFormat_Exception()
        {
            var e = new ExcelFileLoader();
            var records = e.GetTransactionRecords(".\\IntegrationTests\\Infrastructure\\Excel\\TestFiles\\InvalidHeader.xlsx");
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void Import_FileDoesNotExists_Exception()
        {
            var e = new ExcelFileLoader();
            var records = e.GetTransactionRecords(".\\IntegrationTests\\Infrastructure\\Excel\\TestFiles\\doesnotexists.xlsx");
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void Import_NoRowsToImport_Exception()
        {
            var e = new ExcelFileLoader();
            var records = e.GetTransactionRecords(".\\IntegrationTests\\Infrastructure\\Excel\\TestFiles\\norows.xlsx");
        }
    }
}
