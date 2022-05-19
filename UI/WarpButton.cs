using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeahsPlatinumTracker
{
    public class WarpButton : Button
    {
        public string MapID { get; set; }
        public int WarpID { get; set; }
        public Warp associatedWarp;
        private Point position;
        private MarkerPictureBox? Marker;
        private MapsForm? parent;
        public bool selected { get; set; } = false;

        public WarpButton()
        {
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 2;
            UseCompatibleTextRendering = true;

            Size = new(107, 23);
            Font = new Font("Nirmala UI", (float)8.25, FontStyle.Regular);
            ForeColor = Color.FromArgb(255, 54, 82, 129);
            BackColor = Color.FromArgb(255, 160, 183, 214);
            FlatAppearance.BorderColor = Color.FromArgb(255, 112, 146, 190);
        }

        public void Init(bool fromUpdate = false)
        {
            parent = (MapsForm)Parent;
            associatedWarp = parent.Player.GetMapSector(MapID).Warps[WarpID];
            position = Location;
            if (!fromUpdate) updateAppearance();
        }

        public WarpButton(MapsForm form, Warp warp, Point position)
        {
            associatedWarp = warp;
            parent = form;
            this.position = position;

            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 2;
            this.UseCompatibleTextRendering = true;
            updateAppearance();
        }

        public void updateAppearance()
        {
            if (parent == null) Init(true);
            if (Marker != null) Marker.RemoveFrom(parent);
            Marker = null;
            if (associatedWarp.VisualMarkers == 0)
            {
                this.Show();
                if (associatedWarp.Destination.MapID != "Not set")
                {
                    this.Size = new(107, 23);
                    this.Location = position;
                    this.Text = associatedWarp.DestinationVisualMapSector.DisplayName;
                    this.Font = new Font("Nirmala UI", (float)8.25, FontStyle.Regular);

                    if (associatedWarp.Destination.MapID.Contains("Pokecentre") == true || associatedWarp.Destination.MapID == "PokeLeague Int")
                    {
                        this.ForeColor = Color.FromArgb(255, 238, 238, 238);
                        this.BackColor = Color.FromArgb(255, 243, 109, 116);
                        this.FlatAppearance.BorderColor = Color.FromArgb(255, 239, 63, 71);
                    }
                    // check if one-way somehow?
                    else
                    {
                        this.ForeColor = Color.FromArgb(255, 54, 82, 129);
                        this.BackColor = Color.FromArgb(255, 160, 183, 214);
                        this.FlatAppearance.BorderColor = Color.FromArgb(255, 112, 146, 190);
                    }
                }
                else
                {
                    this.Size = new(43, 23);
                    this.Location = new Point(position.X + 32, position.Y);
                    this.Text = "?";
                    this.Font = new Font("Power Clear", 9, FontStyle.Bold);
                    if (associatedWarp.ParentMapSector.IsUnlocked)
                    {
                        this.ForeColor = Color.FromArgb(255, 238, 238, 238);
                        this.BackColor = Color.FromArgb(255, 53, 53, 53);   
                        this.FlatAppearance.BorderColor = Color.FromName("Black");
                    }
                    else
                    {
                        // not unlocked visuals
                        this.ForeColor = Color.FromArgb(255, 155, 155, 155);
                        this.BackColor = Color.FromArgb(255, 209, 209, 209);
                        this.FlatAppearance.BorderColor = Color.FromArgb(255, 155, 155, 155);
                    }
                    
                }
            }
            else
            {
                Marker = new MarkerPictureBox(this);
                Marker.Size = new Size(32, 32);
                if (associatedWarp.Destination.WarpID < 0)
                {
                    this.Hide();
                    Marker.Location = new Point(position.X + 38 - ((selected ? 1 : 0) * 2), position.Y - 4 - ((selected ? 1 : 0) * 2));
                }
                else
                {
                    Text = associatedWarp.DestinationVisualMapSector.DisplayName;
                    Font = new Font("Nirmala UI", (float)8.25, FontStyle.Regular);
                    Marker.Location = new Point(position.X + 38 - ((selected ? 1 : 0) * 2), position.Y + 20 - ((selected ? 1 : 0) * 2));
                }

                switch (associatedWarp.VisualMarkers)
                {
                    case 1:
                        Marker.Image = Properties.MarkerResources.dead_end;
                        break;
                    case 2:
                        Marker.Image = Properties.MarkerResources.arrow;
                        break;
                    case 3:
                        Marker.Image = Properties.MarkerResources.bike;
                        break;
                    case 4:
                        Marker.Image = Properties.MarkerResources.trainer;
                        break;
                    case 5:
                        Marker.Image = Properties.MarkerResources.CoalBadge;
                        break;
                    case 6:
                        Marker.Image = Properties.MarkerResources.ForestBadge;
                        break;
                    case 7:
                        Marker.Image = Properties.MarkerResources.RelicBadge;
                        break;
                    case 8:
                        Marker.Image = Properties.MarkerResources.CobbleBadge;
                        break;
                    case 9:
                        Marker.Image = Properties.MarkerResources.FenBadge;
                        break;
                    case 10:
                        Marker.Image = Properties.MarkerResources.MineBadge;
                        break;
                    case 11:
                        Marker.Image = Properties.MarkerResources.IcicleBadge;
                        break;
                    case 12:
                        Marker.Image = Properties.MarkerResources.BeaconBadge;
                        break;
                    case 13:
                        Marker.Image = Properties.MarkerResources.Aaron;
                        break;
                    case 14:
                        Marker.Image = Properties.MarkerResources.Bertha;
                        break;
                    case 15:
                        Marker.Image = Properties.MarkerResources.Flint;
                        break;
                    case 16:
                        Marker.Image = Properties.MarkerResources.Lucian;
                        break;
                    case 17:
                        Marker.Image = Properties.MarkerResources.Cynthia;
                        break;
                }

                Marker.MouseDown += new MouseEventHandler(parent.Warp_Click);
                parent.Controls.Add(Marker);
                parent.parent.updateMapSelectorButtons();
                Marker.BringToFront();
                parent.MapImages.SendToBack();
            }
            if (selected) this.FlatAppearance.BorderColor = Color.White;
        }
    }

    internal class MarkerPictureBox : ClickablePictureBox
    {
        public WarpButton parent;

        public MarkerPictureBox(WarpButton _parent) : base()
        {
            parent = _parent;
            ToggleSelected(parent.selected);
        }


        public void RemoveFrom(MapsForm form)
        {
            Hide();
            form.Controls.Remove(this);
            Dispose();
        }

    }
}
