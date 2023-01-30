using EhbOverFlow.Areas.Identity.Data;

namespace EhbOverFlow.Models
{
    public class Likes
    {
        public int Id { get; set; }
        public int MainCommentId { get; set; }
        public string? UserId { get; set; }
        public MainComment? MainComment { get; set; }
        public ApplicationUser? User { get; set; }
    }
}
