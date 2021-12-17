﻿namespace DistributedShop.Identity.Domain
{
    using DistributedShop.Common.Mongo.Attributes;
    using DistributedShop.Common.Types;
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Text.RegularExpressions;

    [BsonCollection("users")]
    public class User : IIdentifiable
    {
        private static readonly Regex EmailRegex = new(
           @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
           @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
           RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);

        public User(Guid id, string email, string role)
        {
            this.Id = id;
            this.SetEmail(email);
            this.SetRole(role);
            this.CreatedAt = DateTime.UtcNow;
            this.UpdatedAt = DateTime.UtcNow;
        }

        public Guid Id { get; private set; }

        public string Email { get; private set; }

        public string PasswordHash { get; private set; }

        public string Role { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public DateTime UpdatedAt { get; private set; }

        public void SetPassword(string password, IPasswordHasher<User> passwordHasher)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new Exception("Password can not be empty.");

            this.PasswordHash = passwordHasher.HashPassword(this, password);
        }

        public bool ValidatePassword(string password, IPasswordHasher<User> passwordHasher)
            => passwordHasher.VerifyHashedPassword(this, this.PasswordHash, password) != PasswordVerificationResult.Failed;

        private void SetEmail(string email)
        {
            if (!EmailRegex.IsMatch(email))
                throw new Exception($"Invalid email: '{email}'.");

            this.Email = email.ToLowerInvariant();
        }

        private void SetRole(string role)
        {
            if (!Domain.Role.IsValid(role))
                throw new Exception($"Invalid role: '{role}'.");

            this.Role = role.ToLowerInvariant();
        }
    }
}
