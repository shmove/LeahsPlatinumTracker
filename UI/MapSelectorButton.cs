using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeahsPlatinumTracker
{
    internal class MapSelectorButton : Button
    {
        internal Tracker? Player;
        internal VisualMapSector? associatedVisualMap;

        public MapSelectorButton() 
        {
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 2;
            this.Size = new(107, 23);
            this.Text = "MapID";
            this.Font = new Font("Nirmala UI", (float)8.25, FontStyle.Regular);
            this.ForeColor = Color.FromArgb(255, 54, 82, 129);
            this.BackColor = Color.FromArgb(255, 160, 183, 214);
            this.FlatAppearance.BorderColor = Color.FromArgb(255, 112, 146, 190);
            this.UseCompatibleTextRendering = true;
        }

        public MapSelectorButton(Tracker tracker, VisualMapSector sector)
        {
            Player = tracker;
            associatedVisualMap = sector;
        }

        private void MapSelectorButton_Load(object sender, EventArgs e)
        {
           //
        }

        public void updateAppearance()
        {
            if (associatedVisualMap.IsUnlocked)
            {
                if (associatedVisualMap.IsCompleted)
                {
                    // completed visuals
                    this.ForeColor = Color.FromArgb(255, 73, 180, 111);
                    this.BackColor = Color.FromArgb(255, 193, 230, 206);
                    this.FlatAppearance.BorderColor = Color.FromArgb(255, 73, 180, 111);
                }
                else
                {
                    // unlocked visuals
                    this.ForeColor = Color.FromArgb(255, 54, 82, 129);
                    this.BackColor = Color.FromArgb(255, 160, 183, 214);
                    this.FlatAppearance.BorderColor = Color.FromArgb(255, 112, 146, 190);
                }
            }
            else
            {
                // locked visuals
                this.ForeColor = Color.FromArgb(255, 155, 155, 155);
                this.BackColor = Color.FromArgb(255, 209, 209, 209);
                this.FlatAppearance.BorderColor = Color.FromArgb(255, 155, 155, 155);
            }
        }

    }
}
