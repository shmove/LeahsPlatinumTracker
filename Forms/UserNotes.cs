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
    public partial class UserNotes : Form
    {

        private Tracker Player { get; set; }

        // Constructor
        public UserNotes(Tracker _player)
        {
            InitializeComponent();
            Player = _player;
        }

        // Methods
        private void UserNotes_Load(object sender, EventArgs e)
        {
            TextBox.Text = Player.UserNotes;
            TextBox.TextChanged += TextBox_TextChanged;
        }

        private void TextBox_TextChanged(object? sender, EventArgs e)
        {
            Player.UserNotes = TextBox.Text;
        }

    }
}
