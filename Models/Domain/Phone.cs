using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DotnetPhonebook.Models.Domain
{
    public class Phone
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Please enter phone mobile")]  
        [StringLength(13)] 
        [Phone]
        public string? Number { get; set; }

        [Required(ErrorMessage = "Please enter your name")]  
        [Display(Name = "Full name")]  
        [StringLength(100)]  
        public string? OwnerName { get; set; }

        [Required(ErrorMessage = "Please enter your email")]  
        [EmailAddress]
        public string? Email { get; set; }
    }
}