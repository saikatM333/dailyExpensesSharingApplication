using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace repositoryLayer.entity
{
    public class ExpenseParticipant
    {
        public int ExpenseParticipantId { get; set; }
        public int ExpenseId { get; set; }
        public Expense Expense { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public decimal AmountOwed { get; set; }
        public decimal PercentageOwed { get; set; }
    }
}
