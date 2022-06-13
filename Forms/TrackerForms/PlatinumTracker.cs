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
    public partial class PlatinumTracker : TrackerForm
    {

        public PlatinumTracker() : base("PokemonPlatinum")
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

        public PlatinumTracker(Tracker _player, string LoadedFile) : base(_player, LoadedFile)
        {
            InitializeComponent();
            base.MainPanel = MainPanel;
            base.LinkButton = LinkButton;
            base.UnlinkButton = UnlinkButton;
            base.UndoButton = UndoButton;
            base.RedoButton = RedoButton;
            base.SaveButton = SaveButton;
            base.NotesButton = NotesButton;

            base.UpdateWindowTitle();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UpdateAllAppearances();
        }

    }
}
