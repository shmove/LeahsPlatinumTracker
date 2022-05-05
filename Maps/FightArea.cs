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
    public partial class FightArea : MapsForm
    {

        public FightArea()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            MapImages.Add(pictureBox1);
            MapImages.Add(pictureBox2);
            MapSector FightArea = Player.GetMapSector("FightArea");
            MapSector FightAreaPokecentre = Player.GetMapSector("FightArea Pokecentre");

            // create warp buttons
            CreateWarpButton(this, FightArea.Warps[0], new Point(251, 40)); // top left route connector building
            CreateWarpButton(this, FightArea.Warps[0], new Point(513, 131)); // pc
            CreateWarpButton(this, FightArea.Warps[0], new Point(729, 131)); // mart
            CreateWarpButton(this, FightArea.Warps[0], new Point(637, 244)); // lower houses top
            CreateWarpButton(this, FightArea.Warps[0], new Point(637, 318)); // lower houses bottom

            CreateWarpButton(this, FightAreaPokecentre.Warps[0], new Point(pictureBox2.Location.X - 27, pictureBox2.Location.Y + 113)); // centre left stairs
            CreateWarpButton(this, FightAreaPokecentre.Warps[1], new Point(pictureBox2.Location.X + 58, pictureBox2.Location.Y + 139)); // centre entrance
            CreateWarpButton(this, FightAreaPokecentre.Warps[2], new Point(pictureBox2.Location.X + 145, pictureBox2.Location.Y + 113)); // centre right stairs

            CreateRouteConnectorButton(this, Player, "Snowpoint", new Point(80, 195));
            CreateRouteConnectorButton(this, Player, "ResortArea", new Point(838, 202));

            MapImages.SendToBack();
        }

    }
}
