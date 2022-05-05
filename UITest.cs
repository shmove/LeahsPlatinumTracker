using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LeahsPlatinumTracker
{
    public partial class UITest : Form
    {

        internal Tracker? Player;
        private MapsForm? activePanel;
        private List<MapsForm>? mapPanels;

        public Warp? warp1 { get; set; } = null;
        private Warp? warp2 = null;
        private bool linking = false;

        public UITest()
        {
            InitializeComponent();
        }

        private void UITest_Load(object sender, EventArgs e)
        {

            // Map Buttons
            foreach(Control control in this.Controls)
            {
                initialiseControls(control);
            }

            // Map Sectors / Sub-forms
            mapPanels = new List<MapsForm>();

            var classType = typeof(MapsForm);
            var subClassTypes = classType.Assembly.DefinedTypes
                .Where(x => classType.IsAssignableFrom(x) && x != classType)
                .ToList();

            foreach (var subClassType in subClassTypes)
            {
                MapsForm subClass = (MapsForm)Activator.CreateInstance(subClassType);
                subClass.Player = Player;
                subClass.parent = this;
                subClass.TopLevel = false;
                mapPanels.Add(subClass);
            }
            // https://stackoverflow.com/a/32795682 - Loops through all classes that derive from MapsForm and makes a map panel

            activePanel = mapPanels[0];
            MainPanel.Controls.Add(activePanel);
            activePanel.Show();
        }

        private void initialiseControls(Control control)
        {
            if (control.GetType().Name == "MapSelectorButton")
            {
                MapSelectorButton button = (MapSelectorButton)control;
                button.Player = Player;
                if (button.Name.StartsWith("r2")) button.associatedVisualMap = Player.GetVisualMapSector(button.Name.Substring(1));
                else button.associatedVisualMap = Player.GetVisualMapSector(button.Name);
                button.Click += new EventHandler(MapSelectorButton_Click);
                button.updateAppearance();
            }
            else if (control.GetType().Name == "Panel")
            {
                // recursion for panels
                foreach(Control subControl in control.Controls)
                {
                    initialiseControls(subControl);
                }
            }
        }

        internal void updateMapSelectorButtons()
        {
            foreach (Control control in this.Controls)
            {
                if (control.GetType().Name == "MapSelectorButton")
                {
                    MapSelectorButton button = (MapSelectorButton)control;
                    button.updateAppearance();
                }
            }
        }

        private void button62_Click(object sender, EventArgs e)
        {
            if (warp1 != null)
            {
                if (!linking)
                {
                    linking = true;
                    button62.FlatAppearance.BorderColor = Color.White;
                }
                else
                {
                    linking = false;
                    button62.FlatAppearance.BorderColor = Color.FromArgb(255, 112, 146, 190);
                }
            }
        }

        internal bool setLinkWarps(Warp warp)
        {
            if (warp1 == null)
            {
                warp1 = warp;
                return false;
            }
            else
            {
                if (linking && warp != warp1)
                {
                    warp2 = warp;
                    System.Diagnostics.Debug.WriteLine("linking " + warp1.MapID + warp1.WarpID + " to " + warp2.MapID + warp2.WarpID);
                    Player.LinkWarps((warp1.MapID, warp1.WarpID), (warp2.MapID, warp2.WarpID));
                    warp1 = null;
                    warp2 = null;
                    linking = false;
                    button62.FlatAppearance.BorderColor = Color.FromArgb(255, 112, 146, 190);
                    activePanel.UpdateWarpAppearances();
                    updateMapSelectorButtons();
                }
                else
                {
                    warp1 = warp;
                    return false;
                }
            }
            return true;
        }

        private void MapSelectorButton_Click(object sender, EventArgs e)
        {
            MapSelectorButton button = (MapSelectorButton)sender;
            LoadMapPanel(button.Name);
        }

        public void LoadMapPanel(string MapName)
        {
            foreach (MapsForm panel in mapPanels)
            {
                if (panel.Name == MapName)
                {
                    activePanel.Hide();
                    activePanel = panel;
                    MainPanel.Controls.Clear();
                    MainPanel.Controls.Add(panel);
                    panel.Reload();
                    panel.UpdateWarpAppearances();
                    activePanel.Show();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            updateMapSelectorButtons();
            activePanel.UpdateWarpAppearances();
        }

        private void ApplyMarker(int markerNum)
        {
            if (activePanel.lastSelectedWarp != null)
            {
                if (activePanel.lastSelectedWarp.associatedWarp.VisualMarkers == markerNum) activePanel.lastSelectedWarp.associatedWarp.VisualMarkers = 0;
                else activePanel.lastSelectedWarp.associatedWarp.VisualMarkers = markerNum;
                activePanel.lastSelectedWarp.updateAppearance();
            }
        }

        private void MarkerSelector1_Click(object sender, EventArgs e)
        {
            ApplyMarker(1);
        }

        private void MarkerSelector2_Click(object sender, EventArgs e)
        {
            ApplyMarker(2);
        }
    }
}
