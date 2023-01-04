﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace EhbOverFlow.Areas.Identity.Data
{
    public class ApplicationUser : IdentityUser
    {

        [Required]
        [Display(Name = "First name")]
        public string FirstName { get; set; } = "?";
		[Required]
		[Display(Name = "Last name")]
		public string LastName { get; set; } = "?";
    }
}
