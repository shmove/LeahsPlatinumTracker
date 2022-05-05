using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LeahsPlatinumTracker.Maps
{
    public partial class BacklotMansion : MapsForm
    {

        public BacklotMansion()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            MapImages.Add(pictureBox1);
            MapSector BacklotMansion = Player.GetMapSector("BacklotMansion");

            // create warp buttons
            CreateWarpButton(this, BacklotMansion.Warps[0], new Point(22, 40));
            CreateWarpButton(this, BacklotMansion.Warps[1], new Point(150, 40));
            CreateWarpButton(this, BacklotMansion.Warps[2], new Point(279, 40));
            CreateWarpButton(this, BacklotMansion.Warps[3], new Point(470, 2));
            CreateWarpButton(this, BacklotMansion.Warps[4], new Point(710, 40));
            CreateWarpButton(this, BacklotMansion.Warps[5], new Point(470, 182));

            MapImages.SendToBack();
        }

    }
}
