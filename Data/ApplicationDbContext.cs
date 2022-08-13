using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotnetPhonebook.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace DotnetPhonebook.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Phone> Phones { get; set; }
        // public DbSet<Account> Accounts { get; set; }
    }
}