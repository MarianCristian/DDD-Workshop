using AccountManagement.Commands;
using AccountManagement.Domain;
using AccountManagement.DTO;
using AccountManagement.Projections;
using AccountManagement.Queries;
using Common.Messages;
using Common.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankAccountController : ControllerBase
    {
        private readonly ICommandDispatcher commandDispatcher;
        private readonly IQueryDispatcher queryDispatcher;

        public BankAccountController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            this.commandDispatcher = commandDispatcher;
            this.queryDispatcher = queryDispatcher;
        }

        [HttpGet]
        [Route("api/GetBankAccount")]
        public BankAccountProjection GetBankAccount(Guid bankAccountId)
        {
            return queryDispatcher.Dispatch<BankAccountQuery, BankAccountProjection>(new BankAccountQuery { BankAccountId = bankAccountId});
        }

        [HttpPost]
        [Route("api/InitiateTransaction")]
        public HttpResponseMessage InitiateTransaction(InitiateTransactionCommand command)
        {
            return commandDispatcher.Dispatch(command);
        }

        [HttpPost]
        [Route("api/RegisterBankAccount")]
        public HttpResponseMessage Register(RegisterBankAccountCommand command)
        {
            return commandDispatcher.Dispatch(command);
        }
    }
}
