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
        public Warp associatedWarp { get; set; }
        private Point position;
        private MarkerPictureBox? Marker;
        private MapsForm? parent;
        public bool selected { get; set; } = false;

        private ToolTip? ButtonToolTip { get; set; }

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

            MouseHover += WarpButton_MouseHover;
        }

        public void Init(bool fromUpdate = false)
        {
            parent = (MapsForm)Parent;
            associatedWarp = parent.Player.GetMapSector(MapID).Warps[WarpID];
            //if (associatedWarp == null) throw new Exception("WarpButton was provided invalid Warp identifers.");
            position = Location;
            if (!fromUpdate) UpdateAppearance();
        }

        public new void Dispose()
        {
            if (Marker != null && parent != null) Marker.RemoveFrom(parent);
            base.Dispose();
        }

        private void UpdateButtonCosmetics()
        { 
            if (associatedWarp.Destination.MapID != "Not set")
            {
                Warp relevantWarp = associatedWarp;

                Location = position;
                Size = new(107, 23);
                Font = new Font("Nirmala UI", (float)8.25, FontStyle.Regular);

                // Check if pseudo corridor, then act as if the warp is linked directly if true
                if (associatedWarp.DestinationMapSector.IsPseudoCorridor)
                {
                    while (relevantWarp.DestinationMapSector.IsPseudoCorridor)
                    {
                        (Warp Warp1, Warp Warp2) PseudoWarps = relevantWarp.DestinationMapSector.PseudoCorridorWarps;
                        if (PseudoWarps.Warp1.Destination.MapID != relevantWarp.MapID) relevantWarp = PseudoWarps.Warp1;
                        else relevantWarp = PseudoWarps.Warp2;
                    }

                    Text = relevantWarp.DestinationVisualMapSector.DisplayName + "*";
                }
                else Text = associatedWarp.DestinationVisualMapSector.DisplayName;

                if (relevantWarp.Destination.MapID.Contains("Pokecentre") || relevantWarp.Destination.MapID == "PokeLeague Int")
                {
                    ForeColor = Color.FromArgb(255, 238, 238, 238);
                    BackColor = Color.FromArgb(255, 243, 109, 116);
                    FlatAppearance.BorderColor = Color.FromArgb(255, 239, 63, 71);
                }
                // check if one-way somehow?
                else
                {
                    ForeColor = Color.FromArgb(255, 54, 82, 129);
                    BackColor = Color.FromArgb(255, 160, 183, 214);
                    FlatAppearance.BorderColor = Color.FromArgb(255, 112, 146, 190);
                }
            }
            else
            {
                Location = new Point(position.X + 32, position.Y);
                Size = new(43, 23);
                Text = "?";
                Font = new Font("Power Clear", 9, FontStyle.Bold);
                if (associatedWarp.ParentMapSector.IsUnlocked)
                {
                    ForeColor = Color.FromArgb(255, 238, 238, 238);
                    BackColor = Color.FromArgb(255, 53, 53, 53);
                    FlatAppearance.BorderColor = Color.FromName("Black");
                }
                else
                {
                    // not unlocked visuals
                    ForeColor = Color.FromArgb(255, 155, 155, 155);
                    BackColor = Color.FromArgb(255, 209, 209, 209);
                    FlatAppearance.BorderColor = Color.FromArgb(255, 155, 155, 155);
                }

            }
            if (selected) FlatAppearance.BorderColor = Color.White;
        }

        public void UpdateAppearance()
        {
            if (parent == null) Init(true);
            if (Marker != null) Marker.RemoveFrom(parent);
            Marker = null;
            if (associatedWarp.VisualMarkers == 0)
            {
                this.Show();
                UpdateButtonCosmetics();
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
                    UpdateButtonCosmetics();
                    Marker.Location = new Point(position.X + 38 - ((selected ? 1 : 0) * 2), position.Y + 20 - ((selected ? 1 : 0) * 2));
                }

                switch (associatedWarp.VisualMarkers)
                {
                    case 1:
                        Marker.Image = Resources.MarkerResources.dead_end;
                        break;
                    case 2:
                        Marker.Image = Resources.MarkerResources.arrow;
                        break;
                    case 3:
                        Marker.Image = Resources.MarkerResources.bike;
                        break;
                    case 4:
                        Marker.Image = Resources.MarkerResources.trainer;
                        break;
                    case 5:
                        Marker.Image = Resources.MarkerResources.CoalBadge;
                        break;
                    case 6:
                        Marker.Image = Resources.MarkerResources.ForestBadge;
                        break;
                    case 7:
                        Marker.Image = Resources.MarkerResources.RelicBadge;
                        break;
                    case 8:
                        Marker.Image = Resources.MarkerResources.CobbleBadge;
                        break;
                    case 9:
                        Marker.Image = Resources.MarkerResources.FenBadge;
                        break;
                    case 10:
                        Marker.Image = Resources.MarkerResources.MineBadge;
                        break;
                    case 11:
                        Marker.Image = Resources.MarkerResources.IcicleBadge;
                        break;
                    case 12:
                        Marker.Image = Resources.MarkerResources.BeaconBadge;
                        break;
                    case 13:
                        Marker.Image = Resources.MarkerResources.Aaron;
                        break;
                    case 14:
                        Marker.Image = Resources.MarkerResources.Bertha;
                        break;
                    case 15:
                        Marker.Image = Resources.MarkerResources.Flint;
                        break;
                    case 16:
                        Marker.Image = Resources.MarkerResources.Lucian;
                        break;
                    case 17:
                        Marker.Image = Resources.MarkerResources.Cynthia;
                        break;
                    case 18:
                        Marker.Image = Resources.MarkerResources.RockSmash;
                        break;
                    case 19:
                        Marker.Image = Resources.MarkerResources.Cut;
                        break;
                    case 20:
                        Marker.Image = Resources.MarkerResources.Strength;
                        break;
                    case 21:
                        Marker.Image = Resources.MarkerResources.Surf;
                        break;
                    case 22:
                        Marker.Image = Resources.MarkerResources.Waterfall;
                        break;
                    case 23:
                        Marker.Image = Resources.MarkerResources.RockClimb;
                        break;
                    case 24:
                        Marker.Image = Resources.MarkerResources.MasterBall;
                        break;
                    case 25:
                        Marker.Image = Resources.MarkerResources.Pokeball;
                        break;
                    case 26:
                        Marker.Image = Resources.MarkerResources.Mart;
                        break;
                    case 27:
                        Marker.Image = Resources.MarkerResources.exclamation;
                        break;
                    case 28:
                        Marker.Image = Resources.MarkerResources.GalacticKey;
                        break;
                    case 29:
                        Marker.Image = Resources.MarkerResources.Defog;
                        break;
                    case 30:
                        Marker.Image = Resources.MarkerResources.Heal;
                        break;
                }

                Marker.MouseDown += new MouseEventHandler(parent.Warp_Click);
                Marker.MouseHover += WarpButton_MouseHover;
                parent.Controls.Add(Marker);
                parent.parent.UpdateMapSelectorButtons(parent.parent);
                Marker.BringToFront();
                parent.MapImages.SendToBack();
            }
        }

        private void WarpButton_MouseHover(object? sender, EventArgs e)
        {
            if (ButtonToolTip == null)
            {
                if (associatedWarp.ParentMapSector.IsUnlocked) return;
                else
                {
                    ButtonToolTip = new ToolTip
                    {
                        InitialDelay = 600,
                        AutoPopDelay = 32000 // why is this even a thing?
                    };
                    ButtonToolTip.SetToolTip(this, associatedWarp.ParentMapSector.RequirementsString);
                }
            }
            else
            {
                if (associatedWarp.ParentMapSector.IsUnlocked) { ButtonToolTip.RemoveAll(); ButtonToolTip = null; return; }
                ButtonToolTip.SetToolTip(this, associatedWarp.ParentMapSector.RequirementsString);
            }
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
