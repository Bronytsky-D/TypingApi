namespace TypingWebApi.Dtos
{
    public class UserGetDto
    {
        public string FullName { get; set; }
        public int Level { get; set; }
        public int ExperiencePoints { get; set; }
        public int ExperienceToNextLevel { get; set; }
        public List<string> Achievements { get; set; }
    }
}
