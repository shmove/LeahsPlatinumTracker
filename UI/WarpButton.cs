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
        public bool selected = false;

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

        public void Init()
        {
            parent = (MapsForm)Parent;
            associatedWarp = parent.Player.GetMapSector(MapID).Warps[WarpID];
            position = Location;
            updateAppearance();
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
            if (parent == null) Init();
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
                Marker = new MarkerPictureBox();
                Marker.Size = new Size(32, 32);
                if (associatedWarp.Destination.WarpID < 0)
                {
                    this.Hide();
                    Marker.Location = new Point(position.X + 38, position.Y - 4);
                }
                else
                {
                    Marker.Location = new Point(position.X + 38, position.Y + 20);
                }

                switch (associatedWarp.VisualMarkers)
                {
                    case 1:
                        Marker.Image = Properties.MarkerResources.dead_end;
                        break;
                    case 2:
                        Marker.Image = Properties.MarkerResources.arrow;
                        break;
                }

                Marker.parent = this;
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

        public void RemoveFrom(MapsForm form)
        {
            this.Hide();
            this.Dispose();
            form.Controls.Remove(this);
        }
    }
}
