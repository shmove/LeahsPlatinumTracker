using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Squirrel;

namespace LeahsPlatinumTracker
{
    public partial class Index : Form
    {
        /// <summary>
        /// The currently loaded and active <see cref="TrackerForm"/>.
        /// </summary>
        private TrackerForm? SubForm { get; set; }

        private UpdateManager Manager;

        public Index()
        {
            InitializeComponent();
            if (Program.HasInstalledNewFonts)
            {
                MessageBox.Show("New fonts installed! Restarting program...", "Required fonts installed");
                Application.Restart();
                Environment.Exit(0);
            }
        }

        private async void Index_Load(object sender, EventArgs e)
        {
            VersionLabel.Text = "v" + Program.Version; // set version label

            if (System.Diagnostics.Debugger.IsAttached) return; // early exit in debug mode

            try
            {
                Manager = await UpdateManager.GitHubUpdateManager(@"https://github.com/shmove/LeahsPlatinumTracker");
                CheckForUpdates();
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an error checking for new updates. Is something blocking this program from accessing the internet?\nError: " + ex.InnerException?.Message, "Update error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void NewFileButton_Click(object sender, EventArgs e)
        {
            SubForm = new PlatinumTracker();
            Hide();
            SubForm.ShowDialog();
            Show();
        }

        private void LoadFileButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();

            string folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "LeahsPlatinumTracker");
            Directory.CreateDirectory(folder);

            fileDialog.InitialDirectory = folder;
            fileDialog.Filter = "LPT save files (*.lpt, *.json)|*.lpt;*.json";
            fileDialog.FilterIndex = 0;
            fileDialog.RestoreDirectory = true;

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedFileName = fileDialog.FileName;
                string json = File.ReadAllText(selectedFileName);

                Tracker Player = TrackerManager.FromJSON(json);
                if (Player != null)
                {
                    SubForm = new PlatinumTracker(Player, selectedFileName);
                    Hide();
                    SubForm.ShowDialog();
                    Show();
                }
            }
        }

        private async Task CheckForUpdates()
        {
            ReleaseEntry release = null;
            var updateInfo = await Manager.CheckForUpdate();

            if (updateInfo.ReleasesToApply.Count > 0)
            {
                DialogResult updateDialog = 
                    MessageBox.Show(
                        "Your version of Leah's Platinum Tracker is outdated. Do you want to install the new version?" +
                        "\n(Current version: v" + updateInfo.CurrentlyInstalledVersion.Version + ")" +
                        "\n(New version: v" + updateInfo.FutureReleaseEntry.Version + ")",
                            
                        "New update available", 
                        MessageBoxButtons.YesNo
                    );
                if (updateDialog == DialogResult.Yes)
                {
                    release = await Manager.UpdateApp();
                }

                if (release != null)
                {
                    MessageBox.Show("Successfully updated! Restarting application...", "Update successful");
                    UpdateManager.RestartApp();
                }
            }
        }

    }
}
