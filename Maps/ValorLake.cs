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
    public partial class ValorLake : MapsForm
    {

        public ValorLake()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            MapImages.Add(pictureBox1);
            //MapSector Floaroma = Player.GetMapSector("Floaroma");
            //MapSector FloaromaPokecentre = Player.GetMapSector("Floaroma Pokecentre");

            // create warp buttons
            // CreateWarpButton(this, Floaroma.Warps[0], new Point(0, 0));

            CreateRouteConnectorButton(this, Player, "214", new Point(818, 5)); // top 214 route
            CreateRouteConnectorButton(this, Player, "222", new Point(884, 397)); // right 222 route
            CreateRouteConnectorButton(this, Player, "213", new Point(817, 654)); // bottom 213 surf route

            MapImages.SendToBack();
        }

    }
}
