using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pierre.Avenant.Assignment.Core.Entities;
using Pierre.Avenant.Assignment.Infrastructure.Database;

namespace Pierre.Avenant.Assignment.Test.IntegrationTests.Infrastructure.Database
{
    [TestClass]
    public class AccountTransactionRepositoryTest
    {
        [TestMethod]
        public void Save_Success()
        {
            var accountTransactionRespositoryRepository = new AccountTransactionRepository(Configuration.ConnectionString);
            var accountTransaction = new AccountTransaction(){ Account = "account",Description = "Description",CurrencyCode = "GBP",Amount = 1.00m};
            accountTransactionRespositoryRepository.Save(accountTransaction);
            Assert.AreNotEqual(0, accountTransaction.AccountTransactionId);
        }
    }
}
