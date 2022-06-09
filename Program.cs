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

        // Main

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AddFileFontToCollection("Pokemon DPPt.ttf", CustomFonts);
            //AddFileFontToCollection("PowerClear.ttf", CustomFonts);
            AddFileFontToCollection("PowerClearB.ttf", CustomFonts);

            ApplicationConfiguration.Initialize();
            Application.SetCompatibleTextRenderingDefault(true);
            Application.Run(new Index());
        }


        // Fonts

        /// <summary>
        /// Custom fonts used by Leah's Platinum Tracker.       </ br>
        /// 0: Pokemon DPPt                                     </ br>
        /// 1: Power Clear (/bold)                              </ br>
        /// </summary>
        public static PrivateFontCollection CustomFonts = new();

        /// Before build, replace every instance of X with Y, and vice versa for design.
        /// 
        ///  X:   Font("Pokemon DPPt"
        ///  Y:   Font(Program.CustomFonts.Families[0]
        ///
        ///  X:   Font("Power Clear"
        ///  Y:   Font(Program.CustomFonts.Families[1]
        ///

        private static void AddFileFontToCollection(string fontName, PrivateFontCollection collection)
        {
            string fontPath = Path.Combine(Directory.GetCurrentDirectory(), fontName);
            collection.AddFontFile(fontPath);
        }

    }
}