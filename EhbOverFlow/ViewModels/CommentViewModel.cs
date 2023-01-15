using EhbOverFlow.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace EhbOverFlow.ViewModels
{
    public class CommentViewModel
    {
        [Required]
        public int NoteId { get; set; }
        [Required]

        public int MainCommentId { get; set; }
        [Required]
        [Display(Name = "Comment")]
        public string Message { get; set; } = "";
     
    }
}
