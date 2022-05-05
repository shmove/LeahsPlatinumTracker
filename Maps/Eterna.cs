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
    public partial class Eterna : MapsForm
    {

        public Eterna()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            MapImages.Add(pictureBox1);
            MapImages.Add(pictureBox2);
            MapImages.Add(pictureBox3);
            MapSector Eterna = Player.GetMapSector("Eterna");
            MapSector TGEternaExt = Player.GetMapSector("TGEterna Ext");
            MapSector EternaPokecentre = Player.GetMapSector("Eterna Pokecentre");

            // create warp buttons
            CreateWarpButton(this, TGEternaExt.Warps[0], new Point(170, 12)); // exit from galactic building

            CreateWarpButton(this, Eterna.Warps[0], new Point(352, 34)); // top house
            CreateWarpButton(this, Eterna.Warps[1], new Point(169, 129)); // pc
            CreateWarpButton(this, Eterna.Warps[2], new Point(257, 155)); // house next to pc
            CreateWarpButton(this, Eterna.Warps[3], new Point(529, 232)); // house below statue
            CreateWarpButton(this, Eterna.Warps[4], new Point(249, 255)); // bike shop
            CreateWarpButton(this, Eterna.Warps[5], new Point(240, 353)); // mart
            CreateWarpButton(this, Eterna.Warps[6], new Point(329, 379)); // tall building next to mart
            CreateWarpButton(this, Eterna.Warps[7], new Point(67, 540)); // bottom left house
            CreateWarpButton(this, Eterna.Warps[8], new Point(281, 540)); // gym
            CreateWarpButton(this, Eterna.Warps[9], new Point(162, 628)); // bottom route building

            CreateWarpButton(this, EternaPokecentre.Warps[0], new Point(pictureBox3.Location.X - 27, pictureBox3.Location.Y + 113)); // centre left stairs
            CreateWarpButton(this, EternaPokecentre.Warps[1], new Point(pictureBox3.Location.X + 58, pictureBox3.Location.Y + 139)); // centre entrance
            CreateWarpButton(this, EternaPokecentre.Warps[2], new Point(pictureBox3.Location.X + 145, pictureBox3.Location.Y + 113)); // centre right stairs

            CreateRouteConnectorButton(this, Player, "205", new Point(5, 208)); // left route connector
            CreateRouteConnectorButton(this, Player, "211", new Point(865, 200)); // right route connector

            MapImages.SendToBack();
        }

    }
}
