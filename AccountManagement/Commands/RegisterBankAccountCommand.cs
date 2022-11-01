using AccountManagement.DTO;
using Common.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManagement.Commands
{
    public class RegisterBankAccountCommand : ICommand
    {
        public BankAccountModel BankAccount { get; set; }
    }

    public class RegisterBankAccountCommandResponse : ICommandResponse
    {
        public Guid BankAccountId { get; set; }
    }
}
