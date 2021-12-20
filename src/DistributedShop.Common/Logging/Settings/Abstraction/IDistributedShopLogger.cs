namespace DistributedShop.Common.Logging.Settings.Abstraction
{
    public interface IDistributedShopLogger
    {
        void Debug(string message);

        void Info(string message);

        void Warning(string message);

        void Error(string message);
    }
}
