using Microsoft.Win32;
using System.Drawing.Text;
using System.IO;
using System.Runtime.InteropServices;

namespace LeahsPlatinumTracker
{
    internal static class Program
    {

        /// <summary>
        /// The current version number of Leah's Platinum Tracker.
        /// </summary>
        internal const string Version = "1.0.0";
        // Updating via Squirrel
        // https://github.com/Squirrel/Squirrel.Windows/blob/develop/docs/getting-started/2-packaging.md
        // https://www.youtube.com/watch?v=W8Qu4qMJyh4

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            RegisterFont("Nirmala.ttf");
            RegisterFont("Pokemon DPPt.ttf");
            RegisterFont("PowerClear.ttf");
            RegisterFont("PowerClear-Bold.ttf");

            ApplicationConfiguration.Initialize();
            Application.SetCompatibleTextRenderingDefault(true);
            Application.Run(new Index());
        }


        // Fonts
        [DllImport("gdi32", EntryPoint = "AddFontResource")]
        public static extern int AddFontResourceA(string lpFileName);
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern int AddFontResource(string lpszFilename);
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern int CreateScalableFontResource(uint fdwHidden, string
            lpszFontRes, string lpszFontFile, string lpszCurrentPath);
        internal static bool HasInstalledNewFonts = false;

        /// <summary>
        /// Installs font on the user's system and adds it to the registry so it's available on the next session
        /// Your font must be included in your project with its build path set to 'Content' and its Copy property
        /// set to 'Copy Always'.
        /// From <see href="https://stackoverflow.com/a/29334492"/>.
        /// </summary>
        /// <param name="contentFontName">Your font to be passed as a resource (i.e. "myfont.tff")</param>
        private static void RegisterFont(string contentFontName)
        {
            // Creates the full path where your font will be installed
            var fontDestination = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Fonts), contentFontName);

            if (!File.Exists(fontDestination))
            {
                // Copies font to destination
                System.IO.File.Copy(Path.Combine(System.IO.Directory.GetCurrentDirectory(), contentFontName), fontDestination);

                // Retrieves font name
                // Makes sure you reference System.Drawing
                PrivateFontCollection fontCol = new PrivateFontCollection();
                fontCol.AddFontFile(fontDestination);
                var actualFontName = fontCol.Families[0].Name;

                //Add font
                AddFontResource(fontDestination);
                //Add registry entry   
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", actualFontName, contentFontName, RegistryValueKind.String);
                HasInstalledNewFonts = true;
            }
        }

    }
}