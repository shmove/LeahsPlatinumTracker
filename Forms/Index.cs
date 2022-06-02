using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LeahsPlatinumTracker
{
    public partial class Index : Form
    {
        /// <summary>
        /// The currently loaded and active <see cref="TrackerForm"/>.
        /// </summary>
        private TrackerForm? SubForm { get; set; }

        public Index()
        {
            InitializeComponent();
        }

        private void Index_Load(object sender, EventArgs e)
        {
            VersionLabel.Text = "v" + Program.Version; // set version label
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
    }
}
