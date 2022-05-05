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
    public partial class Sandgem : MapsForm
    {

        public Sandgem()
        {
            InitializeComponent();
        }

        private void Sandgem_Load(object sender, EventArgs e)
        {
            MapImages.Add(pictureBox1);
            MapImages.Add(pictureBox2);
            MapSector Sandgem = Player.GetMapSector("Sandgem");
            MapSector SandgemPokecentre = Player.GetMapSector("Sandgem Pokecentre");

            // create warp buttons
            CreateWarpButton(this, Sandgem.Warps[0], new Point(219, 98)); // centre
            CreateWarpButton(this, Sandgem.Warps[1], new Point(387, 98)); // mart
            CreateWarpButton(this, Sandgem.Warps[2], new Point(66, 248)); // left house
            CreateWarpButton(this, Sandgem.Warps[3], new Point(219, 248)); // right house

            CreateWarpButton(this, SandgemPokecentre.Warps[0], new Point(pictureBox2.Location.X - 27, pictureBox2.Location.Y + 113)); // centre left stairs
            CreateWarpButton(this, SandgemPokecentre.Warps[1], new Point(pictureBox2.Location.X + 58, pictureBox2.Location.Y + 139)); // centre entrance
            CreateWarpButton(this, SandgemPokecentre.Warps[2], new Point(pictureBox2.Location.X + 145, pictureBox2.Location.Y + 113)); // centre right stairs

            CreateRouteConnectorButton(this, Player, "Jubilife", new Point(368, 12)); // Route upwards to Jubilife
            CreateRouteConnectorButton(this, Player, "221", new Point(306, 374)); // Surf route downwards to 221

            MapImages.SendToBack();
        }

    }
}
