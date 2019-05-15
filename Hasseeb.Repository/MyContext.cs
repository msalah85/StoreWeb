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
            optionsBuilder.UseSqlServer(@"Server=.;Database=ManyHsasseeb;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
        public MyContext(DbContextOptions<MyContext> options)
        : base(options)
        { }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountNature> AccountNatures { get; set; }
    }
}
