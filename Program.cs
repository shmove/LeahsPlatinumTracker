using Squirrel;
using System.Threading.Tasks;

namespace LeahsPlatinumTracker
{
    internal static class Program
    {

        /// <summary>
        /// The current version number of Leah's Platinum Tracker.
        /// </summary>
        internal const string Version = "0.1.1";
        // Updating via Squirrel
        // https://github.com/Squirrel/Squirrel.Windows/blob/develop/docs/getting-started/2-packaging.md

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.SetCompatibleTextRenderingDefault(true);
            Application.Run(new Index());
        }
    }
}