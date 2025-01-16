using System.Collections.Generic;

namespace LitHub.Contract
{
    public interface IGitService
    {
        string GitClone(string url, string localPath);
        void GitCreate(Hub value);
        IEnumerable<Hub> GetHubs();
        Hub GetHub(int id);
        void GitUpdate(Hub value);
        void GitDelete(int id);
    }
}