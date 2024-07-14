using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TypingWebApi.Data.Models
{
    public class Record
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } 
        public int Wpm { get; set; }
        public int Raw { get; set; }
        public int Accuracy { get; set; }
        public int Consistency { get; set; }
        public int Chars { get; set; }
        public int MatchTime { get; set; }
        public DateTime DateRecord { get; set; } = DateTime.Now;

        public string UserId { get; set; }
        public User User { get; set; }
    }
}
