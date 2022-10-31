using AccountManagement.Domain;
using AccountManagement.DTO;
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
        private readonly IEventPublisher eventPublisher;
        private readonly IEventStore eventStore;

        public BankAccountController(IEventPublisher eventPublisher, IEventStore eventStore)
        {
            this.eventPublisher = eventPublisher;
            this.eventStore = eventStore;
        }

        [HttpPost]
        [Route("api/InitiateTransaction")]
        public HttpResponse InitiateTransaction(TransactionModel transaction)
        {
            var bankAccount = eventStore.GetById<BankAccount>(transaction.BankAccountId);
            //Validations

            bankAccount.InititateTransaction(new Transaction(transaction.Amount));

            eventStore.Save(bankAccount);

            bankAccount.PendingEvents.ForEach(e => eventPublisher.Publish(e));

            return null;
        }

        [HttpPost]
        [Route("api/RegisterBankAccount")]
        public HttpResponse Register(BankAccountModel bankAccountModel)
        {
            BankAccount account = new BankAccount();

            account.Register(bankAccountModel);

            eventStore.Save(account);

            account.PendingEvents.ForEach(e => eventPublisher.Publish(e));

            return null;
        }
    }
}
