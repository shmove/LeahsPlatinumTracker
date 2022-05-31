using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeahsPlatinumTracker
{
    public partial class MapsForm : Form
    {
        public Tracker? Player { get; set; }
        public TrackerForm? parent { get; set; }
        public MapImages MapImages { get; set; }
        public WarpButton? lastSelectedWarp { get; set; }

        public MapsForm()
        {
            Player = null;
            parent = null;
            MapImages = new MapImages();
            AutoScroll = false;
        }

        internal void Form_Load(object sender, EventArgs e)
        {
            foreach (Control control in Controls)
            {
                if (control.GetType() == typeof(WarpButton))
                {
                    (control as WarpButton).Init();
                }
                else if (control.GetType() == typeof(PictureBox))
                {
                    PictureBox pictureBox = (PictureBox)control;
                    MapImages.Add(pictureBox);
                    pictureBox.SendToBack();
                }
            }
            
            UpdateWarpAppearances();
        }

        public void Reload()
        {
            foreach(var item in this.Controls)
            {
                if (item.GetType() == typeof(WarpButton))
                {
                    WarpButton button = (WarpButton)item;
                    if (parent.warp1 != button.associatedWarp)
                    {
                        lastSelectedWarp = null;
                        button.selected = false;
                    }
                }
                else if (item.GetType() == typeof(MarkerPictureBox))
                {
                    MarkerPictureBox pictureBox = (MarkerPictureBox)item;
                    if (parent.warp1 != pictureBox.parent.associatedWarp)
                    {
                        lastSelectedWarp = null;
                        pictureBox.ToggleSelected(false);
                    }
                }
            }

            //UpdateWarpAppearances();
        }

        public new void Dispose()
        {
            MapImages.Dispose();
            foreach(Control control in Controls) if (control.GetType() == typeof(WarpButton)) (control as WarpButton).Dispose();
            base.Dispose();
        }

        // Warp Buttons
        public void UpdateWarpAppearances()
        {
            // copies the controls to an empty array, because for some god forsaken reason this collection wont sit still during loops.
            // this fixes the issue where some warpbuttons were just skipped on updating, and remained appearing as their design time alternatives
            Control[] controls = new Control[Controls.Count];
            Controls.CopyTo(controls, 0);

            foreach (Control control in controls)
            {
                if (control.GetType() == typeof(WarpButton))
                {
                    WarpButton button = (WarpButton)control;
                    button.MouseDown -= new MouseEventHandler(Warp_Click);
                    button.MouseDown += new MouseEventHandler(Warp_Click);
                    button.UpdateAppearance();
                }
                else if (control.GetType() == typeof(MarkerPictureBox))
                {
                    WarpButton button = ((MarkerPictureBox)control).parent;
                    button.MouseDown -= new MouseEventHandler(Warp_Click);
                    button.MouseDown += new MouseEventHandler(Warp_Click);
                    button.UpdateAppearance();
                }
                else if (control.GetType() == typeof(RouteConnectorButton))
                {
                    RouteConnectorButton button = (RouteConnectorButton)control;
                    button.MouseDown -= new MouseEventHandler(RouteConnector_Click);
                    button.MouseDown += new MouseEventHandler(RouteConnector_Click);
                    button.UpdateAppearance();
                }
            }
        }

        public void SelectWarp(string MapID, int WarpID)
        {
            foreach (Control control in Controls)
            {
                if (control.GetType() == typeof(WarpButton))
                {
                    WarpButton button = (WarpButton)control;
                    if (button.MapID == MapID && button.WarpID == WarpID)
                    {
                        button.selected = true;
                        lastSelectedWarp = button;
                        button.UpdateAppearance();
                    }
                }
                else if (control.GetType() == typeof(MarkerPictureBox))
                {
                    MarkerPictureBox pictureBox = (MarkerPictureBox)control;
                    WarpButton button = pictureBox.parent;
                    if (button.MapID == MapID && button.WarpID == WarpID)
                    {
                        pictureBox.ToggleSelected();
                        button.selected = true;
                        lastSelectedWarp = button;
                        button.UpdateAppearance();
                    }
                        
                }
            }
        }

        // Mouse Events
        internal void Warp_Click(object sender, MouseEventArgs me)
        {
            WarpButton warpButton;
            bool hasPictureBox = false;
            if (sender.GetType() == typeof(MarkerPictureBox))
            {
                hasPictureBox = true;
                MarkerPictureBox pictureBox = (MarkerPictureBox)sender;
                warpButton = pictureBox.parent;
            }
            else
            {
                warpButton = (WarpButton)sender;
            }

            if (me.Button == MouseButtons.Left)
            {
                if (lastSelectedWarp != null)
                {
                    lastSelectedWarp.selected = false;
                    if (hasPictureBox) ((MarkerPictureBox)sender).ToggleSelected(false);
                    lastSelectedWarp.UpdateAppearance();
                }

                if (!parent.SetLinkWarps(warpButton.associatedWarp))
                {
                    warpButton.selected = true;
                    if (hasPictureBox) ((MarkerPictureBox)sender).ToggleSelected(true);
                    warpButton.UpdateAppearance();
                    lastSelectedWarp = warpButton;
                };

            }
            else if (me.Button == MouseButtons.Right)
            {
                // Apply dead end marker
                if (warpButton.associatedWarp.VisualMarkers == 1) warpButton.associatedWarp.VisualMarkers = 0;
                else warpButton.associatedWarp.VisualMarkers = 1;
                warpButton.UpdateAppearance();
                parent.UpdateMapSelectorButtons(parent);
            }
            else if (me.Button == MouseButtons.Middle)
            {
                // Load associated map
                if (warpButton.associatedWarp.Destination.WarpID >= 0)
                {
                    parent.LoadMapPanel(warpButton.associatedWarp.DestinationMapSector.MapID, warpButton.associatedWarp.Destination.WarpID);
                }
            }
        }

        private void RouteConnector_Click(object sender, MouseEventArgs me)
        {
            if (me.Button == MouseButtons.Left || me.Button == MouseButtons.Middle)
            {
                RouteConnectorButton button = (RouteConnectorButton)sender;
                parent.LoadMapPanel(button.associatedVisualMapSector.VisualMapID);
            }
        }
    }

    public class MapImages
    {
        private List<PictureBox> images;

        public MapImages()
        {
            images = new List<PictureBox>();
        }

        public List<PictureBox> Add(PictureBox image)
        {
            images.Add(image);
            return images;
        }

        public void SendToBack()
        {
            foreach (PictureBox image in images)
            {
                image.SendToBack();
            }
        }

        public void Dispose()
        {
            foreach(PictureBox pictureBox in images)
            {
                pictureBox.Image.Dispose();
            }
        }
    }
}
