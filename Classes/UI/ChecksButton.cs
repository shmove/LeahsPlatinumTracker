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
                    CheckState = (Player.Checks.ChecksMade & ((CheckFlagsButton)sender).Flag) > 0;
                    VisualCheckState = (Player.VisualChecks.ChecksMade & ((CheckFlagsButton)sender).Flag) > 0;
                    tooltip.SetToolTip(this, TooltipText ?? ((CheckFlagsButton)sender).Flag.ToString());
                    break;
                case "ProgressFlagsButton":
                    CheckState = (Player.Checks.Progress & ((ProgressFlagsButton)sender).Flag) > 0;
                    VisualCheckState = (Player.VisualChecks.Progress & ((ProgressFlagsButton)sender).Flag) > 0;
                    tooltip.SetToolTip(this, TooltipText ?? ((ProgressFlagsButton)sender).Flag.ToString());
                    break;
                case "HMFlagsButton":
                    CheckState = (Player.Checks.HMs & ((HMFlagsButton)sender).Flag) > 0;
                    VisualCheckState = (Player.VisualChecks.HMs & ((HMFlagsButton)sender).Flag) > 0;
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
                    toggleOutput = ((IChecks)Player.Checks).ToggleCheck(((CheckFlagsButton)sender).Flag);
                    if (toggleOutput) ((IChecks)Player.VisualChecks).UnlockCheck(((CheckFlagsButton)sender).Flag);
                    else ((IChecks)Player.VisualChecks).LockCheck(((CheckFlagsButton)sender).Flag);
                    break;
                case "ProgressFlagsButton":
                    toggleOutput = ((IChecks)Player.Checks).ToggleProgress(((ProgressFlagsButton)sender).Flag);
                    if (toggleOutput) ((IChecks)Player.VisualChecks).UnlockProgress(((ProgressFlagsButton)sender).Flag);
                    else ((IChecks)Player.VisualChecks).LockProgress(((ProgressFlagsButton)sender).Flag);
                    break;
                case "HMFlagsButton":
                    toggleOutput = ((IChecks)Player.Checks).ToggleHM(((HMFlagsButton)sender).Flag);
                    if (toggleOutput) ((IChecks)Player.VisualChecks).UnlockHM(((HMFlagsButton)sender).Flag);
                    else ((IChecks)Player.VisualChecks).LockHM(((HMFlagsButton)sender).Flag);
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

            switch (sender.GetType().Name)
            {
                case "CheckFlagsButton":
                    toggleOutput = ((IChecks)Player.VisualChecks).ToggleCheck(((CheckFlagsButton)sender).Flag);
                    break;
                case "ProgressFlagsButton":
                    toggleOutput = ((IChecks)Player.VisualChecks).ToggleProgress(((ProgressFlagsButton)sender).Flag);
                    break;
                case "HMFlagsButton":
                    toggleOutput = ((IChecks)Player.VisualChecks).ToggleHM(((HMFlagsButton)sender).Flag);
                    break;
            }

            return toggleOutput;
        }

    }

    public class CheckFlagsButton : ChecksButton
    {
        public int Flag { get; set; }

        public CheckFlagsButton() : base()
        {
            Click += CheckFlagsButton_Click;
        }

        private void CheckFlagsButton_Click(object sender, EventArgs e)
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

    public class ProgressFlagsButton : ChecksButton
    {
        public int Flag { get; set; }

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
        public int Flag { get; set; }

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