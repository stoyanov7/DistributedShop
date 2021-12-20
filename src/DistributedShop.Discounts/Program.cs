namespace DistributedShop.Discounts
{
    using DistributedShop.Common.Logging;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Hosting;

    public class Program
    {
        public static void Main(string[] args)
            => Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>().UseLogging())
                .Build()
                .Run();
    }
}
