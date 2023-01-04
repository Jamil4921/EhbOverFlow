namespace EhbOverFlow.Models
{
    public class Note
    {

        public string Title { get; set; } = "";
        public string Body { get; set; } = "";

        public DateTime? CreatedDate { get; set; } = DateTime.Now;
    }
}
