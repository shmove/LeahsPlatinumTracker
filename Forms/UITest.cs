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
    public partial class UITest : TrackerForm
    {

        public UITest() : base()
        {
            InitializeComponent();
            base.MainPanel = MainPanel;
            base.LinkButton = LinkButton;
            base.UnlinkButton = UnlinkButton;
            base.UndoButton = UndoButton;
            base.RedoButton = RedoButton;
            base.SaveButton = SaveButton;
            base.NotesButton = NotesButton;
        }

        public UITest(Tracker _player, string LoadedFile) : base(_player, LoadedFile)
        {
            InitializeComponent();
            base.MainPanel = MainPanel;
            base.LinkButton = LinkButton;
            base.UnlinkButton = UnlinkButton;
            base.UndoButton = UndoButton;
            base.RedoButton = RedoButton;
            base.SaveButton = SaveButton;
            base.NotesButton = NotesButton;
        }

        private void UITest_Load(object sender, EventArgs e)
        {
            base.TrackerForm_Load(sender, e);

            // Map Sectors / Sub-forms

            var classType = typeof(MapsForm);
            mapPanels = classType.Assembly.DefinedTypes
                .Where(x => classType.IsAssignableFrom(x) && x != classType)
                .ToList();

            LoadMapPanel("Sandgem");

            // https://stackoverflow.com/a/32795682
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UpdateAllAppearances();
        }

    }
}
