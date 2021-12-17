namespace DistributedShop.Identity.Dto
{
    using DistributedShop.Common.Mediator.Contracts;
    using System;

    public class SignUpInputModel : ICommand
    {
        public SignUpInputModel(Guid id, string email, string password, string role)
        {
            this.Id = id;
            this.Email = email;
            this.Password = password;
            this.Role = role;
        }

        public Guid Id { get; }

        public string Email { get; }

        public string Password { get; }

        public string Role { get; set; }
    }
}
