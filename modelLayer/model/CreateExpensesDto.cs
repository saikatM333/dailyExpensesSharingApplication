using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace modelLayer.model
{
    public class CreateExpenseDto
    {
        [Required]
        [StringLength(100, ErrorMessage = "Description length can't be more than 100.")]
        public string Description { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
        public decimal Amount { get; set; }

        [Required]
        [RegularExpression("Equal|Exact|Percentage", ErrorMessage = "Invalid split method.")]
        public string SplitMethod { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "At least two participants are required.")]
        public List<ExpenseParticipantDto> ExpenseParticipants { get; set; }
    }
}
