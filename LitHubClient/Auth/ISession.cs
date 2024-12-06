namespace LitHubClient.Auth
{
    public interface ISession
    {
        string LocalDataBasePassword { get; set; }
        bool UseLocalDataBase { get; set; }
        string LocalDataBaseFile { get; set; }
        string ServerUrl { get; set; }
        string ServerPassword { get; set; }
        string ServerLogin { get; set; }
    }
}