using System.ComponentModel.DataAnnotations;

namespace TypingWebApi
{
    public class UserDto
    {
        [Required]
        public required string FullName {  get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public required string Email { get; set; } = string.Empty;
        public required string Password { get; set; }= string.Empty;

    }
}
