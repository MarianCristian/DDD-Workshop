using AccountManagement.Commands;
using AccountManagement.Domain;
using AccountManagement.DTO;
using Common.Messages;
using Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Transaction = AccountManagement.Domain.Transaction;

namespace AccountManagement.CommandHandlers
{
    public class BankAccountCommandHandler :
        IHandleCommand<RegisterBankAccountCommand>,
        IHandleCommand<InitiateTransactionCommand>
    {
        private readonly IEventStore eventStore;
        private readonly IEventPublisher eventPublisher;

        public BankAccountCommandHandler(IEventStore eventStore, IEventPublisher eventPublisher)
        {
            this.eventStore = eventStore;
            this.eventPublisher = eventPublisher;
        }

        public ICommandResponse Handle(RegisterBankAccountCommand command)
        {
            BankAccount account = new BankAccount();

            account.Register(command.BankAccount);

            eventStore.Save(account);

            account.PendingEvents.ForEach(e => eventPublisher.Publish(e));

            return new RegisterBankAccountCommandResponse
            {
                BankAccountId = account.Id
            };
        }

        public ICommandResponse Handle(InitiateTransactionCommand command)
        {
            var bankAccount = eventStore.GetById<BankAccount>(command.Transaction.BankAccountId);
            //Validations

            bankAccount.InititateTransaction(new Transaction(command.Transaction.Amount));

            eventStore.Save(bankAccount);

            bankAccount.PendingEvents.ForEach(e => eventPublisher.Publish(e));

            return new InitiateTransactionCommandResponse { };
        }
    }
}
