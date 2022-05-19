using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeahsPlatinumTracker
{
    public class ClickablePictureBox : PictureBox
    {
        private bool Selected { get; set; } = false;

        public ClickablePictureBox()
        {
            SizeMode = PictureBoxSizeMode.AutoSize;
            MouseEnter += ClickablePictureBox_MouseEnter;
            MouseLeave += ClickablePictureBox_MouseLeave;
        }

        public bool ToggleSelected(bool? value = null)
        {
            if (value == null) Selected = !Selected;
            else Selected = (bool)value;
            if (Selected)
            {
                BorderStyle = BorderStyle.Fixed3D;
                Location = new Point(Location.X - 2, Location.Y - 2);
            }
            else
            {
                BorderStyle = BorderStyle.None;
                Location = new Point(Location.X + 2, Location.Y + 2);
            }
            return Selected;
        }

        private void ClickablePictureBox_MouseEnter(object? sender, EventArgs e)
        {
            if (Selected) return;
            BorderStyle = BorderStyle.Fixed3D;
            Location = new Point(Location.X - 2, Location.Y - 2);
        }

        private void ClickablePictureBox_MouseLeave(object? sender, EventArgs e)
        {
            if (Selected) return;
            BorderStyle = BorderStyle.None;
            Location = new Point(Location.X + 2, Location.Y + 2);
        }

        internal new void Dispose()
        {
            Image.Dispose();
            base.Dispose();
        }

    }
}
