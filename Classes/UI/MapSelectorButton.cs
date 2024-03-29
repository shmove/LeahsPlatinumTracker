﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeahsPlatinumTracker
{
    internal class MapSelectorButton : Button
    {
        internal Tracker? Player;
        internal TrackerForm? FormParent;
        internal VisualMapSector? associatedVisualMap;

        private Font? InitialFont;

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

        public void updateAppearance()
        {
            if (InitialFont == null) InitialFont = Font;
            Font = new Font(InitialFont.FontFamily, InitialFont.Size, InitialFont.Style);

            if (associatedVisualMap.IsUnlocked)
            {
                /*
                if (associatedVisualMap.IsCompleteDeadEnd)
                {
                    // completely useless area visuals
                    ForeColor = Color.FromArgb(255, 160, 61, 27);
                    BackColor = Color.FromArgb(255, 225, 181, 166);
                    FlatAppearance.BorderColor = Color.FromArgb(255, 160, 61, 27);
                }
                */
                if (associatedVisualMap.IsFullyCompleted)
                {
                    // fully completed visuals
                    ForeColor = Color.White;
                    BackColor = Color.FromArgb(255, 73, 180, 111);
                    FlatAppearance.BorderColor = Color.FromArgb(255, 23, 115, 55);
                }
                else if (associatedVisualMap.IsCompleted)
                {
                    // completed visuals
                    ForeColor = Color.FromArgb(255, 34, 153, 76);
                    BackColor = Color.FromArgb(255, 169, 222, 187);
                    FlatAppearance.BorderColor = Color.FromArgb(255, 34, 153, 76);
                }
                else if (associatedVisualMap.IsFullyChecked)
                {
                    // user assigned obstacle visuals
                    ForeColor = Color.FromArgb(255, 155, 132, 32);
                    BackColor = Color.FromArgb(255, 220, 224, 167);
                    FlatAppearance.BorderColor = Color.FromArgb(255, 155, 132, 32);
                }
                else
                {
                    // unlocked visuals
                    ForeColor = Color.FromArgb(255, 54, 82, 129);
                    BackColor = Color.FromArgb(255, 160, 183, 214);
                    FlatAppearance.BorderColor = Color.FromArgb(255, 112, 146, 190);
                }
            }
            else
            {
                // locked visuals
                ForeColor = Color.FromArgb(255, 155, 155, 155);
                BackColor = Color.FromArgb(255, 209, 209, 209);
                FlatAppearance.BorderColor = Color.FromArgb(255, 155, 155, 155);
            }

        }
    }
}
