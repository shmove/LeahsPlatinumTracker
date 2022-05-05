using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeahsPlatinumTracker
{
    internal class RouteConnectorButton : Button
    {

        private MapsForm parent;
        private Tracker Player;
        public VisualMapSector associatedVisualMapSector { get; }

        public RouteConnectorButton(MapsForm form, Tracker tracker, string sectorName, Point location)
        {
            parent = form;
            Player = tracker;
            associatedVisualMapSector = Player.GetVisualMapSector(sectorName);

            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 2;
            UseCompatibleTextRendering = true;
            Text = associatedVisualMapSector.DisplayName;
            Font = new Font("Nirmala UI", (float)8.25, (FontStyle)5);
            Location = location;
            Size = new(107, 23);
            updateAppearance();
        }

        public void updateAppearance()
        {
            if (associatedVisualMapSector.IsUnlocked)
            {
                // regular visuals, unlocked
                ForeColor = Color.White;
                BackColor = Color.FromArgb(255, 73, 180, 111);
                FlatAppearance.BorderColor = ForeColor;
            }
            else
            {
                // special case locked visuals (check locked set warps)
                ForeColor = Color.FromArgb(255, 155, 155, 155);
                BackColor = Color.FromArgb(255, 209, 209, 209);
                FlatAppearance.BorderColor = Color.FromArgb(255, 155, 155, 155);
            }
        }
    }
}
