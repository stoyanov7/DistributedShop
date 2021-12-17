namespace DistributedShop.Common.Mediator
{
    using DistributedShop.Common.Mediator.Contracts;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public static partial class ConfigurationExtensions
    {
        public static IServiceCollection AddMediator(this IServiceCollection services)
            => services
                .AddTransient<IMediator, Mediator>()
                .AddTransient<IQueryMediator, QueryMediator>()
                .AddTransient<ICommandMediator, CommandMediator>();
    }
}
