﻿namespace MiniLoanManagementSystem.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string? Username { get; set; }
        public string? PasswordHash { get; set; }
        public string? Role { get; set; }
        public string? FullName { get; set; }
    }
}
