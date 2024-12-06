using System.Threading.Tasks;

namespace LitHub.Deploy
{
    /// <summary>
    /// Deploy Service interface
    /// </summary>
    public interface IDeployService
    {
        /// <summary>
        /// Deploy DB method
        /// </summary>
        /// <param name="num">last update</param>
        /// <returns></returns>
        Task Deploy(int? num = null);
    }
}