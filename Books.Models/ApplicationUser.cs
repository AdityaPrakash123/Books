using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string Name { get; set; }
    }
}
