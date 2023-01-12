using EhbOverFlow.Areas.Identity.Data;

namespace EhbOverFlow.ViewModels
{
    public class UserViewModel
    {
        public ApplicationUser User { get; set; }
        public string Role { get; set; } = "";

        public bool IsAdmin { get; set; }
    }
}
