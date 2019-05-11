using Hasseeb.Application.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hasseeb.Repository
{
    public partial class MyContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"data source=.\SQLEXPRESS;initial catalog=HasseebDb;persist security info=True;user id=sa;password=123456;MultipleActiveResultSets=True;");
        }
        public MyContext(DbContextOptions<MyContext> options)
        : base(options)
        { }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountNature> AccountNatures { get; set; }
    }
}
