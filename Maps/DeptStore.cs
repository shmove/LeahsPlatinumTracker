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
    public partial class DeptStore : MapsForm
    {

        public DeptStore()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            MapImages.Add(pictureBox1);
            MapSector DeptStore = Player.GetMapSector("DeptStore");

            // create warp buttons
            CreateWarpButton(this, DeptStore.Warps[0], new Point(110, 451)); // b1f

            CreateWarpButton(this, DeptStore.Warps[1], new Point(94, 286)); // 1f left
            CreateWarpButton(this, DeptStore.Warps[2], new Point(171, 262)); // 1f right
            CreateWarpButton(this, DeptStore.Warps[3], new Point(148, 320)); // entrance

            CreateWarpButton(this, DeptStore.Warps[4], new Point(94, 102)); // 2f left
            CreateWarpButton(this, DeptStore.Warps[5], new Point(171, 77)); // 2f right

            CreateWarpButton(this, DeptStore.Warps[6], new Point(442, 469)); // 3f left
            CreateWarpButton(this, DeptStore.Warps[7], new Point(515, 444)); // 3f right

            CreateWarpButton(this, DeptStore.Warps[8], new Point(442, 288)); // 4f left
            CreateWarpButton(this, DeptStore.Warps[9], new Point(515, 263)); // 4f right

            CreateWarpButton(this, DeptStore.Warps[10], new Point(442, 102)); // 5f

            MapImages.SendToBack();
        }

    }
}
