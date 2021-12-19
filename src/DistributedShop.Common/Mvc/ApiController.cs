namespace DistributedShop.Common.Mvc
{
    using DistributedShop.Common.Mediator.Contracts;
    using DistributedShop.Common.Mediator.Types;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [ApiController]
    [Route("[controller]")]
    public abstract class ApiController : ControllerBase
    {
        private readonly IMediator mediator;

        public ApiController(IMediator mediator) => this.mediator = mediator;

        public const string PathSeparator = "/";
        public const string Id = "{id}";

        protected async Task<TResult> QueryAsync<TResult>(IQuery<TResult> query) => await this.mediator.QueryAsync(query);

        protected async Task<IActionResult> SendAsync<TModel>(TModel model) where TModel : ICommand
        {
            await this.mediator.SendAsync(model);

            return this.Accepted();
        }

        protected ActionResult<T> Single<T>(T data)
        {
            if (data is null)
                return this.NotFound();

            return this.Ok(data);
        }
    }
}
