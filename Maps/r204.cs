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
    public partial class r204 : MapsForm
    {

        public r204()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            MapImages.Add(pictureBox1);
            MapSector r204N = Player.GetMapSector("204 N");
            MapSector r204S = Player.GetMapSector("204 S");

            // create warp buttons
            CreateWarpButton(this, r204N.Warps[0], new Point(271, 311)); // upper cave
            CreateWarpButton(this, r204S.Warps[0], new Point(127, 420)); // lower cave
            
            CreateRouteConnectorButton(this, Player, "Floaroma", new Point(129, 5)); // route up to floaroma
            CreateRouteConnectorButton(this, Player, "Jubilife", new Point(169, 774)); // route down to jubilife

            MapImages.SendToBack();
        }

    }
}
