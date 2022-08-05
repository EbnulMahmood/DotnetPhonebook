using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetPhonebook.Models.Domain
{
    public class Role
    {
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public string? RoleName { get; set; }
    }
}