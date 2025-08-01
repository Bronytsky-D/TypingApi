namespace TypingWeb.Domain.Abstractions.Services
{
    public interface IUserGameService
    {
        public Task<bool> AddExperienceAsync(string userId, int xp);
    }
}
