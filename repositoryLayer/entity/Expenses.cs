using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace repositoryLayer.entity
{
    public class Expense
    {
        public int ExpenseId { get; set; }
        public string Description { get; set; }
        public decimal TotalAmount { get; set; }
        public string SplitMethod { get; set; }
        public ICollection<ExpenseParticipant> ExpenseParticipants { get; set; }
    }
}
