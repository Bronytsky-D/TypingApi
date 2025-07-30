using Microsoft.AspNetCore.Identity;
using Moq;

namespace TypingWeb.Tests.Utils
{
    public static class UserManagerMockFactory
    {
        public static Mock<UserManager<TUser>> Create<TUser>() where TUser: class
        {
            var userStore = new Mock<IUserStore<TUser>>();
            return new Mock<UserManager<TUser>>(
                userStore.Object,
                null, // IOptions<IdentityOptions>
                null, // IPasswordHasher<TUser>
                new List<IUserValidator<TUser>>(),
                new List<IPasswordValidator<TUser>>(),
                null, // ILookupNormalizer
                null, // IdentityErrorDescriber
                null, // IServiceProvider
                null  // ILogger<UserManager<TUser>
                );
        }
    }
}
