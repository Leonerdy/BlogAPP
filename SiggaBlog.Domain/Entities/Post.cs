namespace SiggaBlog.Domain.Entities
{
    public class Post
    {
        public int UserId { get; set; }         
        public int Id { get; set; }         
        public string Title { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;

        public Post(int id, string title, string body)
        {
            Id = id;
            Title = title;
            Body = body;
        }
    }
}
