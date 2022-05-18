using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeahsPlatinumTracker
{
    public class ChecksButton : PictureBox
    {
        private UITest? ParentForm;
        private Tracker? Player;
        private static ToolTip tooltip;
        internal bool CheckState = false;

        public Image? Image_Locked { get; set; }
        public Image? Image_Unlocked { get; set; }

        public ChecksButton()
        {
            SizeMode = PictureBoxSizeMode.AutoSize;
            MouseHover += Initialise;
            tooltip = new ToolTip
            {
                InitialDelay = 300,
            };
        }

        private UITest getMainParent(Control control)
        {
            if (control.Parent.GetType().Name != "UITest")
            {
                return getMainParent(control.Parent);
            }
            else return (UITest)control.Parent;
        }

        public void Initialise(object sender, EventArgs? e = null)
        {
            ParentForm = getMainParent(this);
            Player = ParentForm.Player;

            switch (sender.GetType().Name)
            {
                case "CheckFlagsButton":
                    CheckState = Checks.FlagsTool.IsSet(Player.Checks.ChecksMade, ((CheckFlagsButton)sender).Flag);
                    tooltip.SetToolTip(this, ((CheckFlagsButton)sender).Flag.ToString());
                    break;
                case "ProgressFlagsButton":
                    CheckState = Checks.FlagsTool.IsSet(Player.Checks.Progress, ((ProgressFlagsButton)sender).Flag);
                    tooltip.SetToolTip(this, ((ProgressFlagsButton)sender).Flag.ToString());
                    break;
                case "HMFlagsButton":
                    CheckState = Checks.FlagsTool.IsSet(Player.Checks.HMs, ((HMFlagsButton)sender).Flag);
                    tooltip.SetToolTip(this, ((HMFlagsButton)sender).Flag.ToString());
                    break;
            }

            if (CheckState) Image = Image_Unlocked;
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
                    break;
                case "ProgressFlagsButton":
                    toggleOutput = Player.Checks.Toggle(((ProgressFlagsButton)sender).Flag);
                    break;
                case "HMFlagsButton":
                    toggleOutput = Player.Checks.Toggle(((HMFlagsButton)sender).Flag);
                    break;
            }

            // toggleOutput will return true if check was UNLOCKED, and false is check was LOCKED;
            // update map accordingly
            if (toggleOutput) Player.UpdateMap();
            else Player.RevertMap();
            ParentForm.updateAllAppearances();

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
            CheckState = HandleCheckChange(this);
            if (CheckState) Image = Image_Unlocked;
            else Image = Image_Locked;
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
            CheckState = HandleCheckChange(this);
            if (CheckState) Image = Image_Unlocked;
            else Image = Image_Locked;
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
            CheckState = HandleCheckChange(this);
            if (CheckState) Image = Image_Unlocked;
            else Image = Image_Locked;
        }
    }
}
