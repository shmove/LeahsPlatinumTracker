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
    public partial class SurvivalArea : MapsForm
    {

        public SurvivalArea()
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

            CreateRouteConnectorButton(this, Player, "225", new Point(5, 233)); // leftmost route to 225
            CreateRouteConnectorButton(this, Player, "226", new Point(532, 269)); // rightmost route to 226

            MapImages.SendToBack();
        }

    }
}
