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
    public partial class Celestic : MapsForm
    {

        public Celestic()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            MapImages.Add(pictureBox1);
            MapImages.Add(pictureBox2);
            MapSector Celestic = Player.GetMapSector("Celestic");
            MapSector CelesticPokecentre = Player.GetMapSector("Celestic Pokecentre");

            // create warp buttons
            CreateWarpButton(this, Celestic.Warps[0], new Point(12, 81)); // top left house
            CreateWarpButton(this, Celestic.Warps[1], new Point(212, 81)); // top middle house
            CreateWarpButton(this, Celestic.Warps[2], new Point(380, 81)); // top right house
            CreateWarpButton(this, Celestic.Warps[3], new Point(212, 183)); // cave
            CreateWarpButton(this, Celestic.Warps[4], new Point(75, 365)); // bottom left house
            CreateWarpButton(this, Celestic.Warps[5], new Point(357, 357)); // pc

            CreateWarpButton(this, CelesticPokecentre.Warps[0], new Point(pictureBox2.Location.X - 27, pictureBox2.Location.Y + 113)); // centre left stairs
            CreateWarpButton(this, CelesticPokecentre.Warps[1], new Point(pictureBox2.Location.X + 58, pictureBox2.Location.Y + 139)); // centre entrance
            CreateWarpButton(this, CelesticPokecentre.Warps[2], new Point(pictureBox2.Location.X + 145, pictureBox2.Location.Y + 113)); // centre right stairs

            CreateRouteConnectorButton(this, Player, "211", new Point(5, 261)); // left route connector
            CreateRouteConnectorButton(this, Player, "210", new Point(458, 286)); // right route connector

            MapImages.SendToBack();
        }

    }
}
