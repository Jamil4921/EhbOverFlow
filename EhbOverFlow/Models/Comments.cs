﻿using EhbOverFlow.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace EhbOverFlow.Models
{
    public class Comments
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Comment")]
        public string Message { get; set; } = "";
        public DateTime Created { get; set; }

        public string? UserName { get; set; } = "";
        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }

    }
}
