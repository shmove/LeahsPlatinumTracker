namespace LeahsPlatinumTracker
{
    internal static class Program
    {

        internal const string Version = "0.1.1";

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.SetCompatibleTextRenderingDefault(true);
            Application.Run(new IndexTest());
        }
    }
}