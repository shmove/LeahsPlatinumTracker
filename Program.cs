namespace LeahsPlatinumTracker
{
    internal static class Program
    {

        internal const string Version = "0.1.0";

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.SetCompatibleTextRenderingDefault(true);
            Application.Run(new Form1());
        }
    }
}