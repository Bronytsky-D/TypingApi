
namespace TypingWeb.Common.DTOs
{
    public class RecordWriteRequestDto
    {
            public int Wpm { get; set; }
            public int Raw { get; set; }
            public int Accuracy { get; set; }
            public int Chars { get; set; }
            public int MatchTime { get; set; }
            // mode can be an enum or int, depending on your design
            public int Mode { get; set; }
            public int GameLength { get; set; }

            public int? Consistency { get; set; }

            public string UserId { get; set; }
            public int Experience { get; set; }
            public string Language { get; set; }
    }
}
