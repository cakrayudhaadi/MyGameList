namespace MyGameList.Models
{
    public class Games
    {
        public Games() {}

        public Games(int id, string title, string description, DateTime? createdAt, DateTime? updatedAt)
        {
            Id = id;
            Title = title;
            Description = description;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
