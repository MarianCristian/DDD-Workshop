using AccountManagement.DTO;
using Common.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManagement.Commands
{
    public class InitiateTransactionCommand : ICommand
    {
        public TransactionModel Transaction { get; set; }
    }

    public class InitiateTransactionCommandResponse : ICommandResponse
    {
    }
}
