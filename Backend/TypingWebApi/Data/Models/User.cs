using Microsoft.AspNetCore.Identity;

namespace TypingWebApi.Data.Models
{
    public class User : IdentityUser
    {
        public string FullName { get; set; }
        public ICollection<Record> Records { get; set; }

    }
}
