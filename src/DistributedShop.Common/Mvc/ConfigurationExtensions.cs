namespace DistributedShop.Common.Mvc
{
    using DistributedShop.Common.Mediator.Types;
    using DistributedShop.Common.Types;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    public static partial class ConfigurationExtensions
    {
        public static IServiceCollection AddInitializers(this IServiceCollection services, params Type[] initializers)
            => initializers == null
                ? services
                : services.AddTransient<IStartupInitializer, StartupInitializer>(c =>
                {
                    var startupInitializer = new StartupInitializer();
                    var validInitializers = initializers.Where(t => typeof(IInitializer).IsAssignableFrom(t));

                    foreach (var initializer in validInitializers)
                    {
                        startupInitializer.AddInitializer(c.GetService(initializer) as IInitializer);
                    }

                    return startupInitializer;
                });

        public static IServiceCollection AddServices(this IServiceCollection services, Assembly assembly)
        {
            services.Scan(scan => scan.FromAssemblies(assembly).AddClasses().AsMatchingInterface());

            services
                .Scan(scan => scan.FromAssemblies(assembly)
                    .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime());

            services
                .Scan(scan => scan.FromAssemblies(assembly)
                    .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>)))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime());

            return services;
        }

        public static T BindId<T>(this T model, Expression<Func<T, Guid>> expression)
           => model.Bind(expression, Guid.NewGuid());

        public static T Bind<T>(this T model, Expression<Func<T, object>> expression, object value)
           => model.Bind<T, object>(expression, value);

        private static TModel Bind<TModel, TProperty>(this TModel model, Expression<Func<TModel, TProperty>> expression,
            object value)
        {
            var memberExpression = expression.Body as MemberExpression;

            if (memberExpression == null)
                memberExpression = ((UnaryExpression)expression.Body).Operand as MemberExpression;

            var propertyName = memberExpression.Member.Name.ToLowerInvariant();
            var modelType = model.GetType();
            var field = modelType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
                .SingleOrDefault(x => x.Name.ToLowerInvariant().StartsWith($"<{propertyName}>"));

            if (field == null)
                return model;

            field.SetValue(model, value);

            return model;
        }
    }
}
