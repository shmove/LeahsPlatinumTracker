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
    public partial class EternaForest : MapsForm
    {

        public EternaForest()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            MapImages.Add(pictureBox1);
            MapImages.Add(pictureBox2);
            MapSector EternaForest = Player.GetMapSector("EternaForest");
            MapSector EternaForestM = Player.GetMapSector("EternaForest M");

            // create warp buttons
            CreateWarpButton(this, EternaForestM.Warps[0], new Point(367, 130)); // mansion

            CreateWarpButton(this, EternaForest.Warps[0], new Point(587, 399)); // right exit
            CreateWarpButton(this, EternaForest.Warps[1], new Point(69, 576)); // bottom exit

            MapImages.SendToBack();
        }

    }
}
