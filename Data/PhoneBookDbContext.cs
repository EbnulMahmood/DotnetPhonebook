using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotnetPhonebook.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace DotnetPhonebook.Data
{
    public class PhoneBookDbContext : DbContext
    {
        public PhoneBookDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Phone> Phones { get; set; }
    }
}