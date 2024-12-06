using LitHubClient.Auth;
using System;
using System.Configuration;
using System.Windows.Forms;
using Unity;

namespace LitHubClient
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var container = Startup.CreateContainer();
            Application.Run(container.Resolve<IMainForm>() as Form);
        }
    }

    internal static class Startup
    {
        public static IUnityContainer CreateContainer()
        {
            IUnityContainer container = new UnityContainer();
            container.RegisterType<IMainForm, MainForm>();
            container.RegisterSingleton<ISession, Session>();
            container.RegisterType<IAppConfigManager, AppConfigManager>();

            return container;
        }
    }

    public class AppConfigManager : IAppConfigManager
    {
        public bool UseLocalRepo => bool.Parse(ConfigurationManager.AppSettings["UseLocalRepo"]);
        public bool UseRemoteRepo => bool.Parse(ConfigurationManager.AppSettings["UseRemoteRepo"]);
        public bool UseLocalDatabase => bool.Parse(ConfigurationManager.AppSettings["UseLocalDatabase"]);
    }
}
