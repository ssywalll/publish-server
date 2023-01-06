using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Application.BankAccounts.Commands.CreateBankAccount;
using CleanArchitecture.Application.BankAccounts.Queries.GetBankAccounts;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.WebUI.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    [ApiController]
    public class BankAccountsController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<BankAccountsVm>> Get()
        {
            return await Mediator.Send(new GetBankAccountQuery());
        }

        [HttpPost]
        public async Task<ActionResult<BankAccount>> Create(CreateBankAccountCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}