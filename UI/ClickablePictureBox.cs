using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeahsPlatinumTracker
{
    internal class ClickablePictureBox : PictureBox
    {
        public ClickablePictureBox()
        {
            SizeMode = PictureBoxSizeMode.AutoSize;
            MouseEnter += ClickablePictureBox_MouseEnter;
            MouseLeave += ClickablePictureBox_MouseLeave;
        }

        private void ClickablePictureBox_MouseEnter(object? sender, EventArgs e)
        {
            BorderStyle = BorderStyle.Fixed3D;
            Location = new Point(Location.X - 2, Location.Y - 2);
        }

        private void ClickablePictureBox_MouseLeave(object? sender, EventArgs e)
        {
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
