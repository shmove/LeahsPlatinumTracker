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
    public partial class FloaromaMeadow : MapsForm
    {

        public FloaromaMeadow()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            MapImages.Add(pictureBox1);
            MapSector FloaromaMeadowN = Player.GetMapSector("FloaromaMeadow N");
            MapSector FloaromaMeadowS = Player.GetMapSector("FloaromaMeadow S");

            // create warp buttons
            CreateWarpButton(this, FloaromaMeadowN.Warps[0], new Point(637, 47));

            CreateWarpButton(this, FloaromaMeadowS.Warps[0], new Point(461, 471));
            CreateWarpButton(this, FloaromaMeadowS.Warps[1], new Point(30, 595));

            MapImages.SendToBack();
        }

    }
}
