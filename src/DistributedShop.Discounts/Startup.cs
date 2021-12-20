namespace DistributedShop.Discounts
{
    using DistributedShop.Common.Mediator;
    using DistributedShop.Common.Mongo;
    using DistributedShop.Common.Mongo.Contracts;
    using DistributedShop.Common.Mvc;
    using DistributedShop.Common.Swagger;
    using DistributedShop.Discounts.Domain;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using System.Reflection;

    public class Startup
    {
        public Startup(IConfiguration configuration) => this.Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
            => services
                .AddInitializers(typeof(IMongoDbInitializer))
                .AddMongoDatabase(this.Configuration)
                .AddScoped(typeof(IMongoRepository<Discount>), typeof(MongoRepository<Discount>))
                .AddServices(Assembly.GetExecutingAssembly())
                .AddMediator()
                .AddSwagger(this.Configuration)
                .AddControllers();

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IStartupInitializer startupInitializer)
        {
            if (env.IsDevelopment())
            {
                app
                    .UseDeveloperExceptionPage()
                    .UseSwagger();
            }

            app.UseHttpsRedirection()
                .UseRouting()
                .UseAuthorization()
                .UseEndpoints(endpoints => endpoints.MapControllers());

            startupInitializer.InitializeAsync();
        }
    }
}
