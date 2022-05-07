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
    public partial class r210 : MapsForm
    {

        public r210()
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

            CreateRouteConnectorButton(this, Player, "Celestic", new Point(5, 189)); // extended route past psyduck up to celestic
            CreateRouteConnectorButton(this, Player, "215", new Point(884, 537)); // route to the right
            CreateRouteConnectorButton(this, Player, "Solaceon", new Point(695, 774)); // way down to solaceon

            MapImages.SendToBack();
        }

    }
}
