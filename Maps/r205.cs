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
    public partial class r205 : MapsForm
    {

        public r205()
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

            CreateRouteConnectorButton(this, Player, "Eterna", new Point(884, 166)); // upper right route to eterna
            CreateRouteConnectorButton(this, Player, "Floaroma", new Point(5, 694)); // lower left route to floaroma
            CreateRouteConnectorButton(this, Player, "ValleyWindworks", new Point(402, 710)); // lower right route to valley windworks

            MapImages.SendToBack();
        }

    }
}
