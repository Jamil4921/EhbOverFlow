using EhbOverFlow.Areas.Identity.Data;
using EhbOverFlow.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace EhbOverFlow.ViewModels
{
    public class NoteViewModel
    {

        public int Id { get; set; }
        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; } = "";
        [Required]
        [Display(Name = "Content")]
        public string Body { get; set; } = "";

        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public bool Solved { get; set; } = false;
        public string CurrentImage { get; set; } = "";
        public string UserName { get; set; } = "";
        public IFormFile Image { get; set; } = null;

        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }

        public int? CategoryId { get; set; }
        public Category? Category { get; set; }

        public NoteViewModel()
        {
            
            User = new ApplicationUser();
            
        }
    }
}
