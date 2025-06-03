namespace TypingWebApi.Dtos
{
    public class WriteProgressDto
    {
        public string UserId { get; set; }
        public int LessonId { get; set; }
        public int BestWpm { get; set; }
        public int BestRaw { get; set; }
        public int BestAccuracy { get; set; }
    }
}
