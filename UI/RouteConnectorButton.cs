using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeahsPlatinumTracker
{
    internal class RouteConnectorButton : Button
    {
        public string DestinationMapSector { get; set; }
        public int ConditionIndex { get; set; }

        private MapsForm parent;
        private Tracker Player;
        public VisualMapSector associatedVisualMapSector { get; set; }
        private MapSector AssociatedMapSector { get; set; }

        private ToolTip? ButtonToolTip { get; set; }

        public RouteConnectorButton()
        {
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 2;
            UseCompatibleTextRendering = true;
            Font = new Font("Nirmala UI", (float)8.25, (FontStyle)5);
            Size = new(107, 23);
            ForeColor = Color.White;
            BackColor = Color.FromArgb(255, 73, 180, 111);
            FlatAppearance.BorderColor = ForeColor;

            MouseHover += RouteConnectorButton_MouseHover;
        }

        public void Init()
        {
            parent = (MapsForm)Parent;
            Player = parent.Player;
            AssociatedMapSector = Player.GetMapSector(DestinationMapSector);
            associatedVisualMapSector = AssociatedMapSector.ParentVisualMapSector;
            Text = associatedVisualMapSector.DisplayName;
            UpdateAppearance();
        }

        private bool MeetsUnlockedCriteria()
        {
            return (
                AssociatedMapSector.DefaultUnlocked && AssociatedMapSector.Conditions.Count == 0 ||
                AssociatedMapSector.IsUnlocked && AssociatedMapSector.Conditions[ConditionIndex].RequiredChecks.meetsRequirements(Player.Checks)
            );
        }

        public void UpdateAppearance()
        {
            if (parent == null) Init();
            if (MeetsUnlockedCriteria()){
                // regular visuals, unlocked
                ForeColor = Color.White;
                BackColor = Color.FromArgb(255, 73, 180, 111);
                FlatAppearance.BorderColor = ForeColor;
            }
            else
            {
                // special case locked visuals (check locked set warps)
                ForeColor = Color.FromArgb(255, 155, 155, 155);
                BackColor = Color.FromArgb(255, 209, 209, 209);
                FlatAppearance.BorderColor = Color.FromArgb(255, 155, 155, 155);
            }
            return;
        }

        private void RouteConnectorButton_MouseHover(object? sender, EventArgs e)
        {
            if (ButtonToolTip == null)
            {
                if (MeetsUnlockedCriteria()) return;
                else
                {
                    ButtonToolTip = new ToolTip
                    {
                        InitialDelay = 600,
                        AutoPopDelay = 32000 // why is this even a thing?
                    };
                    ButtonToolTip.SetToolTip(this, AssociatedMapSector.RequirementsString);
                }
            }
            else
            {
                if (AssociatedMapSector.IsUnlocked) { ButtonToolTip.RemoveAll(); ButtonToolTip = null; return; }
                ButtonToolTip.SetToolTip(this, AssociatedMapSector.RequirementsString);
            }
        }
    }

}
