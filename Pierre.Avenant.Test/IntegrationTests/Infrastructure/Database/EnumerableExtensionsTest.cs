using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pierre.Avenant.Assignment.Core.Entities;
using Pierre.Avenant.Assignment.Infrastructure.Database;

namespace Pierre.Avenant.Assignment.Test.IntegrationTests.Infrastructure.Database
{
    [TestClass]
    public class EnumerableExtensionsTest
    {
        [TestMethod]
        public void GetDataSet_Success()
        {
            var accountRecord = new List<AccountTransaction>()
            {
                new AccountTransaction()
                {
                    Account = "Account",
                    AccountTransactionId = 1,
                    CurrencyCode = "GBP",
                    Description = "Desc",
                    FileUploadId = 1,
                    Amount = 1
                }
            };
            var dt = EnumerableExtensions.AsDataTable<AccountTransaction>(accountRecord); // accountRecord.AsDataTable();
            Assert.AreEqual(1,dt.Rows.Count);
            Console.WriteLine(dt.Rows[0]["Account"]);
        }
    }
}
