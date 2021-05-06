using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace LendingTree.Models
{
    public class LendingContext : DbContext
    {
        public LendingContext() : base("LendingContext") { }

        public DbSet<User> Users { get; set; }

        public DbSet<Ticket> Tickets { get; set; }

        public DbSet<Admin> Admins { get; set; }

        public DbSet<Agent> Agents { get; set; }

        public DbSet<Loan> Loans { get; set; }

        public virtual DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) { }
    }
}