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
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 2;
            Size = new(107, 23);
            Text = "MapID";
            Font = new Font("Nirmala UI", (float)8.25, FontStyle.Regular);
            ForeColor = Color.FromArgb(255, 54, 82, 129);
            BackColor = Color.FromArgb(255, 160, 183, 214);
            FlatAppearance.BorderColor = Color.FromArgb(255, 112, 146, 190);
            UseCompatibleTextRendering = true;
        }

        public MapSelectorButton(Tracker tracker, VisualMapSector sector)
        {
            Player = tracker;
            associatedVisualMap = sector;
        }

        public void updateAppearance()
        {
            if (associatedVisualMap.IsUnlocked)
            {
                if (associatedVisualMap.IsFullyCompleted)
                {
                    // fully completed visuals
                    ForeColor = Color.White;
                    BackColor = Color.FromArgb(255, 73, 180, 111);
                    //Font = new Font(Font.FontFamily, Font.Size, FontStyle.Underline);
                    FlatAppearance.BorderColor = Color.FromArgb(255, 23, 115, 55);
                }
                else if (associatedVisualMap.IsCompleted)
                {
                    // completed visuals
                    ForeColor = Color.FromArgb(255, 34, 153, 76);
                    BackColor = Color.FromArgb(255, 169, 222, 187);
                    //Font = new Font(Font.FontFamily, Font.Size, FontStyle.Regular);
                    FlatAppearance.BorderColor = Color.FromArgb(255, 34, 153, 76);
                }
                else
                {
                    // unlocked visuals
                    ForeColor = Color.FromArgb(255, 54, 82, 129);
                    BackColor = Color.FromArgb(255, 160, 183, 214);
                    //Font = new Font(Font.FontFamily, Font.Size, FontStyle.Regular);
                    FlatAppearance.BorderColor = Color.FromArgb(255, 112, 146, 190);
                }
            }
            else
            {
                // locked visuals
                ForeColor = Color.FromArgb(255, 155, 155, 155);
                BackColor = Color.FromArgb(255, 209, 209, 209);
                //Font = new Font(Font.FontFamily, Font.Size, FontStyle.Regular);
                FlatAppearance.BorderColor = Color.FromArgb(255, 155, 155, 155);
            }
        }

    }
}
