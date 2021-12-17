namespace DistributedShop.Identity.Controllers
{
    using DistributedShop.Common.Authentication;
    using DistributedShop.Common.Mediator.Contracts;
    using DistributedShop.Common.Mvc;
    using DistributedShop.Identity.Dto;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class IdentityController : ApiController
    {
        public IdentityController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        [Route(nameof(SignUp))]
        public async Task<IActionResult> SignUp([FromBody] SignUpInputModel model)
            => await this.SendAsync(model.BindId(x => x.Id));

        [HttpPost]
        [Route(nameof(SignIn))]
        public async Task<JsonWebToken> SignIn([FromBody] SignInInputModel model)
            => await this.QueryAsync(model);
    }
}
