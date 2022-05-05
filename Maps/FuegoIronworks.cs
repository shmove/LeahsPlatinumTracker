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
    public partial class FuegoIronworks : MapsForm
    {

        public FuegoIronworks()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            MapImages.Add(pictureBox1);
            MapSector FuegoIronworksN = Player.GetMapSector("FuegoIronworks N");
            MapSector FuegoIronworksS = Player.GetMapSector("FuegoIronworks S");

            // create warp buttons
            CreateWarpButton(this, FuegoIronworksN.Warps[0], new Point(99, 183));
            CreateWarpButton(this, FuegoIronworksS.Warps[0], new Point(283, 428));

            CreateRouteConnectorButton(this, Player, "205", new Point(400, 171));

            MapImages.SendToBack();
        }

    }
}
