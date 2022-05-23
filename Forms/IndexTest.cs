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
    public partial class IndexTest : Form
    {
        public Tracker Player { get; set; }
        private UITest? SubForm { get; set; }

        public IndexTest()
        {
            InitializeComponent();
            Player = new Tracker();
        }

        private void button62_Click(object sender, EventArgs e)
        {
            SubForm = new UITest();
            Hide();
            SubForm.ShowDialog();
            Show();
        }

        private void button1_Click(object sender, EventArgs e)
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
                
                Player = TrackerManager.FromJSON(json);
                SubForm = new UITest();
                SubForm.Player = Player;
                Hide();
                SubForm.ShowDialog();
                Show();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
