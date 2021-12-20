namespace DistributedShop.Discounts.Controllers
{
    using DistributedShop.Common.Mediator.Contracts;
    using DistributedShop.Common.Mvc;
    using DistributedShop.Discounts.Dto;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class DiscountController : ApiController
    {
        public DiscountController(IMediator mediator)
           : base(mediator)
        {
        }

        [HttpPost]
        [Route(nameof(CreateDiscount))]
        public async Task<IActionResult> CreateDiscount([FromBody] CreateDiscountInputModel model)
            => await this.SendAsync(model.BindId(x => x.Id));
    }
}
