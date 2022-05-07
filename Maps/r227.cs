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
    public partial class r227 : MapsForm
    {

        public r227()
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

            CreateRouteConnectorButton(this, Player, "StarkMtn", new Point(428, 5)); // way up to stark mountain
            CreateRouteConnectorButton(this, Player, "226", new Point(221, 774)); // path down to 226

            MapImages.SendToBack();
        }

    }
}
