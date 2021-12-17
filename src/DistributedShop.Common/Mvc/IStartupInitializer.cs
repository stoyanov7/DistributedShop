namespace DistributedShop.Common.Mvc
{
    using DistributedShop.Common.Types;

    public interface IStartupInitializer : IInitializer
    {
        void AddInitializer(IInitializer initializer);
    }
}
