using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeahsPlatinumTracker
{
    public class MarkerSelector : ClickablePictureBox
    {
        public int MarkerID { get; set; }
        internal TrackerForm? ParentForm { get; set; }

        public MarkerSelector()
        {
            MarkerID = 1;
            SizeMode = PictureBoxSizeMode.AutoSize;
            MouseDown += MarkerSelector_MouseDown;
        }

        public void Initialise(TrackerForm form)
        {
            ParentForm = form;
        }

        private void MarkerSelector_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ParentForm.ApplyMarker(MarkerID);
            }
        }
    }
}
