using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeahsPlatinumTracker
{
    internal class RouteConnectorButton : Button
    {
        public string destinationVisualMapSectorID { get; set; }

        private MapsForm parent;
        private Tracker Player;
        public VisualMapSector associatedVisualMapSector { get; set; }

        public RouteConnectorButton()
        {
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 2;
            UseCompatibleTextRendering = true;
            Font = new Font("Nirmala UI", (float)8.25, (FontStyle)5);
            Size = new(107, 23);
            ForeColor = Color.White;
            BackColor = Color.FromArgb(255, 73, 180, 111);
            FlatAppearance.BorderColor = ForeColor;
        }

        public void Init()
        {
            parent = (MapsForm)Parent;
            Player = parent.Player;
            associatedVisualMapSector = Player.GetVisualMapSector(destinationVisualMapSectorID);
            Text = associatedVisualMapSector.DisplayName;
            updateAppearance();
        }

        public RouteConnectorButton(MapsForm form, Tracker tracker, string sectorName, Point location)
        {
            parent = form;
            Player = tracker;
            if (sectorName.StartsWith("r2")) sectorName = sectorName.Substring(1);
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
            if (parent == null) Init();
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
