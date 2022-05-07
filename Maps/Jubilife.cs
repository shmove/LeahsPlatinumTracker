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
    public partial class Jubilife : MapsForm
    {

        public Jubilife()
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

            CreateRouteConnectorButton(this, Player, "204", new Point(595, 5)); // 204 top route
            CreateRouteConnectorButton(this, Player, "203", new Point(810, 220)); // 203 right route
            CreateRouteConnectorButton(this, Player, "Sandgem", new Point(592, 674)); // sandgem bottom route

            MapImages.SendToBack();
        }

    }
}
