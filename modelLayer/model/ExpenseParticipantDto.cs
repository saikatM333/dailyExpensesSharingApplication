using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace modelLayer.model
{
    public class ExpenseParticipantDto
    {
        public int UserId { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Amount owed must be non-negative.")]
        public decimal AmountOwed { get; set; }

        [Range(0, 100, ErrorMessage = "Percentage owed must be between 0 and 100.")]
        public decimal PercentageOwed { get; set; }// Used for percentage splits
    }
}
