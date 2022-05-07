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
    public partial class Hearthome : MapsForm
    {

        public Hearthome()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            MapImages.Add(pictureBox1);
            MapImages.Add(pictureBox2);
            MapSector Hearthome = Player.GetMapSector("Hearthome A");
            MapSector HearthomeB = Player.GetMapSector("Hearthome B");
            MapSector HearthomeGym = Player.GetMapSector("Hearthome C");
            MapSector HearthomePokecentre = Player.GetMapSector("Hearthome Pokecentre");

            // create warp buttons
            CreateWarpButton(this, Hearthome.Warps[0], new Point(94, 23)); // top left square entrance
            CreateWarpButton(this, Hearthome.Warps[1], new Point(766, 23)); // top right square entrance
            CreateWarpButton(this, Hearthome.Warps[2], new Point(421, 111)); // contest hall
            CreateWarpButton(this, Hearthome.Warps[3], new Point(198, 172)); // pc
            CreateWarpButton(this, Hearthome.Warps[4], new Point(285, 198)); // house next to pc
            CreateWarpButton(this, Hearthome.Warps[5], new Point(533, 185)); // big house top middle
            CreateWarpButton(this, Hearthome.Warps[6], new Point(118, 340)); // church
            CreateWarpButton(this, Hearthome.Warps[7], new Point(317, 331)); // house next to mart
            CreateWarpButton(this, Hearthome.Warps[8], new Point(397, 357)); // mart
            CreateWarpButton(this, Hearthome.Warps[9], new Point(574, 331)); // bottom right house 1
            CreateWarpButton(this, Hearthome.Warps[10], new Point(662, 357)); // bottom right house 2
            CreateWarpButton(this, Hearthome.Warps[11], new Point(13, 552)); // bottom left route connector 1
            CreateWarpButton(this, Hearthome.Warps[12], new Point(94, 590)); // bottom left route connector 2

            CreateWarpButton(this, HearthomeB.Warps[0], new Point(839, 552)); // bottom right route connector

            CreateWarpButton(this, HearthomeGym.Warps[0], new Point(741, 185)); // gym

            CreateWarpButton(this, HearthomePokecentre.Warps[0], new Point(pictureBox2.Location.X - 27, pictureBox2.Location.Y + 113)); // centre left stairs
            CreateWarpButton(this, HearthomePokecentre.Warps[1], new Point(pictureBox2.Location.X + 58, pictureBox2.Location.Y + 139)); // centre entrance
            CreateWarpButton(this, HearthomePokecentre.Warps[2], new Point(pictureBox2.Location.X + 145, pictureBox2.Location.Y + 113)); // centre right stairs

            MapImages.SendToBack();
        }

    }
}
