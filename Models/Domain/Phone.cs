using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetPhonebook.Models.Domain
{
    public class Phone
    {
        public Guid Id { get; set; }
        public string? Number { get; set; }
        public string? OwnerName { get; set; }
        public string? Email { get; set; }
    }
}