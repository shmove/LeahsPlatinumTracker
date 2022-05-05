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
    public partial class Canalave : MapsForm
    {

        public Canalave()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            MapImages.Add(pictureBox1);
            MapImages.Add(pictureBox2);
            MapImages.Add(pictureBox3);
            MapSector Canalave = Player.GetMapSector("Canalave");
            MapSector CanalavePokecentre = Player.GetMapSector("Canalave Pokecentre");

            // create warp buttons
            CreateWarpButton(this, Canalave.Warps[0], new Point(21, 88)); // hotel?
            CreateWarpButton(this, Canalave.Warps[1], new Point(286, 41)); // darkrai house
            CreateWarpButton(this, Canalave.Warps[2], new Point(281, 121)); // centre
            CreateWarpButton(this, Canalave.Warps[3], new Point(45, 206)); // gym
            CreateWarpButton(this, Canalave.Warps[4], new Point(225, 221)); // top right house
            CreateWarpButton(this, Canalave.Warps[5], new Point(38, 296)); // top left house
            CreateWarpButton(this, Canalave.Warps[6], new Point(225, 296)); // mart
            CreateWarpButton(this, Canalave.Warps[7], new Point(50, 371)); // bottom left house
            CreateWarpButton(this, Canalave.Warps[8], new Point(237, 371)); // bottom right house
            CreateWarpButton(this, Canalave.Warps[9], new Point(440, 452)); // bottom right exit

            CreateWarpButton(this, CanalavePokecentre.Warps[0], new Point(pictureBox2.Location.X - 27, pictureBox2.Location.Y + 113)); // pc left stairs
            CreateWarpButton(this, CanalavePokecentre.Warps[1], new Point(pictureBox2.Location.X + 58, pictureBox2.Location.Y + 139)); // pc entrance
            CreateWarpButton(this, CanalavePokecentre.Warps[2], new Point(pictureBox2.Location.X + 145, pictureBox2.Location.Y + 113)); // pc right stairs

            CreateRouteConnectorButton(this, Player, "IronIsland", new Point(144, 410)); // IronIsland boat

            MapImages.SendToBack();
        }

    }
}
