namespace DistributedShop.Common
{
    using Microsoft.Extensions.Configuration;
    using System;

    public static class Extensions
    {
        public static TModel GetOptions<TModel>(this IConfiguration configuration) where TModel : new()
            => configuration.GetSection(typeof(TModel).Name).Get<TModel>();

        public static long ToTimestamp(this DateTime dateTime)
        {
            var centuryBegin = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var expectedDate = dateTime.Subtract(new TimeSpan(centuryBegin.Ticks));

            return expectedDate.Ticks / 10000;
        }
    }
}
