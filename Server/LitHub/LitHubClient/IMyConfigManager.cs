namespace LitHubClient
{
    public interface IAppConfigManager
    {
        bool UseLocalRepo { get; }
        bool UseRemoteRepo { get; }
        bool UseLocalDatabase { get; }
    }
}