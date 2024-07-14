namespace TypingWebApi.Dtos
{
    public class ReadUserRecords
    {
        public int Wpm { get; set; }
        public int Raw { get; set; }
        public int Accuracy { get; set; }
        public int Consistency { get; set; }
        public int Chars { get; set; }
        public int MatchTime { get; set; }
    }
}
