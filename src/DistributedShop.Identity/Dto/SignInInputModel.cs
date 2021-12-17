namespace DistributedShop.Identity.Dto
{
    using DistributedShop.Common.Authentication;
    using DistributedShop.Common.Mediator.Types;

    public class SignInInputModel : IQuery<JsonWebToken>
    {
        public SignInInputModel(string email, string password)
        {
            this.Email = email;
            this.Password = password;
        }

        public string Email { get; }

        public string Password { get; }
    }
}
