namespace LeahsPlatinumTracker
{
    public partial class Form1 : Form
    {
        Tracker Player;
        public Form1()
        {
            InitializeComponent();
            Player = new Tracker();
            Player.log();
        }

        private void button46_Click(object sender, EventArgs e)
        {
            (string MapID, int WarpID) warp1 = (textBox1.Text, Int16.Parse(textBox2.Text));
            (string MapID, int WarpID) warp2 = (textBox4.Text, Int16.Parse(textBox3.Text));

            Player.LinkWarps(warp1, warp2);
        }

        private void button78_Click(object sender, EventArgs e)
        {
            (string MapID, int WarpID) warp1 = (textBox1.Text, Int16.Parse(textBox2.Text));

            Player.UnlinkWarp(warp1);
        }

        private void button47_Click(object sender, EventArgs e)
        {
            Player.log();
        }

        private void button49_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Unlocked WorksKey");
            Player.Checks.Unlock(Checks.CheckFlags.HasWorksKey);
            Player.UpdateMap();
        }

        private void button50_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Unlocked GalacticKey");
            Player.Checks.Unlock(Checks.CheckFlags.HasGalacticKey);
            Player.UpdateMap();
        }

        private void button51_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Unlocked Teleport");
            Player.Checks.Unlock(Checks.CheckFlags.HasTeleport);
            Player.UpdateMap();
        }

        private void button48_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Unlocked Bike");
            Player.Checks.Unlock(Checks.CheckFlags.HasBike);
            Player.UpdateMap();
        }

        private void button52_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Spoke to Roark");
            Player.Checks.Unlock(Checks.CheckFlags.HasSpokenRoark);
            Player.UpdateMap();
        }

        private void button53_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Spoke to Fantina");
            Player.Checks.Unlock(Checks.CheckFlags.HasSpokenFantina);
            Player.UpdateMap();
        }

        private void button54_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Spoke to Volkner");
            Player.Checks.Unlock(Checks.CheckFlags.HasSpokenVolkner);
            Player.UpdateMap();
        }

        private void button55_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Defeated Windworks");
            Player.Checks.Unlock(Checks.CheckFlags.HasDefeatedWindworks);
            Player.UpdateMap();
        }

        private void button56_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Obtained SecretPotion");
            Player.Checks.Unlock(Checks.CheckFlags.HasSecretPotion);
            Player.UpdateMap();
        }

        private void button57_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Unlocked Badge1");
            Player.Checks.Unlock(Checks.ProgressFlags.HasCoalBadge);
            Player.UpdateMap();
        }

        private void button58_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Unlocked Badge2");
            Player.Checks.Unlock(Checks.ProgressFlags.HasForestBadge);
            Player.UpdateMap();
        }

        private void button59_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Unlocked Badge3");
            Player.Checks.Unlock(Checks.ProgressFlags.HasRelicBadge);
            Player.UpdateMap();
        }

        private void button60_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Unlocked Badge4");
            Player.Checks.Unlock(Checks.ProgressFlags.HasCobbleBadge);
            Player.UpdateMap();
        }

        private void button61_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Unlocked Badge5");
            Player.Checks.Unlock(Checks.ProgressFlags.HasFenBadge);
            Player.UpdateMap();
        }

        private void button62_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Unlocked Badge6");
            Player.Checks.Unlock(Checks.ProgressFlags.HasMineBadge);
            Player.UpdateMap();
        }

        private void button63_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Unlocked Badge7");
            Player.Checks.Unlock(Checks.ProgressFlags.HasIcicleBadge);
            Player.UpdateMap();
        }

        private void button64_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Unlocked Badge8");
            Player.Checks.Unlock(Checks.ProgressFlags.HasBeaconBadge);
            Player.UpdateMap();
        }

        private void button65_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Defeated Aaron");
            Player.Checks.Unlock(Checks.ProgressFlags.HasAaron);
            Player.UpdateMap();
        }

        private void button66_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Defeated Bertha");
            Player.Checks.Unlock(Checks.ProgressFlags.HasBertha);
            Player.UpdateMap();
        }

        private void button67_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Defeated Flint");
            Player.Checks.Unlock(Checks.ProgressFlags.HasFlint);
            Player.UpdateMap();
        }

        private void button68_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Defeated Lucian");
            Player.Checks.Unlock(Checks.ProgressFlags.HasLucian);
            Player.UpdateMap();
        }

        private void button69_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Defeated Cynthia");
            Player.Checks.Unlock(Checks.ProgressFlags.HasCynthia);
            Player.UpdateMap();
        }

        private void button70_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Unlocked RockSmash");
            Player.Checks.Unlock(Checks.HMFlags.HM06);
            Player.UpdateMap();
        }

        private void button71_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Unlocked Cut");
            Player.Checks.Unlock(Checks.HMFlags.HM01);
            Player.UpdateMap();
        }

        private void button72_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Unlocked Defog");
            Player.Checks.Unlock(Checks.HMFlags.HM05);
            Player.UpdateMap();
        }

        private void button73_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Unlocked Fly");
            Player.Checks.Unlock(Checks.HMFlags.HM02);
            Player.UpdateMap();
        }

        private void button74_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Unlocked Surf");
            Player.Checks.Unlock(Checks.HMFlags.HM03);
            Player.UpdateMap();
        }

        private void button75_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Unlocked Strength");
            Player.Checks.Unlock(Checks.HMFlags.HM04);
            Player.UpdateMap();
        }

        private void button76_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Unlocked RockClimb");
            Player.Checks.Unlock(Checks.HMFlags.HM08);
            Player.UpdateMap();
        }

        private void button77_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Unlocked Waterfall");
            Player.Checks.Unlock(Checks.HMFlags.HM07);
            Player.UpdateMap();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            UITest form = new UITest
            {
                Player = Player
            };
            form.Show();
        }
    }
}