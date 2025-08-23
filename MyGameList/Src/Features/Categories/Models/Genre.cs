using System.ComponentModel.DataAnnotations.Schema;

namespace MyGameList.Src.Features.Categories.Models
{
    [Table("genres")]
    public class Genre
    {
        public Genre()
        {
            Option = string.Empty;
        }

        public Genre(int id, string option, DateTime? createdAt, DateTime? updatedAt)
        {
            Id = id;
            Option = option;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        [Column("id")]
        public int Id { get; set; }
        [Column("option")]
        public string Option { get; set; }
        [Column("created_at")]
        public DateTime? CreatedAt { get; set; }
        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }
    }
}
