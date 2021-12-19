namespace DistributedShop.Products.Controllers
{
    using DistributedShop.Common.Mediator.Contracts;
    using DistributedShop.Common.Mvc;
    using DistributedShop.Products.Dto;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class ProductController : ApiController
    {
        public ProductController(IMediator mediator) 
            : base(mediator)
        {
        }

        [HttpPost]
        [Route(nameof(CreateProduct))]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductInputModel model)
            => await this.SendAsync(model.BindId(x => x.Id));
    }
}
