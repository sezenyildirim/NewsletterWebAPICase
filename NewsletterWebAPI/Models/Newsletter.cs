namespace NewsletterWebAPI.Models
{
    public class Newsletter
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }=DateTime.Now;
    }
}
