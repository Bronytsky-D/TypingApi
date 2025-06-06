﻿using System.ComponentModel.DataAnnotations;

namespace TypingWebApi.Dtos
{
    public class LoginDto
    {
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; }
    }
}
