using EhbOverFlow.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace EhbOverFlow.Models
{
    public class Note
    {

        public int Id { get; set; }
        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; } = "";
        [Required]
        [Display(Name = "Content")]
        public string Body { get; set; } = "";

        public DateTime? CreatedDate { get; set; } = DateTime.Now;

        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }

    }
}
