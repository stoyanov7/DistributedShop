namespace DistributedShop.Identity.Handlers
{
    using DistributedShop.Common.Mediator.Types;
    using DistributedShop.Identity.Domain;
    using DistributedShop.Identity.Dto;
    using DistributedShop.Identity.Repositories.Contracts;
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Threading.Tasks;

    public class SignUpHandler : ICommandHandler<SignUpInputModel>
    {
        private readonly IUserRepository userRepository;
        private readonly IPasswordHasher<User> passwordHasher;

        public SignUpHandler(IUserRepository userRepository, IPasswordHasher<User> passwordHasher)
        {
            this.userRepository = userRepository;
            this.passwordHasher = passwordHasher;
        }

        public async Task HandleAsync(SignUpInputModel model)
        {
            var user = await this.userRepository.GetAsync(model.Email);

            if (user != null)
                throw new Exception($"Email: '{model.Email}' is already in use.");

            if (string.IsNullOrWhiteSpace(model.Role))
                model.Role = Role.User;

            user = new User(model.Id, model.Email, model.Role);
            user.SetPassword(model.Password, passwordHasher);
            await this.userRepository.AddAsync(user);
        }
    }
}
