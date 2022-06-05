using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeahsPlatinumTracker
{
    public class TrackerForm : Form
    {

        public Tracker Player { get; set; }
        internal string Game { get { return Player.Game; } }
        internal MapsForm? activePanel;
        internal List<System.Reflection.TypeInfo> mapPanels;

        private string LoadedFile { get; set; } = string.Empty;

        internal UserNotes? NotesForm;

        internal Panel? MainPanel;
        internal Button? LinkButton;
        internal Button? UnlinkButton;
        internal Button? UndoButton;
        internal Button? RedoButton;
        internal Button? SaveButton;
        internal Button? NotesButton;

        internal List<((Warp warp1Prev, Warp warp1Post), (Warp warp2Prev, Warp warp2Post))> UndoHistory;
        internal List<((Warp warp1Prev, Warp warp1Post), (Warp warp2Prev, Warp warp2Post))> RedoHistory;

        public Warp? warp1 { get; set; } = null;
        internal Warp? warp2 = null;
        internal bool linking { get; set; } = false;

        private bool HasUpdated { get; set; } = false;

        // Constructors

        public TrackerForm()
        {
            Player = new Tracker();
            mapPanels = new List<System.Reflection.TypeInfo>();
            UndoHistory = new List<((Warp warp1Prev, Warp warp1Post), (Warp warp2Prev, Warp warp2Post))>();
            RedoHistory = new List<((Warp warp1Prev, Warp warp1Post), (Warp warp2Prev, Warp warp2Post))>();
        }

        public TrackerForm(Tracker _player, string _LoadedFile)
        {
            Player = _player;
            mapPanels = new List<System.Reflection.TypeInfo>();
            LoadedFile = _LoadedFile;
            UndoHistory = new List<((Warp warp1Prev, Warp warp1Post), (Warp warp2Prev, Warp warp2Post))>();
            RedoHistory = new List<((Warp warp1Prev, Warp warp1Post), (Warp warp2Prev, Warp warp2Post))>();
        }

        // Initialisation

        internal void TrackerForm_Load(object sender, EventArgs e)
        {

            // initialise controls
            foreach (Control control in this.Controls)
            {
                InitialiseControls(control);
            }

            UpdateLinkHistoryButtons();
            InitialiseMapPanels();
            UpdateMapSelectorButtons(this);

        }

        internal void InitialiseMapPanels()
        {
            var classType = typeof(MapsForm);
            mapPanels = classType.Assembly.DefinedTypes
                .Where(x => classType.IsAssignableFrom(x) && x != classType && (string)x.GetField("Game").GetValue(null) == Game)
                .ToList();

            switch (Game)
            {
                case "PokemonPlatinum":
                    LoadMapPanel("Sandgem");
                    break;
            }

            // https://stackoverflow.com/a/32795682
        }

        internal void InitialiseControls(Control control)
        {
            if (control.GetType().Name == "MapSelectorButton")
            {
                MapSelectorButton button = (MapSelectorButton)control;
                button.Player = Player;
                button.FormParent = this;
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
                    InitialiseControls(subControl);
                }
            }
        }

        // Visual Updates

        public void UpdateMapSelectorButtons(Control control)
        {
            foreach (Control subControl in control.Controls)
            {
                if (subControl.GetType().Name == "MapSelectorButton")
                {
                    MapSelectorButton button = (MapSelectorButton)subControl;
                    button.updateAppearance();
                }
                else if (subControl.GetType().Name == "Panel")
                {
                    // recursion for panels
                    UpdateMapSelectorButtons(subControl);
                }
            }
        }

        internal void UpdateLinkHistoryButtons()
        {
            if (UndoHistory.Count > 0)
            {
                UndoButton.ForeColor = Color.FromArgb(255, 54, 82, 129);
                UndoButton.BackColor = Color.FromArgb(255, 160, 183, 214);
                UndoButton.FlatAppearance.BorderColor = Color.FromArgb(255, 112, 146, 190);
            }
            else
            {
                UndoButton.ForeColor = Color.FromArgb(255, 155, 155, 155);
                UndoButton.BackColor = Color.FromArgb(255, 209, 209, 209);
                UndoButton.FlatAppearance.BorderColor = Color.FromArgb(255, 155, 155, 155);
            }

            if (RedoHistory.Count > 0)
            {
                RedoButton.ForeColor = Color.FromArgb(255, 54, 82, 129);
                RedoButton.BackColor = Color.FromArgb(255, 160, 183, 214);
                RedoButton.FlatAppearance.BorderColor = Color.FromArgb(255, 112, 146, 190);
            }
            else
            {
                RedoButton.ForeColor = Color.FromArgb(255, 155, 155, 155);
                RedoButton.BackColor = Color.FromArgb(255, 209, 209, 209);
                RedoButton.FlatAppearance.BorderColor = Color.FromArgb(255, 155, 155, 155);
            }
        }

        public void UpdateAllAppearances()
        {
            UpdateMapSelectorButtons(this);
            activePanel.UpdateWarpAppearances();
            UpdateLinkHistoryButtons();
        }

        // Main Buttons

        internal void LinkButton_Click(object sender, EventArgs e)
        {
            if (warp1 != null)
            {
                if (!linking)
                {
                    linking = true;
                    LinkButton.FlatAppearance.BorderColor = Color.White;
                }
                else
                {
                    linking = false;
                    LinkButton.FlatAppearance.BorderColor = Color.FromArgb(255, 112, 146, 190);
                }
            }
        }

        internal void UnlinkButton_Click(object sender, EventArgs e)
        {
            if (warp1 != null && warp1.HasDestination && !linking)
            {
                Warp warp1Prev = new Warp(warp1);
                Warp warp2Prev = new Warp(warp1.DestinationMapSector.GetWarp(warp1.Destination.WarpID));

                Player.UnlinkWarp((warp1.MapID, warp1.WarpID));

                UndoHistory.Add(((warp1Prev, new Warp(warp1)), (warp2Prev, new Warp(warp1Prev.DestinationMapSector.GetWarp(warp1Prev.Destination.WarpID)))));
                RedoHistory.Clear();

                warp1 = null;
                activePanel.lastSelectedWarp.selected = false;
                activePanel.lastSelectedWarp = null;
                UpdateAllAppearances();
            }
            else if (warp1 != null && warp1.VisualMarkers > 0 && !linking)
            {
                warp1.VisualMarkers = 0;
                UpdateAllAppearances();
            }
        }

        internal void UndoButton_Click(object sender, EventArgs e)
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

                        if (UndoInstance.Item1.warp1Prev.HasDestination)
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

                        if (UndoInstance.Item2.warp2Prev.HasDestination)
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

                RedoHistory.Add(((UndoInstance.Item1.warp1Post, UndoInstance.Item1.warp1Prev), (UndoInstance.Item2.warp2Post, UndoInstance.Item2.warp2Prev)));
                UndoHistory.RemoveAt(UndoHistory.Count - 1);
                UpdateAllAppearances();
            }
        }

        internal void RedoButton_Click(object sender, EventArgs e)
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

                        if (RedoInstance.Item1.warp1Prev.HasDestination)
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

                        if (RedoInstance.Item2.warp2Prev.HasDestination)
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
                UpdateAllAppearances();
            }
        }

        internal void MapSelectorButton_Click(object sender, EventArgs e)
        {
            MapSelectorButton button = (MapSelectorButton)sender;
            LoadMapPanel(button.Name);
            UpdateMapSelectorButtons(this);
        }

        // Close override
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (e.CloseReason == CloseReason.WindowsShutDown) return;
            if (!HasUpdated) return;

            switch (MessageBox.Show("Do you want to save?", "Leah's Platinum Tracker", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
            {
                case DialogResult.Yes:
                    Player.ToJSON(LoadedFile);
                    break;
                case DialogResult.Cancel:
                    e.Cancel = true;
                    break;
            }
        }

        // Methods

        public void LoadMapPanel(string MapID, int WarpID = -1)
        {
            if (MapID.StartsWith("r2")) MapID = MapID[1..];
            string VisualMapID = Player.GetVisualMapSector(MapID).VisualMapID;
            
            // if panel is already loaded
            if (activePanel != null)
            {
                if (activePanel.Name == VisualMapID || VisualMapID.StartsWith("2") && activePanel.Name == ("r" + VisualMapID))
                {
                    if (WarpID >= 0) activePanel.SelectWarp(MapID, WarpID);
                    return;
                }
            }
            bool loaded = false;

            foreach (var panelClass in mapPanels)
            {
                if (panelClass.Name == VisualMapID || VisualMapID.StartsWith("2") && panelClass.Name == ("r" + VisualMapID))
                {
                    MapsForm panel = (MapsForm)Activator.CreateInstance(panelClass);
                    panel.Player = Player;
                    panel.parent = this;
                    panel.TopLevel = false;

                    if (activePanel != null)
                    {
                        activePanel.Hide();
                        activePanel.Dispose();
                        MainPanel.Controls.Clear();
                    }

                    activePanel = panel;
                    MainPanel.Controls.Add(activePanel);
                    activePanel.Reload();
                    if (WarpID >= 0) activePanel.SelectWarp(MapID, WarpID);
                    //activePanel.UpdateWarpAppearances();
                    UpdateMapSelectorButtons(this); // updates MapSelectorButtons to display newly selected area
                    activePanel.Show();
                    loaded = true;
                }
            }

            if (!loaded) MessageBox.Show("Attempted to load an invalid panel.", "Error loading panel", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        internal bool SetLinkWarps(Warp warp)
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
                    HasUpdated = true;
                    warp2 = warp;

                    Warp warp1Prev = new Warp(warp1);
                    Warp warp2Prev = new Warp(warp2);

                    if (Player.LinkWarps((warp1.MapID, warp1.WarpID), (warp2.MapID, warp2.WarpID)))
                    {
                        // Add warps to undo history. (limit is 25)
                        UndoHistory.Add(((warp1Prev, new Warp(warp1)), (warp2Prev, new Warp(warp2))));
                        if (UndoHistory.Count > 25) UndoHistory.RemoveAt(0);
                        // reset redo history
                        RedoHistory.Clear();

                        warp1 = null;
                        warp2 = null;
                        linking = false;
                        LinkButton.FlatAppearance.BorderColor = Color.FromArgb(255, 112, 146, 190);
                        UpdateAllAppearances();
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

        public void ApplyMarker(int markerNum)
        {
            HasUpdated = true;
            if (activePanel.lastSelectedWarp != null)
            {
                if (activePanel.lastSelectedWarp.associatedWarp.VisualMarkers == markerNum) activePanel.lastSelectedWarp.associatedWarp.VisualMarkers = 0;
                else activePanel.lastSelectedWarp.associatedWarp.VisualMarkers = markerNum;
                activePanel.lastSelectedWarp.UpdateAppearance();
                UpdateMapSelectorButtons(this);
            }
        }

        internal void UpdateWindowTitle()
        {
            string[] txt = LoadedFile.Split("\\");
            Text = "Leah's Platinum Tracker — " + txt[txt.Length - 1]; // set window title with file name displayed
        }

        internal void SaveButton_Click(object sender, EventArgs e)
        {
            string filename = Player.ToJSON();

            if (filename != null)
            {
                LoadedFile = filename;
                HasUpdated = false; // stop save popup after manual saving
                UpdateWindowTitle();
            }
        }

        internal void NotesButton_Click(object sender, EventArgs e)
        {
            HasUpdated = true;
            if (NotesForm == null)
            {
                NotesForm = new(Player);
                NotesForm.FormClosed += (s, e) => { NotesForm = null; };
                FormClosed += (s, e) => { if (NotesForm != null) NotesForm.Close(); };
                NotesForm.Show();
            }

            else if (NotesForm.WindowState == FormWindowState.Minimized) NotesForm.WindowState = FormWindowState.Normal;

            else NotesForm.Focus();
        }

    }
}
