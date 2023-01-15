namespace EhbOverFlow.Models
{
    public class MainComment : Comments
    {
        public List<SubComment> SubComments { get; set; }
    }
}
