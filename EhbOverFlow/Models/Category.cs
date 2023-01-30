using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace EhbOverFlow.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Subject")]
        public string SubjectName { get; set; } = "";

        public virtual List<Note>? CatNotes { get; set; }

    }
}
