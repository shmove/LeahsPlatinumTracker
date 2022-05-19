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

        private List<((Warp warp1Prev, Warp warp1Post), (Warp warp2Prev, Warp warp2Post))> UndoHistory;
        private List<((Warp warp1Prev, Warp warp1Post), (Warp warp2Prev, Warp warp2Post))> RedoHistory;

        public Warp? warp1 { get; set; } = null;
        private Warp? warp2 = null;
        private bool linking { get; set; } = false;

        public UITest()
        {
            InitializeComponent();
            UndoHistory = new List<((Warp warp1Prev, Warp warp1Post), (Warp warp2Prev, Warp warp2Post))>();
            RedoHistory = new List<((Warp warp1Prev, Warp warp1Post), (Warp warp2Prev, Warp warp2Post))>();
        }

        private void UITest_Load(object sender, EventArgs e)
        {

            // Map Buttons
            foreach (Control control in this.Controls)
            {
                initialiseControls(control);
            }

            UpdateLinkHistoryButtons();

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
            UpdateLinkHistoryButtons();
        }

        // Link
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

        // Unlink
        private void button4_Click(object sender, EventArgs e)
        {
            if (warp1 != null && warp1.Destination.WarpID >= 0 && !linking)
            {
                Warp warp1Prev = new Warp(warp1);
                Warp warp2Prev = new Warp(warp1.DestinationMapSector.GetWarp(warp1.Destination.WarpID));

                Player.UnlinkWarp((warp1.MapID, warp1.WarpID));

                UndoHistory.Add( ((warp1Prev, new Warp(warp1)), (warp2Prev, new Warp(warp1Prev.DestinationMapSector.GetWarp(warp1Prev.Destination.WarpID)))) );
                RedoHistory.Clear();

                warp1 = null;
                activePanel.lastSelectedWarp.selected = false;
                activePanel.lastSelectedWarp = null;
                updateAllAppearances();
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

                    Warp warp1Prev = new Warp(warp1);
                    Warp warp2Prev = new Warp(warp2);

                    if (Player.LinkWarps((warp1.MapID, warp1.WarpID), (warp2.MapID, warp2.WarpID)))
                    {
                        // Add warps to undo history. (limit is 25)
                        UndoHistory.Add(( (warp1Prev, new Warp(warp1)), (warp2Prev, new Warp(warp2)) ));
                        if (UndoHistory.Count > 25) UndoHistory.RemoveAt(0);
                        // reset redo history
                        RedoHistory.Clear();

                        warp1 = null;
                        warp2 = null;
                        linking = false;
                        button62.FlatAppearance.BorderColor = Color.FromArgb(255, 112, 146, 190);
                        updateAllAppearances();
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
            updateAllAppearances();
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

        // Undo
        private void button2_Click(object sender, EventArgs e)
        {
            if (UndoHistory.Count > 0)
            {
                var UndoInstance = UndoHistory[^1]; // retrieves the last element from the list

                bool warp1undone = false;
                bool warp2undone = false;

                foreach (MapSector mapSector in Player.MapSectors)
                {

                    if (mapSector.MapID == UndoInstance.Item1.warp1Post.MapID)
                    {
                        Warp warp1 = mapSector.GetWarp(UndoInstance.Item1.warp1Post.WarpID);

                        if (UndoInstance.Item1.warp1Prev.Destination.WarpID >= 0)
                        {
                            Player.LinkWarps((UndoInstance.Item1.warp1Prev.MapID, UndoInstance.Item1.warp1Prev.WarpID), (UndoInstance.Item1.warp1Prev.Destination.MapID, UndoInstance.Item1.warp1Prev.Destination.WarpID), true);
                        }
                        else
                        {
                            Player.UnlinkWarp((warp1.MapID, warp1.WarpID), true);
                        }

                        warp1undone = true;
                    }

                    if (mapSector.MapID == UndoInstance.Item2.warp2Post.MapID)
                    {
                        Warp warp2 = mapSector.GetWarp(UndoInstance.Item2.warp2Post.WarpID);

                        if (UndoInstance.Item2.warp2Prev.Destination.WarpID >= 0)
                        {
                            Player.LinkWarps((UndoInstance.Item2.warp2Prev.MapID, UndoInstance.Item2.warp2Prev.WarpID), (UndoInstance.Item2.warp2Prev.Destination.MapID, UndoInstance.Item2.warp2Prev.Destination.WarpID), true);
                        }
                        else
                        {
                            Player.UnlinkWarp((warp2.MapID, warp2.WarpID), true);
                        }

                        warp2undone = true;
                    }

                    if (warp1undone && warp2undone) break;
                }

                RedoHistory.Add(( (UndoInstance.Item1.warp1Post, UndoInstance.Item1.warp1Prev), (UndoInstance.Item2.warp2Post, UndoInstance.Item2.warp2Prev) ));
                UndoHistory.RemoveAt(UndoHistory.Count - 1);
                updateAllAppearances();
            }
        }

        // Redo
        private void button3_Click(object sender, EventArgs e)
        {
            if (RedoHistory.Count > 0)
            {
                var RedoInstance = RedoHistory[^1]; // retrieves the last element from the list

                bool warp1redone = false;
                bool warp2redone = false;

                foreach (MapSector mapSector in Player.MapSectors)
                {

                    if (mapSector.MapID == RedoInstance.Item1.warp1Post.MapID)
                    {
                        Warp warp1 = mapSector.GetWarp(RedoInstance.Item1.warp1Post.WarpID);

                        if (RedoInstance.Item1.warp1Prev.Destination.WarpID >= 0)
                        {
                            Player.LinkWarps((RedoInstance.Item1.warp1Prev.MapID, RedoInstance.Item1.warp1Prev.WarpID), (RedoInstance.Item1.warp1Prev.Destination.MapID, RedoInstance.Item1.warp1Prev.Destination.WarpID), true);
                        }
                        else
                        {
                            Player.UnlinkWarp((warp1.MapID, warp1.WarpID), true);
                        }

                        warp1redone = true;
                    }

                    if (mapSector.MapID == RedoInstance.Item2.warp2Post.MapID)
                    {
                        Warp warp2 = mapSector.GetWarp(RedoInstance.Item2.warp2Post.WarpID);

                        if (RedoInstance.Item2.warp2Prev.Destination.WarpID >= 0)
                        {
                            Player.LinkWarps((RedoInstance.Item2.warp2Prev.MapID, RedoInstance.Item2.warp2Prev.WarpID), (RedoInstance.Item2.warp2Prev.Destination.MapID, RedoInstance.Item2.warp2Prev.Destination.WarpID), true);
                        }
                        else
                        {
                            Player.UnlinkWarp((warp2.MapID, warp2.WarpID), true);
                        }

                        warp2redone = true;
                    }

                    if (warp1redone && warp2redone) break;
                }

                UndoHistory.Add(((RedoInstance.Item1.warp1Post, RedoInstance.Item1.warp1Prev), (RedoInstance.Item2.warp2Post, RedoInstance.Item2.warp2Prev)));
                RedoHistory.RemoveAt(RedoHistory.Count - 1);
                updateAllAppearances();
            }
        }

        private void UpdateLinkHistoryButtons()
        {
            if (UndoHistory.Count > 0)
            {
                button2.ForeColor = Color.FromArgb(255, 54, 82, 129);
                button2.BackColor = Color.FromArgb(255, 160, 183, 214);
                button2.FlatAppearance.BorderColor = Color.FromArgb(255, 112, 146, 190);
            }
            else
            {
                button2.ForeColor = Color.FromArgb(255, 155, 155, 155);
                button2.BackColor = Color.FromArgb(255, 209, 209, 209);
                button2.FlatAppearance.BorderColor = Color.FromArgb(255, 155, 155, 155);
            }

            if (RedoHistory.Count > 0)
            {
                button3.ForeColor = Color.FromArgb(255, 54, 82, 129);
                button3.BackColor = Color.FromArgb(255, 160, 183, 214);
                button3.FlatAppearance.BorderColor = Color.FromArgb(255, 112, 146, 190);
            }
            else
            {
                button3.ForeColor = Color.FromArgb(255, 155, 155, 155);
                button3.BackColor = Color.FromArgb(255, 209, 209, 209);
                button3.FlatAppearance.BorderColor = Color.FromArgb(255, 155, 155, 155);
            }
        }

    }
}
