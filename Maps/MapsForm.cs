using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeahsPlatinumTracker
{
    public partial class MapsForm : Form
    {
        public Tracker? Player = null;
        public UITest? parent { get; set; }
        public MapImages MapImages { get; set; }
        public WarpButton? lastSelectedWarp { get; set; }

        public MapsForm()
        {
            Player = null;
            parent = null;
            MapImages = new MapImages();
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
            }
        }

        // Warp Buttons
        internal void CreateWarpButton(MapsForm form, Warp warp, Point location)
        {
            WarpButton button = new WarpButton(form, warp, location);
            button.MouseDown += new MouseEventHandler(Warp_Click);
            form.Controls.Add(button);
        }

        public void UpdateWarpAppearances()
        {
            foreach (var item in this.Controls)
            {
                if (item.GetType().Name == "WarpButton")
                {
                    WarpButton button = (WarpButton)item;
                    button.updateAppearance();
                }
                else if (item.GetType().Name == "RouteConnectorButton")
                {
                    RouteConnectorButton button = (RouteConnectorButton)item;
                    button.updateAppearance();
                }
            }
        }

        // Route Connector Buttons
        public void CreateRouteConnectorButton(MapsForm form, Tracker Player, string sectorName, Point location)
        {
            RouteConnectorButton button = new RouteConnectorButton(form, Player, sectorName, location);
            button.MouseClick += new MouseEventHandler(RouteConnector_Click);
            form.Controls.Add(button);
        }

        // Mouse Events
        internal void Warp_Click(object sender, MouseEventArgs me)
        {
            WarpButton warpButton;
            if (sender.GetType().Name == "MarkerPictureBox")
            {
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
                    lastSelectedWarp.updateAppearance();
                }

                if (!parent.setLinkWarps(warpButton.associatedWarp))
                {
                    warpButton.selected = true;
                    warpButton.updateAppearance();
                    lastSelectedWarp = warpButton;
                };
                
            }
            else if (me.Button == MouseButtons.Right)
            {
                // Apply dead end marker
                if (warpButton.associatedWarp.VisualMarkers == 1) warpButton.associatedWarp.VisualMarkers = 0;
                else warpButton.associatedWarp.VisualMarkers = 1;
                UpdateWarpAppearances();
            }
        }

        /*
        private void Warp_DoubleClick()
        {
            if (lastClickedButton.associatedWarp.Destination.WarpID >= 0)
                parent.LoadMapPanel(Player.GetVisualMapSector(lastClickedButton.associatedWarp.Destination.MapID).VisualMapID);
        }
        */

        private void RouteConnector_Click(object sender, MouseEventArgs me)
        {
            RouteConnectorButton button = (RouteConnectorButton)sender;
            parent.LoadMapPanel(button.associatedVisualMapSector.VisualMapID);
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
    }
}
