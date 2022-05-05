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
    public partial class GalacticHQ : MapsForm
    {

        public GalacticHQ()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            MapImages.Add(pictureBox1);
            MapSector f1A = Player.GetMapSector("GalacticHQ 1F A");
            MapSector f1B = Player.GetMapSector("GalacticHQ 1F B");
            MapSector f1C = Player.GetMapSector("GalacticHQ 1F C");

            MapSector f2A = Player.GetMapSector("GalacticHQ 2F A");
            MapSector f2B = Player.GetMapSector("GalacticHQ 2F B");

            MapSector f3 = Player.GetMapSector("GalacticHQ 3F");

            MapSector f4A = Player.GetMapSector("GalacticHQ 4F A");
            MapSector f4B = Player.GetMapSector("GalacticHQ 4F B");

            MapSector fB2A = Player.GetMapSector("GalacticHQ Warehouse B2F A");
            MapSector fB2B = Player.GetMapSector("GalacticHQ Warehouse B2F B");

            // create warp buttons
            CreateWarpButton(this, f1A.Warps[0], new Point(67, 179)); // left entrance 1f
            CreateWarpButton(this, f1A.Warps[1], new Point(404, 179)); // right entrance 1f

            CreateWarpButton(this, f1B.Warps[0], new Point(237, 70)); // stairs behind galactic key 1f

            CreateWarpButton(this, f1C.Warps[0], new Point(547, 14)); // left warp pad 1f
            CreateWarpButton(this, f1C.Warps[1], new Point(580, 40)); // right warp pad 1f
            CreateWarpButton(this, f1C.Warps[2], new Point(659, 14)); // down stairs 1f

            CreateWarpButton(this, f2A.Warps[0], new Point(277, 249)); // left stairs 2f
            CreateWarpButton(this, f2A.Warps[1], new Point(561, 273)); // middle warp pad 2f
            CreateWarpButton(this, f2A.Warps[2], new Point(816, 260)); // top right warp pad 2f
            CreateWarpButton(this, f2A.Warps[3], new Point(816, 383)); // bottom right warp pad 2f

            CreateWarpButton(this, f2B.Warps[0], new Point(200, 369)); // left warp pad 2f
            CreateWarpButton(this, f2B.Warps[1], new Point(277, 340)); // middle stairs 2f
            CreateWarpButton(this, f2B.Warps[2], new Point(396, 340)); // right stairs 2f

            CreateWarpButton(this, f3.Warps[0], new Point(770, 451)); // right stairs 3f
            CreateWarpButton(this, f3.Warps[1], new Point(430, 576)); // middle warp 3f
            CreateWarpButton(this, f3.Warps[2], new Point(463, 550)); // right warp 3f
            CreateWarpButton(this, f3.Warps[3], new Point(401, 603)); // left warp 3f

            CreateWarpButton(this, f4A.Warps[0], new Point(29, 609)); // cyrus room locked area

            CreateWarpButton(this, f4B.Warps[0], new Point(120, 455));  // cyrus top warp
            CreateWarpButton(this, f4B.Warps[1], new Point(343, 455)); // cyrus top right warp
            CreateWarpButton(this, f4B.Warps[2], new Point(327, 655)); // cyrus bottom right warp

            CreateWarpButton(this, fB2A.Warps[0], new Point(584, 634)); // basement left stairs
            CreateWarpButton(this, fB2A.Warps[1], new Point(812, 611)); // basement right stairs

            CreateWarpButton(this, fB2B.Warps[0], new Point(723, 564)); // basement top stairs

            MapImages.SendToBack();
        }

    }
}
