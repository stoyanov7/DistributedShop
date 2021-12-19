namespace DistributedShop.Products
{
    using DistributedShop.Common.Mediator;
    using DistributedShop.Common.Mongo;
    using DistributedShop.Common.Mongo.Contracts;
    using DistributedShop.Common.Mvc;
    using DistributedShop.Products.Domain;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.OpenApi.Models;
    using System.Reflection;

    public class Startup
    {
        public Startup(IConfiguration configuration) => this.Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddInitializers(typeof(IMongoDbInitializer))
                .AddMongoDatabase(this.Configuration)
                .AddScoped(typeof(IMongoRepository<Product>), typeof(MongoRepository<Product>))
                .AddServices(Assembly.GetExecutingAssembly())
                .AddMediator()
                .AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "DistributedShop.Products", Version = "v1" });
                })
                .AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IStartupInitializer startupInitializer)
        {
            if (env.IsDevelopment())
            {
                app
                    .UseDeveloperExceptionPage()
                    .UseSwagger()
                    .UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DistributedShop.Products v1"));
            }

            app.UseHttpsRedirection()
                .UseRouting()
                .UseAuthorization()
                .UseEndpoints(endpoints => endpoints.MapControllers());

            startupInitializer.InitializeAsync();
        }
    }
}
