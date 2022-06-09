using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeahsPlatinumTracker
{
    public class ChecksButton : PictureBox
    {
        private TrackerForm? ParentForm;
        internal Tracker? Player;
        private static ToolTip tooltip;
        internal bool CheckState = false;
        internal bool VisualCheckState = false;

        public Image? Image_Locked { get; set; }
        public Image? Image_VisualUnlocked { get; set; }
        public Image? Image_Unlocked { get; set; }
        public string? TooltipText { get; set; }

        public ChecksButton()
        {
            SizeMode = PictureBoxSizeMode.AutoSize;
            MouseHover += Initialise;
            tooltip = new ToolTip
            {
                InitialDelay = 400,
                AutoPopDelay = 32000 // why is this even a thing?
            };
        }

        private TrackerForm getMainParent(Control control)
        {
            if (!control.Parent.GetType().IsSubclassOf(typeof(TrackerForm)))
            {
                return getMainParent(control.Parent);
            }
            else return (TrackerForm)control.Parent;
        }

        public void Initialise(object sender, EventArgs? e = null)
        {
            ParentForm = getMainParent(this);
            Player = ParentForm.Player;

            switch (sender.GetType().Name)
            {
                case "CheckFlagsButton":
                    CheckState = Player.Checks.ChecksMade.HasFlag(((CheckFlagsButton)sender).Flag);
                    VisualCheckState = Player.VisualChecks.ChecksMade.HasFlag(((CheckFlagsButton)sender).Flag);
                    tooltip.SetToolTip(this, TooltipText ?? ((CheckFlagsButton)sender).Flag.ToString());
                    break;
                case "ProgressFlagsButton":
                    CheckState = Player.Checks.Progress.HasFlag(((ProgressFlagsButton)sender).Flag);
                    VisualCheckState = Player.VisualChecks.Progress.HasFlag(((ProgressFlagsButton)sender).Flag);
                    tooltip.SetToolTip(this, TooltipText ?? ((ProgressFlagsButton)sender).Flag.ToString());
                    break;
                case "HMFlagsButton":
                    CheckState = Player.Checks.HMs.HasFlag(((HMFlagsButton)sender).Flag);
                    VisualCheckState = Player.VisualChecks.HMs.HasFlag(((HMFlagsButton)sender).Flag);
                    tooltip.SetToolTip(this, TooltipText ?? ((HMFlagsButton)sender).Flag.ToString());
                    break;
            }

            if (CheckState) Image = Image_Unlocked;
            else if (VisualCheckState) Image = Image_VisualUnlocked;
            else Image = Image_Locked;

            MouseHover -= Initialise;
        }

        public bool HandleCheckChange(object sender)
        {
            if (ParentForm == null) Initialise(sender);

            bool toggleOutput = true;

            switch (sender.GetType().Name)
            {
                case "CheckFlagsButton":
                    toggleOutput = Player.Checks.Toggle(((CheckFlagsButton)sender).Flag);
                    if (toggleOutput) Player.VisualChecks.Unlock(((CheckFlagsButton)sender).Flag);
                    else              Player.VisualChecks.Lock(((CheckFlagsButton)sender).Flag);
                    break;
                case "ProgressFlagsButton":
                    toggleOutput = Player.Checks.Toggle(((ProgressFlagsButton)sender).Flag);
                    if (toggleOutput) Player.VisualChecks.Unlock(((ProgressFlagsButton)sender).Flag);
                    else              Player.VisualChecks.Lock(((ProgressFlagsButton)sender).Flag);
                    break;
                case "HMFlagsButton":
                    toggleOutput = Player.Checks.Toggle(((HMFlagsButton)sender).Flag);
                    if (toggleOutput) Player.VisualChecks.Unlock(((HMFlagsButton)sender).Flag);
                    else              Player.VisualChecks.Lock(((HMFlagsButton)sender).Flag);
                    break;
            }

            // toggleOutput will return true if check was UNLOCKED, and false is check was LOCKED;
            // update map accordingly
            if (toggleOutput) Player.UpdateMap();
            else Player.RevertMap();
            ParentForm.UpdateAllAppearances();

            return toggleOutput;
        }

        public bool HandleVisualCheckChange(object sender)
        {
            if (ParentForm == null) Initialise(sender);

            bool toggleOutput = true;

            switch(sender.GetType().Name)
            {
                case "CheckFlagsButton":
                    toggleOutput = Player.VisualChecks.Toggle(((CheckFlagsButton)sender).Flag);
                    break;
                case "ProgressFlagsButton":
                    toggleOutput = Player.VisualChecks.Toggle(((ProgressFlagsButton)sender).Flag);
                    break;
                case "HMFlagsButton":
                    toggleOutput = Player.VisualChecks.Toggle(((HMFlagsButton)sender).Flag);
                    break;
            }

            return toggleOutput;
        }

    }

    public class CheckFlagsButton : ChecksButton
    {
        public Checks.CheckFlags Flag { get; set; }

        public CheckFlagsButton() : base()
        {
            Click += CheckFlagsButton_Click;
        }

        private void CheckFlagsButton_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;

            // if left click, or right click on flag that is actually unlocked
            if (me.Button == MouseButtons.Left || (me.Button == MouseButtons.Right && CheckState) )
            {
                CheckState = HandleCheckChange(this);
                if (CheckState) Image = Image_Unlocked;
                else Image = Image_Locked;
            }
            else if (me.Button == MouseButtons.Right)
            {
                VisualCheckState = HandleVisualCheckChange(this);
                if (VisualCheckState) Image = Image_VisualUnlocked;
                else Image = Image_Locked;
            }
        }
    }

    public class ProgressFlagsButton : ChecksButton
    {
        public Checks.ProgressFlags Flag { get; set; }

        public ProgressFlagsButton() : base()
        {
            Click += ProgressFlagsButton_Click;
        }

        private void ProgressFlagsButton_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;

            // if left click, or right click on flag that is actually unlocked
            if (me.Button == MouseButtons.Left || (me.Button == MouseButtons.Right && CheckState))
            {
                CheckState = HandleCheckChange(this);
                if (CheckState) Image = Image_Unlocked;
                else Image = Image_Locked;
            }
            else if (me.Button == MouseButtons.Right)
            {
                VisualCheckState = HandleVisualCheckChange(this);
                if (VisualCheckState) Image = Image_VisualUnlocked;
                else Image = Image_Locked;
            }
        }
    }

    public class HMFlagsButton : ChecksButton
    {
        public Checks.HMFlags Flag { get; set; }

        public HMFlagsButton() : base()
        {
            Click += HMFlagsButton_Click;
        }

        private void HMFlagsButton_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;

            // if left click, or right click on flag that is actually unlocked
            if (me.Button == MouseButtons.Left || (me.Button == MouseButtons.Right && CheckState))
            {
                CheckState = HandleCheckChange(this);
                if (CheckState) Image = Image_Unlocked;
                else Image = Image_Locked;
            }
            else if (me.Button == MouseButtons.Right)
            {
                VisualCheckState = HandleVisualCheckChange(this);
                if (VisualCheckState) Image = Image_VisualUnlocked;
                else Image = Image_Locked;
            }
        }
    }
}
