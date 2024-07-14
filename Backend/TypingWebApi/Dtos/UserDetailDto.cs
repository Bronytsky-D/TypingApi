﻿namespace TypingWebApi.Dtos
{
    public class UserDetailDto
    {
        public string? Id { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string[]? Roles { get; set; }
        public bool TwoFacotrEnabled { get; set; }
    }
}
