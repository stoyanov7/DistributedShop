namespace DistributedShop.Common.Mvc
{
    using DistributedShop.Common.Types;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class StartupInitializer : IStartupInitializer
    {
        private readonly ISet<IInitializer> initializers = new HashSet<IInitializer>();

        public void AddInitializer(IInitializer initializer) => this.initializers.Add(initializer);

        public async Task InitializeAsync() => await Task.WhenAll(this.initializers.Select(i => i.InitializeAsync()));
    }
}
