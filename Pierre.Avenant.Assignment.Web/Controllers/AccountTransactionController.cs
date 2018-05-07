using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Pierre.Avenant.Assignment.Core.Entities;
using Pierre.Avenant.Assignment.Core.Interfaces.Database;
using Pierre.Avenant.Assignment.Infrastructure.Database;
using Pierre.Avenant.Assignment.Web.Models;

namespace Pierre.Avenant.Assignment.Web.Controllers
{
    public class AccountTransactionController : Controller
    {
        private Configuration _config;
        private IAccountTransactionRepository _accountTransaction;

        public AccountTransactionController(IOptions<Configuration> configuration, IAccountTransactionRepository accountTransaction)
        {
            _config = configuration.Value;
            _accountTransaction = accountTransaction;
        }

        public IActionResult List(int fileUploadId,string fileName)
        {
            AccountTransactionModel model = new AccountTransactionModel(_accountTransaction, fileUploadId,fileName);
            return View(model);
        }
    }
}