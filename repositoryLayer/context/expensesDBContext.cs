using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using repositoryLayer.entity;
namespace repositoryLayer.context
{
    public class expensesDBContext : DbContext
    {
        public expensesDBContext(DbContextOptions<expensesDBContext> options) : base(options) { }
            public DbSet<User> Users { get; set; }
            public DbSet<Expense> Expenses { get; set; }
            public DbSet<ExpenseParticipant> ExpenseParticipants { get; set; }
}
}
