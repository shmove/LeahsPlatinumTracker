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
        public UITest? parent { get; set; }
        public MapImages MapImages { get; set; }
        public WarpButton? lastSelectedWarp { get; set; }

        public MapsForm()
        {
            Player = null;
            parent = null;
            MapImages = new MapImages();
        }

        internal void Form_Load(object sender, EventArgs e)
        {
            foreach (Control control in Controls)
            {
                if (control.GetType().Name == "PictureBox")
                {
                    PictureBox pictureBox = (PictureBox)control;
                    MapImages.Add(pictureBox);
                    pictureBox.SendToBack();
                }
            }
        }

        public void Reload()
        {
            foreach(var item in this.Controls)
            {
                if (item.GetType().Name == "WarpButton")
                {
                    WarpButton button = (WarpButton)item;
                    if (parent.warp1 != button.associatedWarp)
                    {
                        lastSelectedWarp = null;
                        button.selected = false;
                    }
                }
                else if (item.GetType().Name == "MarkerPictureBox")
                {
                    MarkerPictureBox pictureBox = (MarkerPictureBox)item;
                    if (parent.warp1 != pictureBox.parent.associatedWarp)
                    {
                        lastSelectedWarp = null;
                        pictureBox.ToggleSelected(false);
                    }
                }
            }
        }

        public new void Dispose()
        {
            MapImages.Dispose();
            base.Dispose();
        }

        // Warp Buttons
        public void UpdateWarpAppearances()
        {
            foreach (var item in this.Controls)
            {
                if (item.GetType().Name == "WarpButton")
                {
                    WarpButton button = (WarpButton)item;
                    button.MouseDown -= new MouseEventHandler(Warp_Click);
                    button.MouseDown += new MouseEventHandler(Warp_Click);
                    button.updateAppearance();
                }
                else if (item.GetType().Name == "MarkerPictureBox")
                {
                    WarpButton button = ((MarkerPictureBox)item).parent;
                    button.MouseDown -= new MouseEventHandler(Warp_Click);
                    button.MouseDown += new MouseEventHandler(Warp_Click);
                    button.updateAppearance();
                }
                else if (item.GetType().Name == "RouteConnectorButton")
                {
                    RouteConnectorButton button = (RouteConnectorButton)item;
                    button.MouseDown -= new MouseEventHandler(RouteConnector_Click);
                    button.MouseDown += new MouseEventHandler(RouteConnector_Click);
                    button.updateAppearance();
                }
            }
        }

        // Mouse Events
        internal void Warp_Click(object sender, MouseEventArgs me)
        {
            WarpButton warpButton;
            bool hasPictureBox = false;
            if (sender.GetType().Name == "MarkerPictureBox")
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
                    lastSelectedWarp.updateAppearance();
                }

                if (!parent.setLinkWarps(warpButton.associatedWarp))
                {
                    warpButton.selected = true;
                    if (hasPictureBox) ((MarkerPictureBox)sender).ToggleSelected(true);
                    warpButton.updateAppearance();
                    lastSelectedWarp = warpButton;
                };

            }
            else if (me.Button == MouseButtons.Right)
            {
                // Apply dead end marker
                if (warpButton.associatedWarp.VisualMarkers == 1) warpButton.associatedWarp.VisualMarkers = 0;
                else warpButton.associatedWarp.VisualMarkers = 1;
                warpButton.updateAppearance();
            }
            else if (me.Button == MouseButtons.Middle)
            {
                // Load associated map
                if (warpButton.associatedWarp.Destination.WarpID >= 0)
                {
                    parent.LoadMapPanel(warpButton.associatedWarp.DestinationVisualMapSector.VisualMapID);
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
