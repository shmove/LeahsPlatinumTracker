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

        internal Tracker Player;
        private MapsForm? activePanel;
        private List<System.Reflection.TypeInfo> mapPanels;

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
            foreach (Control control in this.Controls)
            {
                initialiseControls(control);
            }

            // Map Sectors / Sub-forms

            var classType = typeof(MapsForm);
            mapPanels = classType.Assembly.DefinedTypes
                .Where(x => classType.IsAssignableFrom(x) && x != classType)
                .ToList();

            LoadMapPanel("Sandgem");
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
            else if (control.GetType().Name.EndsWith("FlagsButton"))
            {
                ((ChecksButton)control).Initialise(control);
            }
            else if (control.GetType().Name == "MarkerSelector")
            {
                ((MarkerSelector)control).Initialise(this);
            }
            else if (control.GetType().Name == "Panel")
            {
                // recursion for panels
                foreach (Control subControl in control.Controls)
                {
                    initialiseControls(subControl);
                }
            }
        }

        public void updateMapSelectorButtons()
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

        public void updateAllAppearances()
        {
            updateMapSelectorButtons();
            activePanel.UpdateWarpAppearances();
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
                    if (Player.LinkWarps((warp1.MapID, warp1.WarpID), (warp2.MapID, warp2.WarpID)))
                    {
                        warp1 = null;
                        warp2 = null;
                        linking = false;
                        button62.FlatAppearance.BorderColor = Color.FromArgb(255, 112, 146, 190);
                        activePanel.UpdateWarpAppearances();
                        updateMapSelectorButtons();
                    };
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
            foreach(var panelClass in mapPanels)
            {
                if (panelClass.Name == MapName || MapName.StartsWith("2") && panelClass.Name == ("r" + MapName))
                {
                    MapsForm panel = (MapsForm)Activator.CreateInstance(panelClass);
                    panel.Player = Player;
                    panel.parent = this;
                    panel.TopLevel = false;

                    if (activePanel != null)
                    {
                        activePanel.Hide();
                        MainPanel.Controls.Clear();
                        activePanel.Dispose();
                    }

                    activePanel = panel;
                    MainPanel.Controls.Add(activePanel);
                    activePanel.Reload();
                    activePanel.UpdateWarpAppearances();
                    activePanel.Show();
                }
                // https://stackoverflow.com/a/32795682
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            updateMapSelectorButtons();
            activePanel.UpdateWarpAppearances();
        }

        public void ApplyMarker(int markerNum)
        {
            if (activePanel.lastSelectedWarp != null)
            {
                if (activePanel.lastSelectedWarp.associatedWarp.VisualMarkers == markerNum) activePanel.lastSelectedWarp.associatedWarp.VisualMarkers = 0;
                else activePanel.lastSelectedWarp.associatedWarp.VisualMarkers = markerNum;
                activePanel.lastSelectedWarp.updateAppearance();
            }
        }
    }
}
