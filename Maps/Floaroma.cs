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
    public partial class Floaroma : MapsForm
    {

        public Floaroma()
        {
            InitializeComponent();
        }

        private void Floaroma_Load(object sender, EventArgs e)
        {
            MapImages.Add(pictureBox1);
            MapImages.Add(pictureBox2);
            MapSector Floaroma = Player.GetMapSector("Floaroma");
            MapSector FloaromaPokecentre = Player.GetMapSector("Floaroma Pokecentre");

            // create warp buttons
            CreateWarpButton(this, Floaroma.Warps[0], new Point(10,21)); // meadows entrance
            CreateWarpButton(this, Floaroma.Warps[1], new Point(154, 173)); // left house
            CreateWarpButton(this, Floaroma.Warps[2], new Point(289, 127)); // florists
            CreateWarpButton(this, Floaroma.Warps[3], new Point(362, 210)); // mart
            CreateWarpButton(this, Floaroma.Warps[4], new Point(225, 319)); // centre
            CreateWarpButton(this, Floaroma.Warps[5], new Point(360, 319)); // right house

            CreateWarpButton(this, FloaromaPokecentre.Warps[0], new Point(pictureBox2.Location.X - 27, pictureBox2.Location.Y + 113)); // centre left stairs
            CreateWarpButton(this, FloaromaPokecentre.Warps[1], new Point(pictureBox2.Location.X + 58, pictureBox2.Location.Y + 139)); // centre entrance
            CreateWarpButton(this, FloaromaPokecentre.Warps[2], new Point(pictureBox2.Location.X + 145, pictureBox2.Location.Y + 113)); // centre right stairs

            CreateRouteConnectorButton(this, Player, "205", new Point(462, 263)); // right route
            CreateRouteConnectorButton(this, Player, "204", new Point(147, 391)); // bottom route

            MapImages.SendToBack();
        }

    }
}
